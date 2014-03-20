namespace DemoFeederAlpha
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.btn2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn1.Font = new System.Drawing.Font("Orator Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.ForeColor = System.Drawing.Color.AliceBlue;
            this.btn1.Location = new System.Drawing.Point(38, 399);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(197, 58);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "Start Feeding";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Controls.Add(this.tab2);
            this.tabControl1.ItemSize = new System.Drawing.Size(72, 40);
            this.tabControl1.Location = new System.Drawing.Point(34, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 351);
            this.tabControl1.TabIndex = 3;
            // 
            // tab1
            // 
            this.tab1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab1.Controls.Add(this.tb1);
            this.tab1.Location = new System.Drawing.Point(4, 44);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(636, 303);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Log";
            // 
            // tb1
            // 
            this.tb1.AcceptsReturn = true;
            this.tb1.Enabled = false;
            this.tb1.Location = new System.Drawing.Point(330, 6);
            this.tb1.MinimumSize = new System.Drawing.Size(300, 200);
            this.tb1.Multiline = true;
            this.tb1.Name = "tb1";
            this.tb1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb1.Size = new System.Drawing.Size(300, 200);
            this.tb1.TabIndex = 2;
            // 
            // tab2
            // 
            this.tab2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tab2.Controls.Add(this.pictureBox1);
            this.tab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab2.Location = new System.Drawing.Point(4, 44);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(636, 303);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "About";
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn2.Font = new System.Drawing.Font("Orator Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.ForeColor = System.Drawing.Color.AliceBlue;
            this.btn2.Location = new System.Drawing.Point(481, 399);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(197, 58);
            this.btn2.TabIndex = 4;
            this.btn2.Text = "Stop Feeding";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DemoFeederAlpha.Properties.Resources.aaaa;
            this.pictureBox1.Location = new System.Drawing.Point(10, -52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(620, 389);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 467);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "AlphaFeeder";
            this.tabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.Button btn2;
        protected System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.PictureBox pictureBox1;
        
    }
}

