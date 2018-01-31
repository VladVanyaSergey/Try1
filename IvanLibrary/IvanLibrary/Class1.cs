using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IvanLibrary
{
	public static class GiveMeBlockStructureWithRemainElements
	{
		public static void StartWorking(string AdressTextFile ,string adressIndex, TextBox textbox)
		{
			string[,] index = ReadInformFromSemanticFragmentTable(adressIndex);
			string text = ReadText(AdressTextFile);
			int inaccuracy = 0;
			string underline = "";
			for (double i = 1; i < textbox.Size.Width / 6.135; i++)
			{
				underline = underline + "_";
			}

			for (int i = 0; i < index.Length / 3; i++)
			{
				string timetext = "\r\n" + "\r\n" + index[i, 2] + "\r\n" + "\r\n";
				text = text.Insert(Convert.ToInt32(index[i,0])+ inaccuracy, timetext);
				inaccuracy = inaccuracy + timetext.Length;

				timetext = "\r\n" + underline + "\r\n" + "\r\n";
				text = text.Insert(Convert.ToInt32(index[i, 1]) + inaccuracy, timetext);
				inaccuracy = inaccuracy + timetext.Length;
			}
			textbox.Text = text;
		}
		private static string[,] ReadInformFromSemanticFragmentTable(string File)
		{
			int lengthTable = System.IO.File.ReadAllLines(File).Length;
			string[,]  index = new string[lengthTable, 3];
			int ind = 0;
			using (StreamReader sr = new StreamReader(File))   //System.IO.File.Create(File))
			{
				while (sr.Peek() >= 0)
				{
					string s = sr.ReadLine();
					string[] s1 = s.Split('\t');
					index[ind, 0] = s1[1];
					index[ind, 1] = s1[2];
					index[ind, 2] = s1[0];
					ind = ind + 1;
				}
			}
			return index;
		}
		private static string ReadText(string AdressTextFile)
		{
			string text = "";
			using (StreamReader sr = new StreamReader(AdressTextFile))   //System.IO.File.Create(File))
			{
				while (sr.Peek() >= 0)
				{
					text = text + sr.ReadToEnd();
				}
			}
			return text;
		}
	}
	public static class CreateSemanticFile
	{
		static string[,] index;
		static List<int> ProblemElements;
		public static void SelectedTextIntoIndexForSemanticFragmentTable(System.Windows.Forms.TextBox textBox, string File)
		{
			if (textBox.SelectionLength > 0)
			{
				int[] outdata = new int[2];
				outdata[0] = textBox.SelectionStart;
				outdata[1] = textBox.SelectionStart + textBox.SelectionLength - 1;
				AddIndexIntoSemanticFragmentTable(File, outdata, textBox);
			}
			else
			{
				MessageBox.Show("Вы не выделили смысловой фрагмент");
			}
		} //Эта функция вытаскивает выделенный фрагмент и извлекает начальную и конечную координату
		private static void AddIndexIntoSemanticFragmentTable(string File, int[] outdata, System.Windows.Forms.TextBox textBox)
		{
			index = ReadInformFromSemanticFragmentTable(File);
			index = SortMatrix(index);
			index[0, 0] = outdata[0].ToString();
			index[0, 1] = outdata[1].ToString();
			index[0, 2] = NewNameOfSemanticFragment(index);
			ProblemElements = CheckCrossingElements(index);
			if (ProblemElements.Count() > 0)
			{
				//if (MessageBox.Show("Затронуты области пересечения. Вы хотите...", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				//{
				//   TakeProblemPartOfTheText(textBox);
				//	rewriting.ShowDialog();
				//}
				//else
				//{
				//	index = RemoveOneLineInSemanticFragmentTable(index, 0);
				//	WriteInformIntoSemanticFragmentTable(File, index);
				//}
				MessageBox.Show("Затронуты области пересечения. Попробуйте выделить повторно.");
			}
			else
			{
				index = SortMatrix(index);
				WriteInformIntoSemanticFragmentTable(File, index);
			}
		}
		private static string[,] ReadInformFromSemanticFragmentTable(string File)
		{
			int lengthTable = System.IO.File.ReadAllLines(File).Length + 1;
			index = new string[lengthTable, 3];
			index[0, 0] = "-1";
			index[0, 1] = "-1";
			index[0, 2] = "-1";
			int ind = 1;
			using (StreamReader sr = new StreamReader(File))   //System.IO.File.Create(File))
			{
				while (sr.Peek() >= 0)
				{
					string s = sr.ReadLine();
					string[] s1 = s.Split('\t');
					index[ind, 0] = s1[1];
					index[ind, 1] = s1[2];
					index[ind, 2] = s1[0];
					ind = ind + 1;
				}
			}
			return index;
		}                                           //Считываение из таблицы смысловых фрагментов
		private static string[,] SortMatrix(string[,] index)
		{
			List<int> FirstElement = new List<int>();
			List<int> SecondElement = new List<int>();
			List<string> ThirdElement = new List<string>();
			string[,] timeindex = new string[index.GetLength(0), index.GetLength(1)];
			for (int i = 0; i < index.GetLength(0); i++)
			{
				FirstElement.Add(Convert.ToInt32(index[i, 0]));
				SecondElement.Add(Convert.ToInt32(index[i, 1]));
				ThirdElement.Add(index[i, 2]);
			}
			for (int i = 0; i < index.GetLength(0); i++)
			{
				int c = FirstElement.IndexOf(FirstElement.Min());
				timeindex[i, 0] = FirstElement[c].ToString();
				timeindex[i, 1] = SecondElement[c].ToString();
				timeindex[i, 2] = ThirdElement[c];
				FirstElement.RemoveAt(c);
				SecondElement.RemoveAt(c);
				ThirdElement.RemoveAt(c);
			}
			return timeindex;
		}                                                                   //Сортировка матрицы по начальному индексу																														//Сортировка по индексу
		private static void WriteInformIntoSemanticFragmentTable(string File, string[,] index)
		{
			using (StreamWriter sw = new StreamWriter(File))
			{
				for (int i = 0; i < index.Length / 3; i++)
				{
					sw.WriteLine(index[i, 2] + "\t" + index[i, 0] + "\t" + index[i, 1]);
				}
			}
		}                           //Запись списка элементов в таблицу смысловых фрагментов
		private static string NewNameOfSemanticFragment(string[,] index)
		{
			int ind = 1;
			for (int i = 0; i < index.GetLength(0); i++)
			{
				if ("СФ" + ind.ToString() == index[i, 2])
				{
					i = -1;
					ind += 1;
				}
			}
			return "СФ" + ind;
		}                                                   //Дефолтное название
		private static List<int> CheckCrossingElements(string[,] index)
		{
			List<int> ProblemElements = new List<int>();
			if (index.GetLength(0) > 1)
			{
				for (int i = 1; i < index.GetLength(0); i++)
				{
					int x1 = Convert.ToInt32(index[0, 0]);
					int x2 = Convert.ToInt32(index[0, 1]);
					int y1 = Convert.ToInt32(index[i, 0]);
					int y2 = Convert.ToInt32(index[i, 1]);
					if (((x1 > y1) && (x1 < y2)) || ((x2 < y2) && (x2 > y1)) || ((x1 < y1) && (x2 > y2)) || (x1 == y1) || (x1 == y2) || (x2 == y1) || (x2 == y2))
					{
						ProblemElements.Add(i);
					}
				}
			}
			return ProblemElements;
		}
		private static string[,] RemoveOneLineInSemanticFragmentTable(string[,] index, int NumberOfLine)
		{
			string[,] timeindex = new string[index.GetLength(0) - 1, index.GetLength(1)];
			int delta = 0;
			for (int i = 0; i < index.GetLength(0); i++)
			{
				if (i != NumberOfLine)
				{

					timeindex[i - delta, 0] = index[i, 0];
					timeindex[i - delta, 1] = index[i, 1];
					timeindex[i - delta, 2] = index[i, 2];
				}
				else
				{
					delta = 1;
				}
			}
			return timeindex;
		}
        //Все, чтобы вытащить нужный кусок текста
        private static List<int> ArrayIntoList()
        {
            List<int> ListIndex = new List<int>();
            ListIndex.Add(Convert.ToInt32(index[0, 0]));
            ListIndex.Add(Convert.ToInt32(index[0, 1]));
            for (int i = 0; i < ProblemElements.Count(); i++)
            {
                ListIndex.Add(Convert.ToInt32(index[ProblemElements[i], 0]));
                ListIndex.Add(Convert.ToInt32(index[ProblemElements[i], 1]));
            }
            return ListIndex;
        }
    }
	public class RewritingClass
	{
		public static void StartWork(TextBox textbox,RichTextBox richtextbox, System.Windows.Forms.ListBox listbox, string[,] index, List<int> ProblemElements, string ProblemText)
		{
			listbox.Items.Clear();
            listbox.Items.Add(index[0, 2]);
			for (int i = 0; i < ProblemElements.Count; i++)
			{
				listbox.Items.Add(index[ProblemElements[i], 2]);
				listbox.SetSelected(0,true);
            }
            textbox.Text = ProblemText;
			richtextbox.Text = ProblemText;
			richtextbox.Select(1,9);
			richtextbox.SelectionBackColor = Color.BlueViolet;
			richtextbox.Select(31, 9);
			richtextbox.SelectionBackColor = Color.Aquamarine;


		}
		public static void SelectingText(TextBox textbox, RichTextBox richtextbox, System.Windows.Forms.ListBox listbox, string[,] index, List<int> ProblemElements)
		{

		    //textbox.Select
		}
	}//Временно потерянный код, возможно я к нему еще вернусь. Если я не помню зачем это нужно, то можно смело удалять (нужен для мастера выделения)
	//public class Neo4j
}
