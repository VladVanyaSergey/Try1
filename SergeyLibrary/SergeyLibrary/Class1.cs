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
using VladLibrary;
using IvanLibrary;

namespace SergeyLibrary
{
    public class Sergey
    {
        static private Word.Application S_wordapp;
        static private Word.Document S_worddocument;
        static string S_namefiledirect;
        static string S_namefile;
        static string S_text;
        static string S_Osnova;
        static string S_Perem1;
        static string S_Dobavl;
        static string S_STD;   // переменная Список текстовых документов
        static List<string> S_nameFile = new List<string>();
        static List<string> S_textFile = new List<string>();

        public static void добавитьФайлToolStripMenuItem_Click(System.Windows.Forms.TextBox textBox, System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.OpenFileDialog openFileDialog1)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)                           //Открытие диалогового окна для открытия файла
            {
                S_namefiledirect = openFileDialog1.FileName;                               //Сохранение в переменную пути к выбранному фалу
                S_namefile = openFileDialog1.SafeFileName;                                 //Сохранение в переменную имени выбранного файла
                S_namefile = S_namefile.Remove(S_namefile.IndexOf(".docx"));
                S_wordapp = new Word.Application();                                        //Создаем объект Word - равносильно запуску Word.
                S_wordapp.Visible = false;
                Object filename = S_namefiledirect;
                S_worddocument = S_wordapp.Documents.Open(ref filename);                   //Открываем конкретный существующий word документ из нужной директории.
                Object begin = Type.Missing;                                               //В документе определяем диапазон,вызовом метода Range  
                Object end = Type.Missing;                                                 //с передачей ему начального 
                Word.Range wordrange = S_worddocument.Range(ref begin, ref end);           //и конечного значений позиций символов.
                wordrange.Copy();                                                          //Копирование в буфер обмена.
                S_text = Clipboard.GetText();                                              //Сохранение в переменную скопированного текста
                if (S_Osnova != S_STD)
                {
                    System.IO.File.WriteAllText((S_STD + "/" + S_namefile + ".txt"), S_text); //Создание текстового файла txt и запись в него текста из буфера обмена
                    System.IO.File.WriteAllText((S_Osnova + "/" + "The table of semantic fragments" + "/" + S_namefile + ".txt"), ""); //Создание текстового файла для таблиц смысловых фрагментов
                }
                if (S_Osnova == S_STD)
                {
                    System.IO.File.WriteAllText((S_STD + "/" + S_namefile + ".txt"), S_text); //Создание текстового файла txt и запись в него текста из буфера обмена
                    System.IO.File.WriteAllText((S_Dobavl + "/" + "The table of semantic fragments" + "/" + S_namefile + ".txt"), ""); //Создание текстового файла для таблиц смысловых фрагментов

                }
                textBox.Text = S_text;                                                                                //Извлекаем из буфера обмена копированный текст.
                comboBox.Items.Add(S_namefile);                                                                       //Добавление в combobox1 имени файла, с которого мы скопировали текст 
                comboBox.Text = S_namefile;                                                                           //Надпись комбобокса меняется на имя файла
                S_nameFile.Add(S_namefile);                                                                           //Добавление в список имени файла
                S_textFile.Add(S_text);                                                                               //Добавление в список текста файла
                Object saveChanges = Word.WdSaveOptions.wdPromptToSaveChanges;
                Object originalFormat = Word.WdOriginalFormat.wdWordDocument;
                Object routeDocument = Type.Missing;
                S_wordapp.Quit(ref saveChanges, ref originalFormat, ref routeDocument);    // Закрытие файла
                S_wordapp = null;
            }
        }


        public static int[,] ComboBox1_SelectedIndexChanged(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox, System.Windows.Forms.ListBox listBox, int[,] Intron)
        {
            for (int i = 0; i < S_nameFile.Count; i++)
            {
                if (comboBox.Text == S_nameFile[i])
                {
                    textBox.Text = S_textFile[i];
                    Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + S_nameFile[i] + ".txt", "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt", textBox, 0);
                    Vlad.b1(listBox, "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt");
                }
            }
            return Intron;
        }

        public static int[,] удалитьФайлToolStripMenuItem_Click(System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.TextBox textBox, System.Windows.Forms.ListBox listBox, int[,] Intron)
        {
            if (MessageBox.Show("Удалить текущий документы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int i = 0; i < S_nameFile.Count; i++)
                {
                    if (comboBox.Text == S_nameFile[i])
                    {
                        comboBox.Items.Remove(S_nameFile[i]);
                        S_Perem1 = S_nameFile[i];
                        S_nameFile.RemoveAt(i);
                        S_textFile.RemoveAt(i);
                        if (i == 0 && S_nameFile.Count != 0)
                        {
                            comboBox.Text = S_nameFile[i];
                            textBox.Text = S_textFile[i];
                            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + S_nameFile[i] + ".txt", "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt", textBox, 0); //вставка
                            Vlad.b1(listBox, "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt");//вставка
                            FileInfo fileInf = new FileInfo(S_STD + "/" + S_Perem1 + ".txt");
                            if (fileInf.Exists)
                            {
                                fileInf.Delete();
                            }
                            if (S_Osnova == S_STD)   //удаление таблици смысловых фрагментов у вновь созданного проекта
                            {
                                FileInfo file = new FileInfo(S_Dobavl + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                            if (S_Osnova != S_STD)    //удаление таблици смысловых фрагментов у существующего открытого
                            {
                                FileInfo file = new FileInfo(S_Osnova + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                        }
                        if (i != 0 && S_nameFile.Count != 0)
                        {
                            comboBox.Text = S_nameFile[i - 1];
                            textBox.Text = S_textFile[i - 1];
                            Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + S_nameFile[i - 1] + ".txt", "Primer/The table of semantic fragments/" + S_nameFile[i - 1] + ".txt", textBox, 0); //вставка
                            Vlad.b1(listBox, "Primer/The table of semantic fragments/" + S_nameFile[i - 1] + ".txt");//вставка
                            FileInfo fileInf = new FileInfo(S_STD + "/" + S_Perem1 + ".txt");
                            if (fileInf.Exists)
                            {
                                fileInf.Delete();
                            }
                            if (S_Osnova == S_STD)   //удаление таблици смысловых фрагментов у вновь созданного проекта
                            {
                                FileInfo file = new FileInfo(S_Dobavl + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                            if (S_Osnova != S_STD)    //удаление таблици смысловых фрагментов у существующего открытого
                            {
                                FileInfo file = new FileInfo(S_Osnova + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                        }
                        if (i == 0 && S_nameFile.Count == 0)
                        {
                            comboBox.Items.Clear();
                            comboBox.Text = "Файл";
                            S_nameFile.Clear();
                            S_textFile.Clear();
                            textBox.Text = "";
                            FileInfo fileInf = new FileInfo(S_STD + "/" + S_Perem1 + ".txt");
                            if (fileInf.Exists)
                            {
                                fileInf.Delete();
                            }
                            if (S_Osnova == S_STD)   //удаление таблици смысловых фрагментов у вновь созданного проекта
                            {
                                FileInfo file = new FileInfo(S_Dobavl + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                            if (S_Osnova != S_STD)    //удаление таблици смысловых фрагментов у существующего открытого проекта
                            {
                                FileInfo file = new FileInfo(S_Osnova + "/" + "The table of semantic fragments" + "/" + S_Perem1 + ".txt");
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                        }
                    }
                }
            }
            return Intron;
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
                if (S_Osnova == S_STD) //удаление всего из вновь созданного файла
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(S_Dobavl + "/" + "Text documents");
                        dirInfo.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(S_Dobavl + "/" + "The table of semantic fragments");
                        dirInfo.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Directory.CreateDirectory(S_Dobavl + "/" + "The table of semantic fragments");      //Создание папки таблици смысловых фрагментов
                    Directory.CreateDirectory(S_Dobavl + "/" + "Text documents");       //Создание папки список текстовых документов
                }
                if (S_Osnova != S_STD) //удаление всего из открытого существующего файла 
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(S_Osnova + "/" + "Text documents");
                        dirInfo.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(S_Osnova + "/" + "The table of semantic fragments");
                        dirInfo.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Directory.CreateDirectory(S_Dobavl + "/" + "The table of semantic fragments");      //Создание папки таблици смысловых фрагментов
                    Directory.CreateDirectory(S_Dobavl + "/" + "Text documents");       //Создание папки список текстовых документов
                }
            }
        }
        public static void создатьToolStripMenuItem_Click(System.Windows.Forms.FolderBrowserDialog folderBrowserDialog, System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                S_Osnova = folderBrowserDialog.SelectedPath;                                     //запоминаем директорию проекта    
                DirectoryInfo dirInfo = new DirectoryInfo(S_Osnova);                             //Создаем папку проекта
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                Directory.CreateDirectory(S_Osnova + "/" + "The table of semantic fragments");      //Создание папки таблици смысловых фрагментов
                Directory.CreateDirectory(S_Osnova + "/" + "Text documents");       //Создание папки список текстовых документов
                S_Dobavl = S_Osnova;
                S_Osnova = (folderBrowserDialog.SelectedPath + "/" + "Text documents");
                S_STD = S_Osnova;
            }
            toolStripStatusLabel.Text = "Проект создан"; //изменение в строке состояний
        }

        public static void открытьToolStripMenuItemToolStripMenuItem_Click(System.Windows.Forms.FolderBrowserDialog folderBrowserDialog, System.Windows.Forms.TextBox textBox, System.Windows.Forms.ComboBox comboBox, System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)                           //диалоговое окно для выбора папки
            {
                S_Osnova = folderBrowserDialog.SelectedPath;
                S_STD = (folderBrowserDialog.SelectedPath + "/" + "Text documents");  //директория, в которой содержатся текстовые документы
                string[] fileEntries = Directory.GetFiles(S_STD);                             //в массив записываем имена файлов из директории
                for (int i = 0; i < fileEntries.Length; i++)                                   //создание цикла на проход по массиву
                {
                    S_namefiledirect = fileEntries[i];                                         //сохранение директорий файлов в переменную
                    FileInfo fileInf = new FileInfo(fileEntries[i]);
                    S_namefile = fileInf.Name;                                                 //сохранение имени файла с расширением в переменную
                    S_namefile = S_namefile.Remove(S_namefile.IndexOf(".txt"));                //удаление расширения из названия файла
                    Clipboard.SetText(System.IO.File.ReadAllText(S_namefiledirect = fileEntries[i]));
                    S_text = Clipboard.GetText();                                              //Сохранение в переменную скопированного текста
                    textBox.Text = S_text;                                                     //Извлекаем из буфера обмена копированный текст.
                    S_textFile.Add(S_text);                                                    //Добавление в список текстов файлов
                    S_nameFile.Add(S_namefile);                                                //Добавление в список названий файлов
                    comboBox.Items.Add(S_namefile);                                            //Добавление в combobox1 имени файла, с которого мы скопировали текст 
                    comboBox.Text = S_namefile;                                                //Надпись комбобокса меняется на имя файла
                }
            }
            toolStripStatusLabel.Text = "Проект открыт"; //изменение в строке состояний
        }
        
        public static void button2_Click(System.Windows.Forms.ComboBox comboBox)
        {
            FileInfo fileInf = new FileInfo(S_Osnova + "/" + "The table of semantic fragments" + "/" + comboBox.Text + ".txt");
            if (fileInf.Exists)
            {
                //Обновление таблици
            }
            else
            {
                if (S_Osnova != S_STD) //При открытии существующего проекта
                {
                    System.IO.File.WriteAllText((S_Osnova + "/" + "The table of semantic fragments" + "/" + comboBox.Text + ".txt"), ""); //Создание текстового файла для таблиц смысловых фрагментов
                }
                if (S_Osnova == S_STD) //При создании нового проекта
                {
                    System.IO.File.WriteAllText((S_Dobavl + "/" + "The table of semantic fragments" + "/" + comboBox.Text + ".txt"), ""); //Создание текстового файла для таблиц смысловых фрагментов
                }
            }
            //если файла нет, то создаем файл
            // System.IO.File.WriteAllText((S_Osnova + "/" + "Таблици смысловых фрагментов" +"/" + comboBox.Text + ".txt"), ""); //Создание текстового файла для таблиц смысловых фрагментов
            //еслий файл есть, то пишем меседж бокс, о том, что таблица смысловых фрагментов для данного файла создана
        }

        public static void button3_Click(System.Windows.Forms.Button button1, System.Windows.Forms.Button button3, System.Windows.Forms.Button button4, System.Windows.Forms.Button button5, System.Windows.Forms.Button button6, System.Windows.Forms.Button button7, System.Windows.Forms.TextBox textBox, System.Windows.Forms.ComboBox comboBox1, System.Windows.Forms.ComboBox comboBox2, int[,] Intron)
        {
            button1.Visible = false; //кнопки окна СФ
            button3.Visible = false;
            button4.Visible = false;
            comboBox1.Visible = false;
            button5.Visible = true; //кнопки окна Понятий
            button6.Visible = true;
            button7.Visible = true;
            comboBox2.Visible = true;
            for (int i = 0; i < S_nameFile.Count; i++)
            {
                comboBox2.Items.Add(S_nameFile[i]);
                comboBox2.Text = comboBox1.Text;
            }

            Vlad.b3(textBox, Intron);
        }

        public static int[,] ComboBox2_SelectedIndexChanged(System.Windows.Forms.ComboBox comboBox2, System.Windows.Forms.TextBox textBox, System.Windows.Forms.ListBox listBox, int[,] Intron)
        {
            for (int i = 0; i < S_nameFile.Count; i++)
            {
                if (comboBox2.Text == S_nameFile[i])
                {
                    textBox.Text = S_textFile[i];
                    Intron = IvanLibrary.GiveMeBlockStructureWithRemainElements.StartWorking("Primer/Text documents/" + S_nameFile[i] + ".txt", "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt", textBox, 0);
                    Vlad.b1(listBox, "Primer/The table of semantic fragments/" + S_nameFile[i] + ".txt");
                }
            }
            return Intron;
        }
    }
}
