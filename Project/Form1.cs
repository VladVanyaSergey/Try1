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
		//
		public Form1()
        {
            InitializeComponent();
			// Для Сережи! При запуске и подгрузки страницы должен запускаться вот этот фрагемнт!!!
			string A = "Иван"; // Потом убрать, это времянка!!!!
			Intron=IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1);
			//Эксперимент
			IvanLibrary.Neo4j.start();
		}

		private void button1_Click(object sender, EventArgs e)
        {

            string A = comboBox1.Text;
			A = "Иван"; // Потом убрать, это времянка!!!!
			IvanLibrary.CreateSemanticFile.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, "Primer/The table of semantic fragments/" + A + ".txt", Intron);
			Intron=IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + A + ".txt", "Primer/The table of semantic fragments/" + A + ".txt", textBox1);
			//Vlad.b1(listBox1, "Primer/The table of semantic fragments/" + A + ".txt");

		}

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vlad.lb1(listBox1, textBox1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sergey.ComboBox1_SelectedIndexChanged(comboBox1,textBox1);          //Переход между документами в комбобоксе
        }

        private void удалитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sergey.удалитьФайлToolStripMenuItem_Click(comboBox1, textBox1);    //Удаление текущего документа (по значению в комбобоксе)
        }

        private void добавитьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();             //Открытие диалогового окна для подгрузки текста из файла
            Sergey.добавитьФайлToolStripMenuItem_Click(textBox1, comboBox1, openFileDialog);
        }

        private void удалитьВсеДокументыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sergey.удалитьВсеДокументыToolStripMenuItem_Click(comboBox1, textBox1);  //Удаление всех существующих документов
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.создатьToolStripMenuItem_Click(folderBrowserDialog);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            Sergey.открытьToolStripMenuItemToolStripMenuItem_Click(folderBrowserDialog, textBox1, comboBox1);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void button3_Click(object sender, EventArgs e)
		{

		}
	}
}
