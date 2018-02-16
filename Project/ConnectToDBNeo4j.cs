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
	public partial class ConnectToDBNeo4j : Form
	{
		public ConnectToDBNeo4j()
		{
			InitializeComponent();
			richTextBox1.Text = Form1.neo4j.adress;
			richTextBox2.Text = Form1.neo4j.username;
			richTextBox3.Text = Form1.neo4j.password;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void tabPage1_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (Form1.neo4j.ConnectToDataBase(richTextBox1.Text, richTextBox2.Text, richTextBox3.Text))
			{
				this.Close();
			}
			
		}
	}
}
