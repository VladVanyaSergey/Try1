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
	public class CreateSemanticFile
	{
		public static void SelectedTextIntoIndexForSemanticFragmentTable(System.Windows.Forms.TextBox textBox, string File)
		{
			if (textBox.SelectionLength > 0)
			{
				int[] outdata = new int[2];
				outdata[0] = textBox.SelectionStart;
				outdata[1] = textBox.SelectionStart + textBox.SelectionLength - 1;
				AddIndexIntoSemanticFragmentTable(File, outdata);
			}
			else
			{
				MessageBox.Show("Вы не выделили смысловой фрагмент");
			}
		}
		public static void AddIndexIntoSemanticFragmentTable(string File, int[] outdata)
		{
			string[,] index = ReadInformFromSemanticFragmentTable(File);
			index = SortMatrix(index);
			index[0, 0] = outdata[0].ToString();
			index[0, 1] = outdata[1].ToString();
			index[0, 2] = NewNameOfSemanticFragment(index);
			//index = CheckCrossingElements(index);
			WriteInformIntoSemanticFragmentTable(File, index);
		}
		private static string[,] ReadInformFromSemanticFragmentTable(string File)
		{
			int lengthTable = System.IO.File.ReadAllLines(File).Length + 1;
			string[,] index = new string[lengthTable, 3];
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
		}
		private static void WriteInformIntoSemanticFragmentTable(string File, string[,] index)
		{
			using (StreamWriter sw = new StreamWriter(File))
			{
				for (int i = 0; i < index.Length / 3; i++)
				{
					sw.WriteLine(index[i,2] + "\t" + index[i, 0] + "\t" + index[i, 1]);
				}
			}
		}
		private static string NewNameOfSemanticFragment(string[,] index)
		{
			int ind = 1;
			for (int i = 0; i < index.GetLength(0); i++)
			{
				if ("СФ" + ind.ToString() == index[i, 2])
				{
					i = 0;
					ind += 1;
				}
			}
			return "СФ"+ind;
		}
		
		//private static int[,] RemoveOneElement(int [,]index)
		//{

		//}
		private static int[,] CheckCrossingElements(int[,] index)
		{
			if (index.Length > 2)
			{
				//Если новый интервал полоностью покрывает старый
				if (index[0, 0] <= index[1, 0] && index[1, 0] >= index[0, 0])
				{

				}
			}
			return index;
		}
	}
}
