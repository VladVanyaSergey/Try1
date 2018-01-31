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
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void Rewriting_Load(object sender, EventArgs e)
		{
			IvanLibrary.RewritingClass.StartWork(textBox1, richTextBox1, listBox1, Form1.CSF.index, Form1.CSF.ProblemElements, Form1.CSF.ProblemText);
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			IvanLibrary.RewritingClass.SelectingText(textBox1, richTextBox1, listBox1, Form1.CSF.index, Form1.CSF.ProblemElements);
		}
	}
}
