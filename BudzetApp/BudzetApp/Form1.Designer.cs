namespace BudzetApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblKwota = new Label();
            Kwota = new TextBox();
            lblKat = new Label();
            comKat = new ComboBox();
            lblOpis = new Label();
            textBox1 = new TextBox();
            lblTytul = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            btnDodaj = new Button();
            lblPrzy = new Label();
            lblWyd = new Label();
            lblOsz = new Label();
            lblPrzyZl = new Label();
            lblWydZl = new Label();
            lblOszZl = new Label();
            btnEksp = new Button();
            btnImp = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lblKwota
            // 
            lblKwota.AutoSize = true;
            lblKwota.Font = new Font("Arial", 11F);
            lblKwota.Location = new Point(47, 52);
            lblKwota.Name = "lblKwota";
            lblKwota.Size = new Size(53, 17);
            lblKwota.TabIndex = 0;
            lblKwota.Text = "Kwota:";
            lblKwota.Click += label1_Click;
            // 
            // Kwota
            // 
            Kwota.Location = new Point(106, 49);
            Kwota.Name = "Kwota";
            Kwota.Size = new Size(100, 23);
            Kwota.TabIndex = 1;
            // 
            // lblKat
            // 
            lblKat.AutoSize = true;
            lblKat.Font = new Font("Arial", 11F);
            lblKat.Location = new Point(26, 93);
            lblKat.Name = "lblKat";
            lblKat.Size = new Size(74, 17);
            lblKat.TabIndex = 2;
            lblKat.Text = "Kategoria:";
            lblKat.Click += label1_Click_1;
            // 
            // comKat
            // 
            comKat.FormattingEnabled = true;
            comKat.Items.AddRange(new object[] { "Przychody", "Wydatki" });
            comKat.Location = new Point(106, 92);
            comKat.Name = "comKat";
            comKat.Size = new Size(121, 23);
            comKat.TabIndex = 3;
            comKat.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lblOpis
            // 
            lblOpis.AutoSize = true;
            lblOpis.Font = new Font("Arial", 11F);
            lblOpis.Location = new Point(57, 134);
            lblOpis.Name = "lblOpis";
            lblOpis.Size = new Size(43, 17);
            lblOpis.TabIndex = 4;
            lblOpis.Text = "Opis:";
            lblOpis.Click += label1_Click_2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(106, 133);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(211, 23);
            textBox1.TabIndex = 5;
            // 
            // lblTytul
            // 
            lblTytul.AutoSize = true;
            lblTytul.Font = new Font("Arial", 11F);
            lblTytul.Location = new Point(12, 9);
            lblTytul.Name = "lblTytul";
            lblTytul.Size = new Size(141, 17);
            lblTytul.TabIndex = 6;
            lblTytul.Text = "Zarządzaj budżetem";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 11F);
            label1.Location = new Point(443, 9);
            label1.Name = "label1";
            label1.Size = new Size(105, 17);
            label1.TabIndex = 7;
            label1.Text = "Lista transakcji";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Location = new Point(443, 49);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Size = new Size(693, 465);
            dataGridView1.TabIndex = 8;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnDodaj
            // 
            btnDodaj.Location = new Point(106, 173);
            btnDodaj.Name = "btnDodaj";
            btnDodaj.Size = new Size(75, 23);
            btnDodaj.TabIndex = 9;
            btnDodaj.Text = "Dodaj";
            btnDodaj.UseVisualStyleBackColor = true;
            // 
            // lblPrzy
            // 
            lblPrzy.AutoSize = true;
            lblPrzy.Font = new Font("Arial", 11F);
            lblPrzy.Location = new Point(89, 219);
            lblPrzy.Name = "lblPrzy";
            lblPrzy.Size = new Size(81, 17);
            lblPrzy.TabIndex = 10;
            lblPrzy.Text = "Przychody:";
            lblPrzy.Click += label2_Click;
            // 
            // lblWyd
            // 
            lblWyd.AutoSize = true;
            lblWyd.Font = new Font("Arial", 11F);
            lblWyd.Location = new Point(106, 253);
            lblWyd.Name = "lblWyd";
            lblWyd.Size = new Size(64, 17);
            lblWyd.TabIndex = 11;
            lblWyd.Text = "Wydatki:";
            lblWyd.Click += label3_Click;
            // 
            // lblOsz
            // 
            lblOsz.AutoSize = true;
            lblOsz.Font = new Font("Arial", 11F);
            lblOsz.Location = new Point(66, 283);
            lblOsz.Name = "lblOsz";
            lblOsz.Size = new Size(104, 17);
            lblOsz.TabIndex = 12;
            lblOsz.Text = "Oszczędzone:";
            lblOsz.Click += label4_Click;
            // 
            // lblPrzyZl
            // 
            lblPrzyZl.AutoSize = true;
            lblPrzyZl.Font = new Font("Arial", 11F);
            lblPrzyZl.Location = new Point(176, 219);
            lblPrzyZl.Name = "lblPrzyZl";
            lblPrzyZl.Size = new Size(51, 17);
            lblPrzyZl.TabIndex = 13;
            lblPrzyZl.Text = "0.00 zł";
            // 
            // lblWydZl
            // 
            lblWydZl.AutoSize = true;
            lblWydZl.Font = new Font("Arial", 11F);
            lblWydZl.Location = new Point(176, 253);
            lblWydZl.Name = "lblWydZl";
            lblWydZl.Size = new Size(51, 17);
            lblWydZl.TabIndex = 14;
            lblWydZl.Text = "0.00 zł";
            // 
            // lblOszZl
            // 
            lblOszZl.AutoSize = true;
            lblOszZl.Font = new Font("Arial", 11F);
            lblOszZl.Location = new Point(176, 283);
            lblOszZl.Name = "lblOszZl";
            lblOszZl.Size = new Size(51, 17);
            lblOszZl.TabIndex = 15;
            lblOszZl.Text = "0.00 zł";
            // 
            // btnEksp
            // 
            btnEksp.Location = new Point(106, 450);
            btnEksp.Name = "btnEksp";
            btnEksp.Size = new Size(139, 23);
            btnEksp.TabIndex = 16;
            btnEksp.Text = "Eksportuj do XML";
            btnEksp.UseVisualStyleBackColor = true;
            // 
            // btnImp
            // 
            btnImp.Location = new Point(106, 491);
            btnImp.Name = "btnImp";
            btnImp.Size = new Size(139, 23);
            btnImp.TabIndex = 17;
            btnImp.Text = "Importuj z CSV";
            btnImp.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1174, 616);
            Controls.Add(btnImp);
            Controls.Add(btnEksp);
            Controls.Add(lblOszZl);
            Controls.Add(lblWydZl);
            Controls.Add(lblPrzyZl);
            Controls.Add(lblOsz);
            Controls.Add(lblWyd);
            Controls.Add(lblPrzy);
            Controls.Add(btnDodaj);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Controls.Add(lblTytul);
            Controls.Add(textBox1);
            Controls.Add(lblOpis);
            Controls.Add(comKat);
            Controls.Add(lblKat);
            Controls.Add(Kwota);
            Controls.Add(lblKwota);
            Font = new Font("Segoe UI", 9F);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblKwota;
        private TextBox Kwota;
        private Label lblKat;
        private ComboBox comKat;
        private Label lblOpis;
        private TextBox textBox1;
        private Label lblTytul;
        private Label label1;
        private DataGridView dataGridView1;
        private Button btnDodaj;
        private Label lblPrzy;
        private Label lblWyd;
        private Label lblOsz;
        private Label lblPrzyZl;
        private Label lblWydZl;
        private Label lblOszZl;
        private Button btnEksp;
        private Button btnImp;

    }
}
