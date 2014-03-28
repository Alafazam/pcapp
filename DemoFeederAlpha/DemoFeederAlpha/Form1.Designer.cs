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
            this.tb1 = new System.Windows.Forms.TextBox();
            this.btn2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn1.Font = new System.Drawing.Font("Orator Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.ForeColor = System.Drawing.Color.AliceBlue;
            this.btn1.Location = new System.Drawing.Point(1, 60);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(119, 38);
            this.btn1.TabIndex = 2;
            this.btn1.Text = "Start Feeding";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // tb1
            // 
            this.tb1.AcceptsReturn = true;
            this.tb1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tb1.Location = new System.Drawing.Point(126, 3);
            this.tb1.MinimumSize = new System.Drawing.Size(300, 200);
            this.tb1.Multiline = true;
            this.tb1.Name = "tb1";
            this.tb1.ReadOnly = true;
            this.tb1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb1.Size = new System.Drawing.Size(300, 231);
            this.tb1.TabIndex = 2;
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btn2.Font = new System.Drawing.Font("Orator Std", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.ForeColor = System.Drawing.Color.AliceBlue;
            this.btn2.Location = new System.Drawing.Point(1, 104);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(119, 40);
            this.btn2.TabIndex = 4;
            this.btn2.Text = "Stop Feeding";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Font = new System.Drawing.Font("Trajan Pro", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.AliceBlue;
            this.button1.Location = new System.Drawing.Point(1, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "About ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 236);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Name = "Form1";
            this.Text = "AlphaFeeder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        protected System.Windows.Forms.TextBox tb1;
        private System.Windows.Forms.Button button1;
        
    }
}

