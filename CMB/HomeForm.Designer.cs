namespace CMB
{
    partial class HomeForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelProblema1 = new System.Windows.Forms.Label();
            this.labelProblema2 = new System.Windows.Forms.Label();
            this.labelProblema3 = new System.Windows.Forms.Label();
            this.labelProblema4 = new System.Windows.Forms.Label();
            this.labelProblema5 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Pesquisar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Enviar um feedback";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelProblema1
            // 
            this.labelProblema1.AutoSize = true;
            this.labelProblema1.Location = new System.Drawing.Point(28, 244);
            this.labelProblema1.Name = "labelProblema1";
            this.labelProblema1.Size = new System.Drawing.Size(35, 13);
            this.labelProblema1.TabIndex = 2;
            this.labelProblema1.Text = "label1";
            // 
            // labelProblema2
            // 
            this.labelProblema2.AutoSize = true;
            this.labelProblema2.Location = new System.Drawing.Point(28, 286);
            this.labelProblema2.Name = "labelProblema2";
            this.labelProblema2.Size = new System.Drawing.Size(35, 13);
            this.labelProblema2.TabIndex = 3;
            this.labelProblema2.Text = "label1";
            // 
            // labelProblema3
            // 
            this.labelProblema3.AutoSize = true;
            this.labelProblema3.Location = new System.Drawing.Point(28, 329);
            this.labelProblema3.Name = "labelProblema3";
            this.labelProblema3.Size = new System.Drawing.Size(35, 13);
            this.labelProblema3.TabIndex = 4;
            this.labelProblema3.Text = "label1";
            // 
            // labelProblema4
            // 
            this.labelProblema4.AutoSize = true;
            this.labelProblema4.Location = new System.Drawing.Point(28, 370);
            this.labelProblema4.Name = "labelProblema4";
            this.labelProblema4.Size = new System.Drawing.Size(35, 13);
            this.labelProblema4.TabIndex = 5;
            this.labelProblema4.Text = "label1";
            // 
            // labelProblema5
            // 
            this.labelProblema5.AutoSize = true;
            this.labelProblema5.Location = new System.Drawing.Point(28, 413);
            this.labelProblema5.Name = "labelProblema5";
            this.labelProblema5.Size = new System.Drawing.Size(35, 13);
            this.labelProblema5.TabIndex = 6;
            this.labelProblema5.Text = "label1";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(482, 43);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(35, 13);
            this.lblWelcome.TabIndex = 7;
            this.lblWelcome.Text = "label1";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.labelProblema5);
            this.Controls.Add(this.labelProblema4);
            this.Controls.Add(this.labelProblema3);
            this.Controls.Add(this.labelProblema2);
            this.Controls.Add(this.labelProblema1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelProblema1;
        private System.Windows.Forms.Label labelProblema2;
        private System.Windows.Forms.Label labelProblema3;
        private System.Windows.Forms.Label labelProblema4;
        private System.Windows.Forms.Label labelProblema5;
        private System.Windows.Forms.Label lblWelcome;
    }
}