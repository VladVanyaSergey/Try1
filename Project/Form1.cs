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
using Word = Microsoft.Office.Interop.Word; //Введение алиаса пространства имен Word
using Microsoft.VisualBasic;  //Подключение Inputbox (В обозревателе решений:Ссылки>Добавить ссылку>Сборки>выбрать Microsoft.VisualBasic  > написать здесь using Microsoft.VisualBasic
using System.Reflection;
using IvanLibrary;
using SergeyLibrary;
using VladLibrary;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
		//
		int[,] Intron;
		public static IvanLibrary.Neo4j neo4j = new IvanLibrary.Neo4j();
        //
		public Form1()
        {
			InitializeComponent();
			listBox1.ContextMenuStrip = contextMenuStrip1;
		}

        private void button1_Click(object sender, EventArgs e)
        {
			string A = comboBox1.Text;
            string p1 = "";
            //A = "Иван"; // Потом убрать, это времянка!!!!
            if (label1.Text == "1")
            {
                Sergey.Serg(out p1);
            }
            else if (label1.Text == "0")
            {
                Sergey.Ukr(out p1);
            }
			//p1 = "E://Иван/Рабочий стол/Primer";
			if (IvanLibrary.CreateSemanticFile.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, p1 + "/The table of semantic fragments/" + A + ".txt", Intron))
			{
				Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking(p1 + "/Text documents/" + A + ".txt", p1 + "/The table of semantic fragments/" + A + ".txt", textBox1);
				Vlad.focusirovka(IvanLibrary.FocusAfterCreateSemanticFile.FindNewElementofSFT(p1 + "/The table of semantic fragments/" + A + ".txt"), textBox1, Intron);
			}
			Vlad.b1(listBox1, p1 + "/The table of semantic fragments/" + A + ".txt");
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sergey.button2_Click(comboBox2,button2, button4, button5, button6, button7, textBox1, listBox1, button8, button9, button10, dataGridView1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vlad.lb1(listBox1, textBox1, Intron);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Vlad.tb1change(textBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
			
			Intron = Sergey.ComboBox1_SelectedIndexChanged(comboBox1,textBox1,listBox1,Intron,label1);          //Переход между документами в комбобоксе
        }

        private void удалитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Intron = Sergey.удалитьФайлToolStripMenuItem_Click(comboBox1, textBox1, listBox1, Intron,label1,comboBox2);    //Удаление текущего документа (по значению в комбобоксе)
        }

        private void добавитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();             //Открытие диалогового окна для подгрузки текста из файла
            Intron = Sergey.добавитьФайлToolStripMenuItem_Click(textBox1, comboBox1, listBox1, openFileDialog, Intron,label1);
        }

        private void удалитьВсеДокументыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sergey.удалитьВсеДокументыToolStripMenuItem_Click(comboBox1, textBox1,listBox1,comboBox2);  //Удаление всех существующих документов
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.создатьToolStripMenuItem_Click(folderBrowserDialog, toolStripStatusLabel1,label1,Intron,comboBox1,проектToolStripMenuItem,comboBox2,textBox1,listBox1);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.открытьToolStripMenuItemToolStripMenuItem_Click(folderBrowserDialog, textBox1, comboBox1, toolStripStatusLabel1,label1,проектToolStripMenuItem);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
		
		private void button3_Click(object sender, EventArgs e)
		{
            Sergey.button3_Click(button1, button2, button3, button4, button5, button6, button7, textBox1, comboBox1, comboBox2 ,Intron);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Intron = Sergey.ComboBox2_SelectedIndexChanged(comboBox2, textBox1, listBox1, Intron,label1);          //Переход между документами в комбобоксе
        }

        private void подключениеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var window = new ConnectToDBNeo4j();
			window.ShowDialog();
		}

        private void button6_Click(object sender, EventArgs e)
        {
			IvanLibrary.SelectedTextSecondForm.SelecteTextSecondWindow(textBox1, neo4j, Intron);
		}

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Intron = Sergey.ComboBox2_SelectedIndexChanged(comboBox2, textBox1, listBox1, Intron,label1);          //Переход между документами в комбобоксе
        }

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			
		}

		private void listBox1_MouseDown(object sender, MouseEventArgs e)
		{
			listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)//переименовывание СФа
		{
            string nameSF;
            int i = listBox1.SelectedIndex;
            nameSF = Interaction.InputBox("Хотите дать название смысловому фрагменту? Введите название и нажмите ОК.", "", Convert.ToString(listBox1.Items[i]));
            if (nameSF == "") { nameSF = Convert.ToString(listBox1.Items[i]); }
            string A = comboBox1.Text; //хотелось бы убрать по возможности
            //A = "Иван"; // Потом убрать, это времянка!!!!
            string p1 = "";
            if (label1.Text == "1")
            {
                Sergey.Serg(out p1);
            }
            else if (label1.Text == "0")
            {
                Sergey.Ukr(out p1);
            }
            Vlad.pereimSF(listBox1,p1 + "/The table of semantic fragments/" + A + ".txt", nameSF);
            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking(p1 + "/Text documents/" + A + ".txt", p1 + "/The table of semantic fragments/" + A + ".txt", textBox1); // потом добавить 0 в конец из-за изменений вани. Надо доработать координату фокуса при переименовывании
            Vlad.b1(listBox1, p1 + "/The table of semantic fragments/" + A + ".txt");
            Vlad.focusirovka(i, textBox1, Intron);
        }

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)//удаление СФа
		{
            string A = comboBox1.Text; //хотелось бы убрать по возможности
            // A = "Иван"; // Потом убрать, это времянка!!!!
            string p1 = "";
            if (label1.Text == "1")
            {
                Sergey.Serg(out p1);
            }
            else if (label1.Text == "0")
            {
                Sergey.Ukr(out p1);
            }
            int i = listBox1.SelectedIndex;
            i = Intron[i * 2, 1] - (Intron[i * 2, 1] - Intron[i * 2, 0]) + 1;
            Vlad.delSF(listBox1,p1 + "/The table of semantic fragments/" + A + ".txt");
            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking(p1 + "/Text documents/" + A + ".txt", p1 + "/The table of semantic fragments/" + A + ".txt", textBox1); // потом добавить 0 в конец из-за изменений вани. Надо доработать координату фокуса при удалении
            Vlad.b1(listBox1, p1 + "/The table of semantic fragments/" + A + ".txt");
            textBox1.Select(i, 0);
            textBox1.ScrollToCaret();
        }

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.A)
            {
				string A = comboBox1.Text;
				string p1 = "";
				//A = "Иван"; // Потом убрать, это времянка!!!!
				if (label1.Text == "1")
				{
					Sergey.Serg(out p1);
				}
				else if (label1.Text == "0")
				{
					Sergey.Ukr(out p1);
				}
				//p1 = "E://Иван/Рабочий стол/Primer";
				if (IvanLibrary.CreateSemanticFile.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, p1 + "/The table of semantic fragments/" + A + ".txt", Intron))
				{
					Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking(p1 + "/Text documents/" + A + ".txt", p1 + "/The table of semantic fragments/" + A + ".txt", textBox1);
					Vlad.focusirovka(IvanLibrary.FocusAfterCreateSemanticFile.FindNewElementofSFT(p1 + "/The table of semantic fragments/" + A + ".txt"), textBox1, Intron);
				}
				Vlad.b1(listBox1, p1 + "/The table of semantic fragments/" + A + ".txt");
			}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sergey.button7_Click(button1, button2, button3, button4, button5, button6, button7, textBox1, comboBox1, comboBox2, Intron);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Sergey.button10_Click(comboBox2, button2, button4, button5, button6, button7, textBox1, listBox1, button8, button9, button10, dataGridView1);
        }

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{

		}

		private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{

		}

		private void типыОтношенийToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Relationships relation = new Relationships();
			relation.Show();
		}
	}
}
