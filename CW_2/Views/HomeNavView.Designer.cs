﻿namespace CW_2.Views
{
    partial class HomeNavView
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
            this.btnContact = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnContact
            // 
            this.btnContact.Location = new System.Drawing.Point(362, 132);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(338, 49);
            this.btnContact.TabIndex = 0;
            this.btnContact.Text = "Contacts";
            this.btnContact.UseVisualStyleBackColor = true;
            this.btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(362, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(338, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Transactions";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(362, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(338, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "Events";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(362, 302);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(338, 49);
            this.button5.TabIndex = 4;
            this.button5.Text = "Reports";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(362, 357);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(338, 49);
            this.button6.TabIndex = 5;
            this.button6.Text = "Predictions";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(940, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 47);
            this.button1.TabIndex = 6;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HomeNavView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 578);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnContact);
            this.Name = "HomeNavView";
            this.Text = "HomeNavView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeNavView_FormClosing);
            this.Load += new System.EventHandler(this.HomeNavView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnContact;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
    }
}