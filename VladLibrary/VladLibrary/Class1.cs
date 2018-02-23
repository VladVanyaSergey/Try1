using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VladLibrary
{
    public class Vlad
    {
        static string c0;
        static string c1;
        static string tmp;
        static string t;
        static string trez;
        static string namesf;
        static int tl;
        static int ts;
        static int i;
        static int i1;
        static int pravka;
        static List<string> rlt = new List<string>();

        public static void b1(System.Windows.Forms.ListBox listBox1, string adr)//считывание данных из txt-файла и формирование СФ-ов в Listbox
        {
            using (StreamReader rdr = new StreamReader(adr))
            {
                listBox1.Items.Clear();
                i = 0;
                i1 = 0;
                rlt.Clear();
                do
                {
                    tmp = rdr.ReadLine();
                    if (tmp == null) break;
                    rlt.Add(tmp);
                    t = Convert.ToString(rlt.ElementAt(i));
                    i = i + 1;
                    i1 = i1 + 1;
                    string[] t1 = t.Split('\t');
                    namesf = t1[0];
                    listBox1.Items.Add(namesf);
                } while (true);
            }
        }

        static public void lb1(System.Windows.Forms.ListBox listBox1, TextBox textBox1, int[,] Intron)//навигация по листбоксу
        {
            i = listBox1.SelectedIndex;
            Vlad.focusirovka(i, textBox1, Intron);
        }

        static public void focusirovka(int i, TextBox textBox1, int[,] Intron)//фокусировка
        {
            if (i == -1)
            {
                i = 0;
            }
            else
            {
                ts = Intron[i * 2, 0];
                tl = Intron[i * 2, 1] - ts - 3;
                textBox1.Select(Intron[i * 2 + 1, 1], 0);
                textBox1.ScrollToCaret();
                textBox1.Select(ts, tl);
                textBox1.ScrollToCaret();
            }
            
        }

        static public void b3(TextBox textBox1, int[,] Intron)//удаление лишнего текста   !!!доделать если интрон пустой
        {
            i = 0;
            trez = textBox1.Text;//пусть пока будет
            t = textBox1.Text;
            tl = t.Length;
            ts = Intron.Length;
            if (ts == 0)////мессенджбокс и остальная хрень связанная с исключением когда интрон пустой
            {
                textBox1.Text = "";
                MessageBox.Show("Не выделено ни одного смыслового фрагмента. Перейдите обратно в окно смысловых фрагментов.");
            }
            else
            {
                t = t.Remove(Intron[ts / 2 - 1, 1]);
                ts = ts / 4;
                for (i = ts - 1; i > 0;)
                {
                    pravka = Intron[i * 2, 0] - Intron[i * 2 - 1, 1];
                    t = t.Remove(Intron[i * 2 - 1, 1], pravka);
                    for (int k = ts; k > i;)
                    {
                        Intron[k * 2 - 2, 0] = Intron[k * 2 - 2, 0] - pravka;
                        Intron[k * 2 - 2, 1] = Intron[k * 2 - 2, 1] - pravka - 4;
                        Intron[k * 2 - 1, 0] = Intron[k * 2 - 1, 0] - pravka + 4;
                        Intron[k * 2 - 1, 1] = Intron[k * 2 - 1, 1] - pravka;
                        k = k - 1;
                    }
                    i = i - 1;
                }
                if (Intron[0, 0] != 0)
                {
                    t = t.Remove(0, Intron[0, 0]);
                    pravka = Intron[0, 0];
                    for (int k = ts; k > 0;)
                    {
                        Intron[k * 2 - 2, 0] = Intron[k * 2 - 2, 0] - pravka;
                        Intron[k * 2 - 2, 1] = Intron[k * 2 - 2, 1] - pravka - 4;
                        Intron[k * 2 - 1, 0] = Intron[k * 2 - 1, 0] - pravka + 4;
                        Intron[k * 2 - 1, 1] = Intron[k * 2 - 1, 1] - pravka;
                        k = k - 1;
                    }
                }
                textBox1.Text = t;
            }
        }

        public static void delSF(System.Windows.Forms.ListBox listBox1, string adr)//удаление СФа
        {
            if (i == listBox1.SelectedIndex)
            {
                using (StreamWriter sdelSF = new StreamWriter(adr))
                {

                    for (int k = 0; k < i1; k++)
                    {
                        if (k != i)
                        {
                            sdelSF.WriteLine(rlt.ElementAt(k));
                        }
                    }
                }
            }
        }

        public static void pereimSF(System.Windows.Forms.ListBox listBox1, string adr, string namesf)//переименовывание СФа
        {
            if (namesf == "")
            {
                MessageBox.Show("Вы ничего не ввели. Имя смыслового фрагмента не может быть пустым.");
            }
            else
            {
                if (i == listBox1.SelectedIndex)
                {
                    using (StreamWriter pereimenovSF = new StreamWriter(adr))
                    {

                        for (int k = 0; k < i1; k++)
                        {
                            if (k != i)
                            {
                                pereimenovSF.WriteLine(rlt.ElementAt(k));
                            }
                            else
                            {
                                t = Convert.ToString(rlt.ElementAt(k));
                                string[] t1 = t.Split('\t');
                                c0 = t1[1];
                                c1 = t1[2];
                                pereimenovSF.WriteLine(namesf + '\t' + t1[1] + '\t' + c1);
                            }
                        }
                    }
                }
            }
        }

        static public bool f_dla_vani(int c1, int c2, int[,] Intron) //функция определяющая затронуты ли интроны по 2-ом окне или нет. Если затронуты возвращается булевское значение try
        {
            bool f_dla_van = false;
            ts = Intron.Length;
            ts = ts / 2;
            for (i = 0; i < ts;)
            {
                if (c1 <= Intron[i, 0])
                {
                    f_dla_van = true;
                }

                if (c2 >= Intron[i, 1])
                {
                    f_dla_van = true;
                }
                i = i + 1;
            }
            return f_dla_van;
        }
        static public void tb1change(TextBox textBox1)
        {
            textBox1.RightToLeft = RightToLeft.No;
            textBox1.TextAlign = HorizontalAlignment.Left;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.ReadOnly = true;
        }
    }
}
