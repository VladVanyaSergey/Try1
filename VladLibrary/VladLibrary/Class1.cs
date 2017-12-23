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
        static string sf = "СФ";
        static int sfchi = 0;
        static int tl;
        static int ts;
        static int i;
        //(@"C:\Users\Влад\Desktop\Влад сем9\Нетрогать_Влад\СФы.txt")
        static List<string> rlt = new List<string>();

        public static void b1(System.Windows.Forms.ListBox listBox1, string adr)
        {
            listBox1.Items.Clear();
            sfchi = 0;
            using (StreamReader rdr = new StreamReader(adr))
            {
                listBox1.Items.Clear();
                sfchi = 0;
                do
                {
                    tmp = rdr.ReadLine();
                    if (tmp == null) break;
                    rlt.Add(tmp);
                    sfchi = sfchi + 1;
                    listBox1.Items.Add(sf + Convert.ToString(sfchi));
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
        }
    }
}
