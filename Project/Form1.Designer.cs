﻿namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.проектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.добавитьДокументToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьДокументToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьВсеДокументыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.neo4jToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.подключениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(219, 43);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(178, 28);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 20;
			this.listBox1.Location = new System.Drawing.Point(20, 108);
			this.listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(181, 584);
			this.listBox1.TabIndex = 1;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(408, 40);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(171, 35);
			this.button1.TabIndex = 2;
			this.button1.Text = "Добавить СФ";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(606, 43);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(132, 58);
			this.button2.TabIndex = 3;
			this.button2.Text = "Хз зачем эта кнопка";
			this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(774, 43);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(130, 58);
			this.button3.TabIndex = 4;
			this.button3.Text = "Переход в окно понятий";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox1
			// 
			this.textBox1.HideSelection = false;
			this.textBox1.Location = new System.Drawing.Point(222, 111);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.textBox1.Size = new System.Drawing.Size(678, 581);
			this.textBox1.TabIndex = 5;
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(20, 43);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(183, 35);
			this.button4.TabIndex = 6;
			this.button4.Text = "Удалить СФ";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.проектToolStripMenuItem,
            this.neo4jToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
			this.menuStrip1.Size = new System.Drawing.Size(920, 35);
			this.menuStrip1.TabIndex = 7;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// создатьToolStripMenuItem
			// 
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
			this.создатьToolStripMenuItem.Text = "Создать";
			this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
			// 
			// открытьToolStripMenuItem
			// 
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
			// 
			// сохранитьКакToolStripMenuItem
			// 
			this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
			this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
			this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
			this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
			// 
			// проектToolStripMenuItem
			// 
			this.проектToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьДокументToolStripMenuItem,
            this.удалитьДокументToolStripMenuItem,
            this.удалитьВсеДокументыToolStripMenuItem});
			this.проектToolStripMenuItem.Name = "проектToolStripMenuItem";
			this.проектToolStripMenuItem.Size = new System.Drawing.Size(84, 29);
			this.проектToolStripMenuItem.Text = "Проект";
			// 
			// добавитьДокументToolStripMenuItem
			// 
			this.добавитьДокументToolStripMenuItem.Name = "добавитьДокументToolStripMenuItem";
			this.добавитьДокументToolStripMenuItem.Size = new System.Drawing.Size(288, 30);
			this.добавитьДокументToolStripMenuItem.Text = "Добавить документ";
			this.добавитьДокументToolStripMenuItem.Click += new System.EventHandler(this.добавитьДокументToolStripMenuItem_Click);
			// 
			// удалитьДокументToolStripMenuItem
			// 
			this.удалитьДокументToolStripMenuItem.Name = "удалитьДокументToolStripMenuItem";
			this.удалитьДокументToolStripMenuItem.Size = new System.Drawing.Size(288, 30);
			this.удалитьДокументToolStripMenuItem.Text = "Удалить документ";
			this.удалитьДокументToolStripMenuItem.Click += new System.EventHandler(this.удалитьДокументToolStripMenuItem_Click);
			// 
			// удалитьВсеДокументыToolStripMenuItem
			// 
			this.удалитьВсеДокументыToolStripMenuItem.Name = "удалитьВсеДокументыToolStripMenuItem";
			this.удалитьВсеДокументыToolStripMenuItem.Size = new System.Drawing.Size(288, 30);
			this.удалитьВсеДокументыToolStripMenuItem.Text = "Удалить все документы";
			this.удалитьВсеДокументыToolStripMenuItem.Click += new System.EventHandler(this.удалитьВсеДокументыToolStripMenuItem_Click_1);
			// 
			// neo4jToolStripMenuItem
			// 
			this.neo4jToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подключениеToolStripMenuItem});
			this.neo4jToolStripMenuItem.Name = "neo4jToolStripMenuItem";
			this.neo4jToolStripMenuItem.Size = new System.Drawing.Size(71, 29);
			this.neo4jToolStripMenuItem.Text = "Neo4j";
			// 
			// подключениеToolStripMenuItem
			// 
			this.подключениеToolStripMenuItem.Name = "подключениеToolStripMenuItem";
			this.подключениеToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
			this.подключениеToolStripMenuItem.Text = "Подключение";
			this.подключениеToolStripMenuItem.Click += new System.EventHandler(this.подключениеToolStripMenuItem_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(20, 43);
			this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(183, 35);
			this.button5.TabIndex = 8;
			this.button5.Text = "Удалить понятие";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Visible = false;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(408, 40);
			this.button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(171, 35);
			this.button6.TabIndex = 9;
			this.button6.Text = "Добавить понятие";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Visible = false;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(774, 43);
			this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(130, 58);
			this.button7.TabIndex = 10;
			this.button7.Text = "Переход в окно СФ";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Visible = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 710);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip1.Size = new System.Drawing.Size(920, 30);
			this.statusStrip1.TabIndex = 11;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 25);
			this.toolStripStatusLabel1.Text = "Готово";
			// 
			// comboBox2
			// 
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(219, 43);
			this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(178, 28);
			this.comboBox2.TabIndex = 12;
			this.comboBox2.Visible = false;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged_1);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.удалитьToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(215, 64);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(214, 30);
			this.toolStripMenuItem1.Text = "Переименовать";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(214, 30);
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(920, 740);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form1";
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
		public  System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проектToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьДокументToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьДокументToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьВсеДокументыToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripMenuItem neo4jToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem подключениеToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
	}
}

