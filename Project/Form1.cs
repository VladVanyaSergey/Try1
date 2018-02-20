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
			// Для Сережи! При запуске и подгрузки страницы должен запускаться вот этот фрагемнт!!!
			string A = "Иван"; // Потом убрать, это времянка!!!!
			Intron=IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1,0);
			//Эксперимент
		}

		private void button1_Click(object sender, EventArgs e)
        {

            string A = comboBox1.Text;
			A = "Иван"; // Потом убрать, это времянка!!!!
			int[] outdata=IvanLibrary.CreateSemanticFile.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, "Primer/The table of semantic fragments/" + A + ".txt", Intron);
			if (outdata[0] != -1)
			{
				Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1, outdata[0]);
			}
			Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");

		}

        private void button4_Click(object sender, EventArgs e)// эту кнопку можно убрать
        {
            //string A = comboBox1.Text; //хотелось бы убрать по возможности
            //A = "Иван"; // Потом убрать, это времянка!!!!
            //Vlad.b4(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
            //Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1,0); // потом добавить 0 в конец из-за изменений вани
            //Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
			
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
			
			Intron = Sergey.ComboBox1_SelectedIndexChanged(comboBox1,textBox1,listBox1,Intron);          //Переход между документами в комбобоксе
        }

        private void удалитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Intron = Sergey.удалитьФайлToolStripMenuItem_Click(comboBox1, textBox1, listBox1, Intron);    //Удаление текущего документа (по значению в комбобоксе)
        }

        private void добавитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();             //Открытие диалогового окна для подгрузки текста из файла
            Intron = Sergey.добавитьФайлToolStripMenuItem_Click(textBox1, comboBox1, listBox1, openFileDialog, Intron);
        }

        private void удалитьВсеДокументыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sergey.удалитьВсеДокументыToolStripMenuItem_Click(comboBox1, textBox1);  //Удаление всех существующих документов
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.создатьToolStripMenuItem_Click(folderBrowserDialog, toolStripStatusLabel1);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.открытьToolStripMenuItemToolStripMenuItem_Click(folderBrowserDialog, textBox1, comboBox1, toolStripStatusLabel1);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
		
		private void button3_Click(object sender, EventArgs e)
		{
            Sergey.button3_Click(button1, button3, button4, button5, button6, button7, textBox1, comboBox1, comboBox2 ,Intron);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Intron = Sergey.ComboBox2_SelectedIndexChanged(comboBox2, textBox1, listBox1, Intron);          //Переход между документами в комбобоксе
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
            Intron = Sergey.ComboBox2_SelectedIndexChanged(comboBox2, textBox1, listBox1, Intron);          //Переход между документами в комбобоксе
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
            nameSF = Interaction.InputBox("Хотите дать название смысловому фрагменту? Введите название и нажмите ОК.");

            string A = comboBox1.Text; //хотелось бы убрать по возможности
            A = "Иван"; // Потом убрать, это времянка!!!!
            int i= listBox1.SelectedIndex;
            Vlad.pereimSF(listBox1, "Primer/The table of semantic fragments/" + A + ".txt", nameSF);
            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1, 0); // потом добавить 0 в конец из-за изменений вани. Надо доработать координату фокуса при переименовывании
            Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
            Vlad.focusirovka(i, textBox1, Intron);
        }

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)//удаление СФа
		{
            string A = comboBox1.Text; //хотелось бы убрать по возможности
            A = "Иван"; // Потом убрать, это времянка!!!!
            int i = listBox1.SelectedIndex;
            i = Intron[i * 2, 1] - (Intron[i * 2, 1] - Intron[i * 2, 0]) + 1;
            Vlad.delSF(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1, 0); // потом добавить 0 в конец из-за изменений вани. Надо доработать координату фокуса при удалении
            Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
            textBox1.Select(i, 0);
            textBox1.ScrollToCaret();
        }

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A)
			{
				string A = comboBox1.Text;
				A = "Иван"; // Потом убрать, это времянка!!!!
				int[] outdata = IvanLibrary.CreateSemanticFile.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, "Primer/The table of semantic fragments/" + A + ".txt", Intron);
				if (outdata[0] != -1)
				{
					Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1, outdata[0]);
				}
				Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");
			}
		}

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
