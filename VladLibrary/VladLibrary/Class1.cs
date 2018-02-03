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

        static public void lb1(System.Windows.Forms.ListBox listBox1, TextBox textBox1)
        {
            i = listBox1.SelectedIndex;
            t = Convert.ToString(rlt.ElementAt(i));
            string[] t1 = t.Split('\t');
            ts = Convert.ToInt32(t1[1]);
            tl = Convert.ToInt32(t1[2]) - ts + 1;
            textBox1.Select(ts, tl);
            textBox1.ScrollToCaret();
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
