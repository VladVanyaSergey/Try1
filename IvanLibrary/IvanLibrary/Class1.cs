﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Neo4jClient;


namespace IvanLibrary
{
	public static class GiveMeBlockStructureWithRemainElements
	{
		public static int[,] StartWorking(string AdressTextFile, string adressIndex, TextBox textbox)
		{
			string[,] index = ReadInformFromSemanticFragmentTable(adressIndex);
			string text = ReadText(AdressTextFile);
			int inaccuracy = 0;
			string underline = "";
			int[,] Intron = new int[index.Length * 2 / 3, 2];
			for (double i = 1; i < (textbox.Size.Width - 21) / 6.135; i++)
			{
				underline = underline + "_";
			}

			for (int i = 0; i < index.Length / 3; i++)
			{
				string timetext = "\r\n" + "\r\n" + index[i, 2] + "\r\n" + "\r\n";
				text = text.Insert(Convert.ToInt32(index[i, 0]) + inaccuracy, timetext);
				Intron[i * 2, 0] = Convert.ToInt32(index[i, 0]) + inaccuracy;
				inaccuracy = inaccuracy + timetext.Length;
				Intron[i * 2, 1] = Convert.ToInt32(index[i, 0]) + inaccuracy;

				timetext = "\r\n" + underline + "\r\n" + "\r\n";
				text = text.Insert(Convert.ToInt32(index[i, 1]) + 1 + inaccuracy, timetext);
				Intron[i * 2 + 1, 0] = Convert.ToInt32(index[i, 1]) + inaccuracy;
				inaccuracy = inaccuracy + timetext.Length;
				Intron[i * 2 + 1, 1] = Convert.ToInt32(index[i, 1]) + inaccuracy;
			}
			textbox.Text = text;
			return Intron;
		} // Текст --> текст разбитый на смысловой фрагмент 
		public static string[,] ReadInformFromSemanticFragmentTable(string File)
		{
			int lengthTable = System.IO.File.ReadAllLines(File).Length;
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
		}// Считывание данных из таблицы смысловых фрагемнтов
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
		}//Считывание файла с текстом. Нужно пересмотреть!!! Тут я написал индуский код
	}
	public static class CreateSemanticFile
	{
		static string[,] index; //Таблица смысловых фрагментов
		static List<int> ProblemElements;
		public static bool SelectedTextIntoIndexForSemanticFragmentTable(System.Windows.Forms.TextBox textBox, string File, int[,] Intron)
		{
			var selectedcoor = new SelectedCoor() { Start = textBox.SelectionStart, End = textBox.SelectionStart + textBox.SelectionLength - 1 };
			if (selectedcoor.CheckUserSelectText())
			{
				if (!CheckUserSelectedIntrons(selectedcoor.Start, selectedcoor.End, Intron))
				{
					selectedcoor = CorrectionOutdata(selectedcoor, Intron);
					AddIndexIntoSemanticFragmentTable(File, selectedcoor);
					return true;
				}
			}
			else
			{
				MessageBox.Show("Вы не выделили текст");
			}
			return false;
		} //Эта функция вытаскивает выделенный фрагмент и извлекает начальную и конечную координату
		private class SelectedCoor
		{
			public int Start;
			public int End;
			public bool CheckUserSelectText()
			{
				return Start != End;
			}
		}
		private static SelectedCoor CorrectionOutdata(SelectedCoor selectedcoor, int[,] Intron)
		{
			int Correction = 0;
			for (int i = 0; i < Intron.Length / 2; i++)
			{
				if (selectedcoor.Start > Intron[i, 0])
				{
					Correction = Correction + Intron[i, 1] - Intron[i, 0];
				}
			}
			selectedcoor.Start -= Correction;
			selectedcoor.End -= Correction;
			return selectedcoor;

		}//Поправка выделенных координат с учетом координат элементов по типу "СФ" или "___"
		public static bool CheckUserSelectedIntrons(int start, int end, int[,] Intron)
		{
			bool condition = false;
			for (int i = 0; i < Intron.Length / 2; i++)
			{
				condition = (((start >= Intron[i, 0]) && (start <= Intron[i, 1])) || ((end >= Intron[i, 0]) && (end <= Intron[i, 1])));
				condition = condition || (start <= Intron[i, 0]) && (end >= Intron[i, 0]);
				condition = condition || (start <= Intron[i, 1]) && (end >= Intron[i, 1]);
				if (condition)
				{
					MessageBox.Show("Неправильное магическое число. Попробуйте выделить повторно.");
					return condition;
				}
			}
			return condition;
		}//Проверка выделенного текста на появление в нем элементов по типу "СФ" или "____"

		private static void AddIndexIntoSemanticFragmentTable(string File, SelectedCoor selectedcoor)
		{
			index = Read_SFT_Add_place_new_element(File);
			index = SortMatrix(index);
			index[0, 0] = selectedcoor.Start.ToString();
			index[0, 1] = selectedcoor.End.ToString();
			index[0, 2] = NewNameOfSemanticFragment(index);
			ProblemElements = CheckCrossingElements(index);
			if (ProblemElements.Count() > 0)
			{
				MessageBox.Show("Затронуты области пересечения. Попробуйте выделить повторно.");
			}
			else
			{
				index = SortMatrix(index);
				WriteInformIntoSemanticFragmentTable(File, index);
			}
		}//добавление СФ в таблицу смысловых фрагментов
		private static string[,] Read_SFT_Add_place_new_element(string File)
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
		}//Считываение из файла таблицы смысловых фрагментов
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
		}//Сортировка матрицы по начальному индексу																														//Сортировка по индексу
		private static void WriteInformIntoSemanticFragmentTable(string File, string[,] index)
		{
			using (StreamWriter sw = new StreamWriter(File))
			{
				for (int i = 0; i < index.Length / 3; i++)
				{
					sw.WriteLine(index[i, 2] + "\t" + index[i, 0] + "\t" + index[i, 1]);
				}
			}
		}//Запись списка элементов в таблицу смысловых фрагментов
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
		}//Дефолтное название смысловых фрагментов
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
		} //Выдает список пересекающиеся фрагментов с новым  
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
		}//Удаляет один элемент из таблицы смысловых фрагментов
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
		}//Перевод массива в список 
	}
	public class FocusAfterCreateSemanticFile
	{
		public static int FindNewElementofSFT(string File)
		{
			string[,] index = GiveMeBlockStructureWithRemainElements.ReadInformFromSemanticFragmentTable(File);
			int ind = 1;
			int position = 0;
			for (int i = 0; i < index.GetLength(0); i++)
			{
				if ("СФ" + ind.ToString() == index[i, 2])
				{
					position = i;
					i = -1;
					ind += 1;
				}
			}
			return position;
		}
	}
	public class Neo4j
	{
		//Переменные
		public string adress = "http://localhost:7474/db/data";
		public string username = "neo4j";
		public string password = "1234";
		GraphClient client;
		public List<string> TypesRelationships;
		public Neo4j(){}//Конструктор
		//Рабочая часть
		public bool ConnectToDataBase(string ad, string us, string pa)
		{
			adress = ad;
			username = us;
			password = pa;
			try
			{
				client = new GraphClient(new Uri(adress), username, password);
				client.Connect();
				ListOfRelationships();
				BasicTools();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message.ToString());
				return false;
			}
		}//Команда для подключения к базе данных
		public void BasicTools()
		{
			CheckAndCreateRelationships("Функциональная");
			CheckAndCreateRelationships("Количественная");
			CheckAndCreateRelationships("Пространственная");
			CheckAndCreateRelationships("Временная");
			CheckAndCreateRelationships("Атрибутивная");
			CheckAndCreateRelationships("Логическая");
			CheckAndCreateRelationships("Лингвистическая");
			CheckAndCreateRelationships("Родовидовая");
			CheckAndCreateRelationships("Синонимы");
		}
		public void AddTerm(string Term)
		{
			client.Cypher
				.Create(CommandForCreate("Terms","Name",Term))
				.ExecuteWithoutResults();
			MessageBox.Show("Понятие \""+Term + "\" был успешно добавлено. Сережа тут должен быть твой label помошник");
		}
		public List<AllTerms> FindAllTerms()
		{
			var OutputArray_from_neo4j = client.Cypher
				.Match("(List:Terms)")
				.Return<ID>("List")
				.Results.ToArray();
			var OutputArray_from_neo4j_id = client.Cypher
				.Match("(List:Terms)")
				.Return<string>("id(List)")
				.Results.ToArray();
			MessageBox.Show("Список успешно получен");
			List<AllTerms> allterms = new List<AllTerms>();
			for (int i = 0; i < OutputArray_from_neo4j.Length; i++)
			{
				AllTerms all = new AllTerms();
				all.id = OutputArray_from_neo4j_id[i];
				all.name = OutputArray_from_neo4j[i].Name;
				allterms.Add(all);
			}
			return allterms;
		}
		public List<AllTerms> FindOneTermReturnID(string text)
		{
			List<AllTerms> OneTerm = new List<AllTerms>();
			var OutputArray_from_neo4j = client.Cypher
				.Match(CommandMatch("Result","Terms"))
				.Where(CommandWhere("Result","Name",text))
				.Return<ID>("id(Result)")
				.Results.ToArray();
			MessageBox.Show("Список успешно получен");
			for (int i = 0; i < OutputArray_from_neo4j.Length; i++)
			{
				AllTerms allterms = new AllTerms();
				allterms.id = OutputArray_from_neo4j[i].Name;
				allterms.name = text;
				OneTerm.Add(allterms);
			}
			return OneTerm;
		}
		public void CreateLink(string Term1,string Term2, string Relation)
		{
			client.Cypher
				.Match(CommandMatch("Term1", "Terms"))
				.Where(CommandWhere("Term1", "Name", Term1))
				.Match(CommandMatch("Term2", "Terms"))
				.Where(CommandWhere("Term2", "Name", Term2))
				.Merge(CommandMerge("Term1","Term2", Relation))
				.ExecuteWithoutResults();
			MessageBox.Show("Связь создана");
		}
		public void CreateRelationships(string Nametype)
		{
			client.Cypher
				.Create(CommandCreateRelationships(Nametype))
				.ExecuteWithoutResults();
			MessageBox.Show("Новый тип отношений создан");
		}
		public List<Relationship> MassiveRelationships()
		{
			List<Relationship> Relationships = new List<Relationship>();
			string[] OutRelationsMassive = TakeRelationFromDataUsingOneGroupOfTerms();
			for (int i = 0; i < OutRelationsMassive.Length; i++)
			{
				Relationship relationship = new Relationship();
				string[] line = OutRelationsMassive[i].Split('\r');
				try { relationship.id = TakeIDRelationFromData(line[FindNecessaryElementOfStringArray(line, "id")]); } catch { }
				try { relationship.type = TakeTypeRelationFromData(line[FindNecessaryElementOfStringArray(line, "type")]); } catch { }
				try { relationship.startElement = TakeStartElementOfRelationFromData(line[FindNecessaryElementOfStringArray(line, "start")]); } catch { }
				try { relationship.endElement = TakeEndElementOfRelationFromData(line[FindNecessaryElementOfStringArray(line, "end")]); } catch { }
				Relationships.Add(relationship);
			}
			return Relationships;
		}
		public void ListOfRelationshipsTerms()
		{
			List<Relationship> massive = MassiveRelationships();
			List<string> listRelationShips = new List<string>();
			for (int i = 0; i < massive.Count; i++)
			{
				listRelationShips.Add(massive[i].type);
			}
			var list= listRelationShips.Distinct<string>();
			listRelationShips=list.ToList<string>();
			TypesRelationships = listRelationShips;
		}
		public void ListOfRelationships()
		{
			var Neo4jOutputRelationship  = client.Cypher
				.Match("p=()-[r]-()")
				.Return<string>("r")
				.Results.ToArray();
			List<string> listOfRelationships = new List<string>();
			for (int i = 0; i < Neo4jOutputRelationship.Length; i++)
			{
				string[] line = Neo4jOutputRelationship[i].Split('\r');
				listOfRelationships.Add(TakeTypeRelationFromData(line[4]));
			}
			var list = listOfRelationships.Distinct<string>();
			listOfRelationships = list.ToList<string>();
			TypesRelationships = listOfRelationships;
			TypesRelationships.Sort();
		}
		public void DeleteLink(string Term1, string Term2)
		{
			client.Cypher
				.Match(CommandMatchForDeleteLink("Result", "nodeTerm", "Terms", Term1, "link", "nodeOther", Term2))
				.Delete("link")
				.ExecuteWithoutResults();
			MessageBox.Show("Связь удалена");
		}
		public void RenameLink(string Term1, string Term2, string link)
		{
			client.Cypher
				.Match(CommandMatchForDeleteLink("Result", "nodeTerm", "Terms", Term1, "link", "nodeOther", Term2))
				.Delete("link")
				.Create(CommandCreateForDeleteLink("Result1", "nodeTerm", "Terms", Term1, link, "nodeOther", Term2))
				.ExecuteWithoutResults();
			MessageBox.Show("Связь изменена");
		}
		private void InverseLink(string Term1, string Term2)
		{
			client.Cypher
				.Match(CommandMatchForDeleteLink("Result", "nodeTerm", "Terms", Term1, "link", "nodeOther", Term2))
				.Delete("link as links")
				.Create(CommandCreateForDeleteLink("Result1", "nodeTerm", "Terms", Term2, "links", "nodeOther", Term1))
				.ExecuteWithoutResults();
			MessageBox.Show("Связь инвертирована");
		}//Надо допилить
		public void DeleteAllLinksWithOneNode(string Term)
		{
			client.Cypher
				.Match(CommandMatchForDelete("Result","n","Terms",Term,"link"))
				.Delete("link")
				.ExecuteWithoutResults();
			MessageBox.Show("Связи с понятием \"" + Term + "\" были успешно удалены. Сережа тут должен быть твой label помошник");
		}
		public void DeleteTerm(string Term)
		{
			client.Cypher
				.Match(CommandMatchForDelete("Result", "n", "Terms", Term, "link"))
				.Delete("link")
				.Delete("n")
				.ExecuteWithoutResults();
			MessageBox.Show("Понятие \"" + Term + "\" был успешно удалено. Сережа тут должен быть твой label помошник");
		}
		public void DeleteRelationship(string Relationship)
		{
			client.Cypher
				.Match("p=()-[link:"+ Relationship + "]-()")
				.Delete("link")
				.ExecuteWithoutResults();
			MessageBox.Show("Связь \"" + Relationship + "\" была успешно удалена. Сережа тут должен быть твой label помошник");
		}
		public void RenameRelationship(string RelationshipOld, string RelationshipNew)
		{
			client.Cypher
				.Match("p=(n)-[link:" + RelationshipOld + "]->(m)")
				.Delete("link")
				.Create("(n)-[:" + RelationshipNew + "]->(m)")
				.ExecuteWithoutResults();
			MessageBox.Show("Связь \"" + RelationshipOld + "\" была успешно переименованна в \""+ RelationshipNew + "\". Сережа тут должен быть твой label помошник");
		}

		//Вспомогательные методы
		private string[] TakeRelationFromDataUsingOneGroupOfTerms()
		{
			var OutputArray_from_neo4j = client.Cypher
				.Match("(n:Terms)-[r]->(m:Terms)")
				.Return<string>("r")
				.Results.ToArray();
			return OutputArray_from_neo4j;
		}
		private int TakeIDRelationFromData(string line)
		{
			line=line.Split(':')[1];
			line=line.Replace(',', ' ');
			return Convert.ToInt32(line);
		}
		private string TakeTypeRelationFromData(string line)
		{
			line = line.Split(':')[1];
			line=line.Remove(1, 1);
			line=line.Remove(line.Length-1, 1);
			char[] ch = new char[1];
			ch[0] = ' ';
			line=line.Trim(ch);
			return line;
		}
		private string TakeStartElementOfRelationFromData(string line)
		{
			line = line.Substring(13);
			line = line.Remove(line.Length - 2, 2);
			return line;
		}
		private string TakeEndElementOfRelationFromData(string line)
		{
			line = line.Substring(11);
			line = line.Remove(line.Length - 2, 2);
			return line;
		}
		private string CommandForCreate(string Node, string key, string value)
		{
			return "(u:" + Node + " {" + key + ":\'" + value + "\'})";
		}
		private string CommandMatch(string res, string Group)
		{ return "(" + res + ":" + Group + ")"; }
		private string CommandMatchForDelete(string res, string nodeRes, string Group, string Term, string link)
		{
			return res + "=(" + nodeRes + ":" + Group + "{Name:\"" + Term + "\"})-[" + link + "]-()";
		}
		private string CommandMatchForDeleteLink(string res, string nodeResIn, string Group, string Term1, string link, string nodeResOut,string Term2)
		{
			return res + "=(" + nodeResOut + ":" + Group + "{Name:\"" + Term1 + "\"})-[" + link + "]->(" + nodeResIn + ":" + Group + "{Name:\"" + Term2 + "\"})";
		}
		private string CommandCreateForDeleteLink(string res, string nodeResIn, string Group, string Term1, string link, string nodeResOut, string Term2)
		{
			return res + "=(" + nodeResOut + ")-[:" + link + "]->(" + nodeResIn + ")";
		}
		private string CommandWhere(string ResMatch, string attribute, string required)
		{ return "(" + ResMatch + "." + attribute + " = \'" + required + "\')"; }
		private string CommandMerge(string ResMatchOut, string ResMatchIn, string Relation)
		{ return "(" + ResMatchOut + ")-[:" + Relation + "]->(" + ResMatchIn + ")"; }
		private string CommandCreateRelationships(string Nametype)
		{ return "(()-[:" + Nametype + "]->())"; }
		private int FindNecessaryElementOfStringArray(string[] line, string pattern)
		{
			for (int j = 0; j < line.Length; j++)
			{
				var index = line[j].IndexOf(pattern);
				if (index != -1)
				{
					return j;
				}
			}
			return -1;
		}
		private void CheckAndCreateRelationships(string newRelation)
		{
			bool f = TypesRelationships.Contains(newRelation);
			if (!TypesRelationships.Contains(newRelation))
			{
				CreateRelationships(newRelation);
				TypesRelationships.Add(newRelation);
			}
		}

		//Вспомогательные классы (Если их будет мало, то объединить с блоком "Вспомогательные методы")
		public class ID
		{public string Name { get; set; }}
		public class AllTerms
		{
			public string id;
			public string name;
		}
		public class Relationship
		{
			public int id;
			public string type;
			public string startElement;
			public string endElement;
			public Relationship() { }
		}
	}
	public class SelectedTextSecondForm
	{
		public static void SelecteTextSecondWindow(TextBox textBox, Neo4j neo4j, int[,] Intron)
		{
			int[] outdata = new int[2];
			outdata[0] = textBox.SelectionStart;
			outdata[1] = textBox.SelectionStart + textBox.SelectionLength - 1;
			string text = textBox.SelectedText;
			if (!CreateSemanticFile.CheckUserSelectedIntrons(outdata[0], outdata[1], Intron))
			{
				neo4j.ConnectToDataBase("http://localhost:7474/db/data", "neo4j", "1234");
				text = ProtectFromCrazyMan(text, neo4j);
				if (text != "")
				{
					neo4j.AddTerm(text);
				}
			}
		}
		public static string ProtectFromCrazyMan(string text, Neo4j neo4J)
		{
			text=KillStrangeElements(text);
			text = text[0].ToString().ToUpper() + text.Substring(1);
			var allterms = neo4J.FindAllTerms();
			var repeats = allterms.FindAll(x => x.name == text);
			if (repeats.Count == 0) { return text; }
			else
			{
				MessageBox.Show("Такое понятие уже существует");
			}
			return "";
		}
		public static string KillStrangeElements(string text)
		{
			char[] killElements = new char[3];
			killElements[0] = '\n';
			killElements[1] = '\r';
			killElements[2] = ' ';
			text.Trim(killElements);
			return text;
		}
	}
}
