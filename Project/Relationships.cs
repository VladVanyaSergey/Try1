﻿using System;
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
			for (int i = 0; i < WindowsFormsApp1.Form1.neo4j.TypesRelationships.Count; i++) { listBox1.Items.Add(WindowsFormsApp1.Form1.neo4j.TypesRelationships[i]); }
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var newRelation = Interaction.InputBox("Введите название нового типа отношений", "");
			if (!WindowsFormsApp1.Form1.neo4j.TypesRelationships.Contains(newRelation))
			{
				try
				{
					WindowsFormsApp1.Form1.neo4j.CreateRelationships(newRelation);
					WindowsFormsApp1.Form1.neo4j.TypesRelationships.Add(newRelation);
					WindowsFormsApp1.Form1.neo4j.TypesRelationships.Sort();
					listBox1.Items.Clear();
					for (int i = 0; i < WindowsFormsApp1.Form1.neo4j.TypesRelationships.Count; i++)
					{
						listBox1.Items.Add(WindowsFormsApp1.Form1.neo4j.TypesRelationships[i]);
					}
				}
				catch (Exception error)
				{
					MessageBox.Show(error.ToString());
				}
			}
			else
			{
				MessageBox.Show("Такой тип отношений существует");
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1)
			{
				WindowsFormsApp1.Form1.neo4j.DeleteRelationship(listBox1.SelectedItem.ToString());
				WindowsFormsApp1.Form1.neo4j.TypesRelationships.Remove(listBox1.SelectedItem.ToString());
				WindowsFormsApp1.Form1.neo4j.TypesRelationships.Sort();
				listBox1.Items.Clear();
				for (int i = 0; i < WindowsFormsApp1.Form1.neo4j.TypesRelationships.Count; i++)
				{
					listBox1.Items.Add(WindowsFormsApp1.Form1.neo4j.TypesRelationships[i]);
				}
			}
			else
			{
				MessageBox.Show("Выберите тип отношений");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex != -1)
			{
				var newRelation = Interaction.InputBox("Введите новое название выбранного типа отношений", "");
				WindowsFormsApp1.Form1.neo4j.RenameRelationship(listBox1.SelectedItem.ToString(), newRelation);
				WindowsFormsApp1.Form1.neo4j.TypesRelationships.Remove(listBox1.SelectedItem.ToString());
				WindowsFormsApp1.Form1.neo4j.TypesRelationships.Add(newRelation);
				WindowsFormsApp1.Form1.neo4j.TypesRelationships.Sort();
				listBox1.Items.Clear();
				for (int i = 0; i < WindowsFormsApp1.Form1.neo4j.TypesRelationships.Count; i++)
				{
					listBox1.Items.Add(WindowsFormsApp1.Form1.neo4j.TypesRelationships[i]);
				}
			}
			else
			{
				MessageBox.Show("Выберите тип отношений");
			}
		}
	}
}
