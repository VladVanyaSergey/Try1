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
		public static IvanLibrary.CreateSemanticFile CSF = new CreateSemanticFile();
		Rewriting rewriting = new Rewriting();
		//
		public Form1()
        {
            InitializeComponent();
            textBox1.Text = "111Волнения, случившиеся за полтора месяца до окончания Великой Отечественной войны в 34-й запасной стрелковой дивизии, дислоцировавшейся в Бобруйске, спровоцировали новобранцы, призванные из Западной Белоруссии. Они не хотели служить в РККА и стремились попасть в польскую армию, надеясь на то, что после окончания Второй Мировой их малая родина отойдет к Польше, а не к СССР.";
        }

		private void button1_Click(object sender, EventArgs e)
        {
			CSF.SelectedTextIntoIndexForSemanticFragmentTable(textBox1, "../../../time.txt", rewriting);
			Vlad.b1(listBox1, "../../../time.txt");
		}

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sergey.button2_Click(comboBox1);
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
    }
}
