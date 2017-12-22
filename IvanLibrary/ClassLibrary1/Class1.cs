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

namespace IvanNS
{
	public class Ivan
	{
		//////////
		private static int[] outdata = new int[2];
		public static int[] SelectedTextIntoIndexForSemanticFragmentTable(System.Windows.Forms.TextBox textBox)
		{
			if (textBox.SelectionLength > 0)
			{
				outdata[0] = textBox.SelectionStart;
				outdata[1] = textBox.SelectionStart + textBox.SelectionLength - 1;
				return outdata;
			}
			else
			{
				MessageBox.Show("Вы не выделили смысловой фрагмент");
				return outdata;
			}
		}
		private static int[,] ReadInformFromSemanticFragmentTable(string File)
		{
			int lengthTable = System.IO.File.ReadAllLines(File).Length + 1;
			int[,] index = new int[lengthTable, 2];
			int ind = 0;
			using (StreamReader sr = new StreamReader(File))   //System.IO.File.Create(File))
			{
				while (sr.Peek() >= 0)
				{
					string s = sr.ReadLine();
					string[] s1 = s.Split('\t');
					index[ind, 0] = Convert.ToInt32(s1[1]);
					index[ind, 1] = Convert.ToInt32(s1[2]);
					ind = ind + 1;
				}
			}
			return index;
		}
		private static void WriteInformIntoSemanticFragmentTable(string File, int[,] index)
		{
			using (StreamWriter sw = new StreamWriter(File))
			{
				for (int i = 0; i < index.Length / 2; i++)
				{
					sw.WriteLine("СФ" + i.ToString() + "\t" + index[i, 0].ToString() + "\t" + index[i, 1].ToString());
				}
			}
		}
		public static void AddIndexIntoSemanticFragmentTable(string File)
		{
			int[,] index = ReadInformFromSemanticFragmentTable(File);
			index = Ivan.SortMatrix(index);
			index[0, 0] = outdata[0];
			index[0, 1] = outdata[1];
			index = Ivan.CheckCrossingElements(index);
			WriteInformIntoSemanticFragmentTable(File, index);
		}
		private static int[,] SortMatrix(int[,] index)
		{
			List<int> FirstElement = new List<int>();
			List<int> SecondElement = new List<int>();
			int[,] timeindex = new int[index.GetLength(0), index.GetLength(1)];
			for (int i = 0; i < index.GetLength(0); i++)
			{
				FirstElement.Add(index[i, 0]);
				SecondElement.Add(index[i, 1]);
			}
			for (int i = 0; i < index.GetLength(0); i++)
			{
				int c = FirstElement.IndexOf(FirstElement.Min());
				timeindex[i, 0] = FirstElement[c];
				timeindex[i, 1] = SecondElement[c];
				FirstElement.RemoveAt(c);
				SecondElement.RemoveAt(c);
			}
			return timeindex;
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
