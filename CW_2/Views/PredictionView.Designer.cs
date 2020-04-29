﻿namespace CW_2.Views
{
    partial class PredictionView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPredictDay = new System.Windows.Forms.DateTimePicker();
            this.lblIncome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblIncometot = new System.Windows.Forms.Label();
            this.lblExptot = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expenses Prediction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(311, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select date you need to predict Expenses :";
            // 
            // dtpPredictDay
            // 
            this.dtpPredictDay.Location = new System.Drawing.Point(388, 90);
            this.dtpPredictDay.Name = "dtpPredictDay";
            this.dtpPredictDay.Size = new System.Drawing.Size(312, 26);
            this.dtpPredictDay.TabIndex = 2;
            // 
            // lblIncome
            // 
            this.lblIncome.AutoSize = true;
            this.lblIncome.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncome.Location = new System.Drawing.Point(3, 50);
            this.lblIncome.Name = "lblIncome";
            this.lblIncome.Size = new System.Drawing.Size(252, 35);
            this.lblIncome.TabIndex = 3;
            this.lblIncome.Text = "Average Income : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 35);
            this.label3.TabIndex = 4;
            this.label3.Text = "Average Expense : ";
            // 
            // lblIncometot
            // 
            this.lblIncometot.AutoSize = true;
            this.lblIncometot.Font = new System.Drawing.Font("Microsoft Tai Le", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncometot.Location = new System.Drawing.Point(261, 39);
            this.lblIncometot.Name = "lblIncometot";
            this.lblIncometot.Size = new System.Drawing.Size(0, 51);
            this.lblIncometot.TabIndex = 5;
            // 
            // lblExptot
            // 
            this.lblExptot.AutoSize = true;
            this.lblExptot.Font = new System.Drawing.Font("Microsoft Tai Le", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExptot.Location = new System.Drawing.Point(261, 113);
            this.lblExptot.Name = "lblExptot";
            this.lblExptot.Size = new System.Drawing.Size(0, 51);
            this.lblExptot.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblIncome);
            this.panel1.Controls.Add(this.lblExptot);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblIncometot);
            this.panel1.Location = new System.Drawing.Point(63, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 218);
            this.panel1.TabIndex = 7;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(388, 133);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(312, 48);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Prediction";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // PredictionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 449);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtpPredictDay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PredictionView";
            this.Text = "PredictionView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PredictionView_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPredictDay;
        private System.Windows.Forms.Label lblIncome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblIncometot;
        private System.Windows.Forms.Label lblExptot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStart;
    }
}