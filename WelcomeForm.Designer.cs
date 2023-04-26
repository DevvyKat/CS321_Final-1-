namespace CS321_Final_1_
{
    partial class WelcomeForm
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
            this.welcomeOpenButton = new System.Windows.Forms.Button();
            this.welcomeNewFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // welcomeOpenButton
            // 
            this.welcomeOpenButton.Font = new System.Drawing.Font("Poppins", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeOpenButton.Location = new System.Drawing.Point(60, 181);
            this.welcomeOpenButton.Name = "welcomeOpenButton";
            this.welcomeOpenButton.Size = new System.Drawing.Size(188, 36);
            this.welcomeOpenButton.TabIndex = 1;
            this.welcomeOpenButton.Text = "Open an Existing Project";
            this.welcomeOpenButton.UseVisualStyleBackColor = true;
            this.welcomeOpenButton.Click += new System.EventHandler(this.welcomeOpenButton_Click);
            // 
            // welcomeNewFile
            // 
            this.welcomeNewFile.Font = new System.Drawing.Font("Poppins", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeNewFile.Location = new System.Drawing.Point(60, 234);
            this.welcomeNewFile.Name = "welcomeNewFile";
            this.welcomeNewFile.Size = new System.Drawing.Size(188, 36);
            this.welcomeNewFile.TabIndex = 2;
            this.welcomeNewFile.Text = "Create a New Project";
            this.welcomeNewFile.UseVisualStyleBackColor = true;
            this.welcomeNewFile.Click += new System.EventHandler(this.welcomeNewFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 84);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time to show off your best artwork!";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 381);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.welcomeNewFile);
            this.Controls.Add(this.welcomeOpenButton);
            this.Name = "WelcomeForm";
            this.Text = "Welcome";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button welcomeOpenButton;
        private System.Windows.Forms.Button welcomeNewFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}