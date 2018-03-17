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
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
	public partial class Relationships : Form
	{
		public Relationships()
		{
			InitializeComponent();
			WindowsFormsApp1.Form1.neo4j.ConnectToDataBase("http://localhost:7474/db/data", "neo4j", "1234");
			for (int i = 0; i < WindowsFormsApp1.Form1.neo4j.TypesRelationships.Count; i++){listBox1.Items.Add(WindowsFormsApp1.Form1.neo4j.TypesRelationships[i]);}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var newRelation = Interaction.InputBox("Введите название нового типа отношений", "");
			//WindowsFormsApp1.Form1
		}
	}
}
