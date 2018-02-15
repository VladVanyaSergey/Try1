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
        static string tmp;
        static string t;
        static string trez;
        static int tl;
        static int ts;
        static int i;
        static string namesf;
        static List<string> rlt = new List<string>();

        public static void b1(System.Windows.Forms.ListBox listBox1, string adr)
        {
            using (StreamReader rdr = new StreamReader(adr))
            {
                listBox1.Items.Clear();
                i = 0;
                rlt.Clear();
                do
                {
                    tmp = rdr.ReadLine();
                    if (tmp == null) break;
                    rlt.Add(tmp);
                    t = Convert.ToString(rlt.ElementAt(i));
                    i = i + 1;
                    string[] t1 = t.Split('\t');
                    namesf = t1[0];
                    listBox1.Items.Add(namesf);
                } while (true);
            }
        }

        static public void lb1(System.Windows.Forms.ListBox listBox1, TextBox textBox1, int[,] Intron)
        {
            i = listBox1.SelectedIndex;
            ts = Intron[i * 2, 0];
            tl = Intron[i * 2, 1] - ts-3;
            textBox1.Select(ts, tl);
            textBox1.ScrollToCaret();
        }

        static public void b3(TextBox textBox1, int[,] Intron)//удаление лишнего текста
        {
            i = 0;
            trez = textBox1.Text;//пусть пока будет
            t = textBox1.Text;
            tl = t.Length;
            ts = Intron.Length;
            t = t.Remove(Intron[ts / 2 - 1, 1]);
            ts = ts / 4;
            for (i = ts - 1; i > 0;)
            {
                t = t.Remove(Intron[i * 2 - 1, 1], Intron[i * 2, 0] - Intron[i * 2 - 1, 1]);
                i = i - 1;
            }
            if (Intron[0, 0] != 0)
            {
                t = t.Remove(0, Intron[0, 0] + 3);
            }
            textBox1.Text = t;
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
