using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvanLibrary;

namespace WindowsFormsApp1
{
	public partial class Rewriting : Form
	{
		public Rewriting()
		{
			InitializeComponent();
			IvanLibrary.RewritingClass.StartWork(listBox1, Form1.CSF.index, Form1.CSF.ProblemElements);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
