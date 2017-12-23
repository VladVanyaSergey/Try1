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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "1111qwertyuiop[]asdfghjklzxcvbnm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] x = new int[2];
            //x = Ivan.SelectedTextIntoIndexForSemanticFragmentTable(textBox1);
            //Ivan.AddIndexIntoSemanticFragmentTable("C://Users/ivan_/Desktop/time.txt");
            //Vlad.b1(listBox1);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Vlad.lb1(listBox1, textBox1);
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
    }
}