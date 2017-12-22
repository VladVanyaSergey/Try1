using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace SergeyLibrary
{
    public class Sergey
    {
        static private Word.Application S_wordapp;
        static private Word.Document S_worddocument;
        static string S_namefiledirect;
        static string S_namefile;
        static string S_text;
        static List<string> S_nameFile = new List<string>();
        static List<string> S_textFile = new List<string>();

        public static void добавитьФайлToolStripMenuItem_Click(System.Windows.Forms.TextBox textBox, System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.OpenFileDialog openFileDialog1)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)                           //Открытие диалогового окна для открытия файла
            {
                S_namefiledirect = openFileDialog1.FileName;                               //Сохранение в переменную пути к выбранному фалу
                S_namefile = openFileDialog1.SafeFileName;                                 //Сохранение в переменную имени выбранного файла
                S_wordapp = new Word.Application();                                        //Создаем объект Word - равносильно запуску Word.
                S_wordapp.Visible = false;
                Object filename = S_namefiledirect;
                S_worddocument = S_wordapp.Documents.Open(ref filename);                   //Открываем конкретный существующий word документ из нужной директории.
                Object begin = Type.Missing;                                               //В документе определяем диапазон,вызовом метода Range  
                Object end = Type.Missing;                                                 //с передачей ему начального 
                Word.Range wordrange = S_worddocument.Range(ref begin, ref end);           //и конечного значений позиций символов.
                wordrange.Copy();                                                          //Копирование в буфер обмена.
                S_text = Clipboard.GetText();                                              //Сохранение в переменную скопированного текста
                textBox.Text = S_text;                                                    //Извлекаем из буфера обмена копированный текст.
                comboBox.Items.Add(S_namefile);                                           //Добавление в combobox1 имени файла, с которого мы скопировали текст 
                comboBox.Text = S_namefile;                                               //Надпись комбобокса меняется на имя файла
                S_nameFile.Add(S_namefile);                                                //Добавление в список имени файла
                S_textFile.Add(S_text);                                                    //Добавление в список текста файла
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                S_wordapp.Quit(ref saveChanges, ref originalFormat, ref routeDocument);    // Закрытие файла
                S_wordapp = null;
            }
        }


        public static void ComboBox1_SelectedIndexChanged(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox)
        {
            for (int i = 0; i < S_nameFile.Count; i++)
            {
                if (comboBox.Text == S_nameFile[i])
                {
                    textBox.Text = S_textFile[i];
                }
            }
        }

        public static void удалитьФайлToolStripMenuItem_Click(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox)
        {
            for (int i = 0; i < S_nameFile.Count; i++)
            {
                if (MessageBox.Show("Удалить текущий документы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    if (comboBox.Text == S_nameFile[i])
                    {
                        comboBox.Items.Remove(S_nameFile[i]);
                        S_nameFile.RemoveAt(i);
                        S_textFile.RemoveAt(i);
                        comboBox.Text = "Файл";
                        textBox.Text = "";
                    }
            }
        }
        public static void удалитьВсеДокументыToolStripMenuItem_Click(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox)
        {
            if (MessageBox.Show("Удалить все существующие документы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                comboBox.Items.Clear();
                comboBox.Text = "Файл";
                S_nameFile.Clear();
                S_textFile.Clear();
                textBox.Text = "";
            }
        }
    }
}
