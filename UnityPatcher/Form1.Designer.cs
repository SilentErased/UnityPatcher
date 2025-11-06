namespace UnityPatcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            label3 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button3 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            tabPage2 = new TabPage();
            button5 = new Button();
            comboBox2 = new ComboBox();
            label2 = new Label();
            button4 = new Button();
            tabPage3 = new TabPage();
            label5 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 5.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(6, 214);
            label1.Name = "label1";
            label1.Size = new Size(210, 10);
            label1.TabIndex = 0;
            label1.Text = "Made by SilentErased; ModLoader by Livku2";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 36);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(180, 25);
            comboBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(188, 36);
            button1.Name = "button1";
            button1.Size = new Size(28, 25);
            button1.TabIndex = 2;
            button1.Text = "↺";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(188, 6);
            button2.Name = "button2";
            button2.Size = new Size(28, 25);
            button2.TabIndex = 3;
            button2.Text = "?";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(75, 16);
            label3.Name = "label3";
            label3.Size = new Size(64, 17);
            label3.TabIndex = 5;
            label3.Text = "Package";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new Point(6, 67);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(210, 113);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(6, 186);
            button3.Name = "button3";
            button3.Size = new Size(210, 25);
            button3.TabIndex = 7;
            button3.Text = "Patch";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new Font("JetBrains Mono", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(-1, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(230, 288);
            tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(checkBox2);
            tabPage1.Controls.Add(checkBox1);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(flowLayoutPanel1);
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(button1);
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(222, 259);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Patch";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("JetBrains Mono", 5.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox2.Location = new Point(9, 242);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(119, 14);
            checkBox2.TabIndex = 9;
            checkBox2.Text = "Install after Patch";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("JetBrains Mono", 5.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.Location = new Point(9, 227);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(204, 14);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Delete Decompiled Folder After Patch";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(button4);
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(222, 259);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Pull APK";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.FlatStyle = FlatStyle.Popup;
            button5.Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button5.Location = new Point(6, 109);
            button5.Name = "button5";
            button5.Size = new Size(210, 25);
            button5.TabIndex = 9;
            button5.Text = "Pull";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(6, 78);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(180, 25);
            comboBox2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(75, 58);
            label2.Name = "label2";
            label2.Size = new Size(64, 17);
            label2.TabIndex = 8;
            label2.Text = "Package";
            // 
            // button4
            // 
            button4.FlatStyle = FlatStyle.Popup;
            button4.Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button4.Location = new Point(188, 78);
            button4.Name = "button4";
            button4.Size = new Size(28, 25);
            button4.TabIndex = 7;
            button4.Text = "↺";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(label5);
            tabPage3.Location = new Point(4, 25);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(222, 259);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Information";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("JetBrains Mono", 8.249999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(3, 10);
            label5.Name = "label5";
            label5.Size = new Size(224, 238);
            label5.TabIndex = 1;
            label5.Text = resources.GetString("label5.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(225, 282);
            Controls.Add(tabControl1);
            Font = new Font("JetBrains Mono", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Easy Patcher";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private Button button2;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Button button5;
        private ComboBox comboBox2;
        private Label label2;
        private Button button4;
        private TabPage tabPage3;
        private Label label5;
    }
}
