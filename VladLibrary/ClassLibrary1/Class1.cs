using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ClassLibrary1
{
    public class Vlad
    {
            string tmp;
            string t;
            string sf = "СФ";
            int sfchi = 0;
            int tl;
            int ts;
            int i;
            //(@"C:\Users\Влад\Desktop\Влад сем9\Нетрогать_Влад\СФы.txt")
            List<string> rlt = new List<string>();
            
            //fhhjb
 
            public void b1(System.Windows.Forms.ListBox listBox1)
            {
                using (StreamReader rdr = new StreamReader(@"C:\Users\Влад\Desktop\Влад сем9\Нетрогать_Влад\СФы.txt"))
                {
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

            private void lb1(System.Windows.Forms.ListBox listBox1,TextBox textBox1)
            {
                i = listBox1.SelectedIndex;
                t = Convert.ToString(rlt.ElementAt(i));
                string[] t1 = t.Split('\t');
                ts = Convert.ToInt32(t1[1]);
                tl = Convert.ToInt32(t1[2]);
                textBox1.Select(ts, tl);
            }
     }

}
