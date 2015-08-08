namespace ICFPC2015.Visualizer
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
            this.boardBox = new System.Windows.Forms.PictureBox();
            this.openGameDialog = new System.Windows.Forms.OpenFileDialog();
            this.openGameButton = new System.Windows.Forms.Button();
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.multipierTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startGameButton = new System.Windows.Forms.Button();
            this.timeoutTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.commandsTextBox = new System.Windows.Forms.TextBox();
            this.executeComandButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.scoresLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boardBox
            // 
            this.boardBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardBox.Location = new System.Drawing.Point(-4, 2);
            this.boardBox.Name = "boardBox";
            this.boardBox.Size = new System.Drawing.Size(1, 1);
            this.boardBox.TabIndex = 0;
            this.boardBox.TabStop = false;
            // 
            // openGameDialog
            // 
            this.openGameDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openGameDialog_FileOk);
            // 
            // openGameButton
            // 
            this.openGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openGameButton.Location = new System.Drawing.Point(1971, 73);
            this.openGameButton.Name = "openGameButton";
            this.openGameButton.Size = new System.Drawing.Size(171, 55);
            this.openGameButton.TabIndex = 1;
            this.openGameButton.Text = "Open game";
            this.openGameButton.UseVisualStyleBackColor = true;
            this.openGameButton.Click += new System.EventHandler(this.openGameButton_Click);
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Location = new System.Drawing.Point(1709, 88);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(145, 25);
            this.gameNameLabel.TabIndex = 2;
            this.gameNameLabel.Text = "Choose game";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1971, 169);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(171, 33);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1714, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose game number";
            // 
            // multipierTextBox
            // 
            this.multipierTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.multipierTextBox.Location = new System.Drawing.Point(1971, 237);
            this.multipierTextBox.Name = "multipierTextBox";
            this.multipierTextBox.Size = new System.Drawing.Size(171, 31);
            this.multipierTextBox.TabIndex = 5;
            this.multipierTextBox.Text = "10";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1714, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Choose multiplier";
            // 
            // startGameButton
            // 
            this.startGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startGameButton.Location = new System.Drawing.Point(1714, 312);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(428, 49);
            this.startGameButton.TabIndex = 7;
            this.startGameButton.Text = "START GAME";
            this.startGameButton.UseVisualStyleBackColor = true;
            // 
            // timeoutTextBox
            // 
            this.timeoutTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeoutTextBox.Location = new System.Drawing.Point(1971, 426);
            this.timeoutTextBox.Name = "timeoutTextBox";
            this.timeoutTextBox.Size = new System.Drawing.Size(171, 31);
            this.timeoutTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1714, 426);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Timeout";
            // 
            // commandsTextBox
            // 
            this.commandsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandsTextBox.Location = new System.Drawing.Point(1719, 492);
            this.commandsTextBox.Multiline = true;
            this.commandsTextBox.Name = "commandsTextBox";
            this.commandsTextBox.Size = new System.Drawing.Size(423, 175);
            this.commandsTextBox.TabIndex = 10;
            // 
            // executeComandButton
            // 
            this.executeComandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.executeComandButton.Location = new System.Drawing.Point(1714, 702);
            this.executeComandButton.Name = "executeComandButton";
            this.executeComandButton.Size = new System.Drawing.Size(428, 52);
            this.executeComandButton.TabIndex = 11;
            this.executeComandButton.Text = "EXECUTE";
            this.executeComandButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1723, 818);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "SCORES:";
            // 
            // scoresLabel
            // 
            this.scoresLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scoresLabel.AutoSize = true;
            this.scoresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scoresLabel.Location = new System.Drawing.Point(1835, 806);
            this.scoresLabel.Name = "scoresLabel";
            this.scoresLabel.Size = new System.Drawing.Size(36, 37);
            this.scoresLabel.TabIndex = 13;
            this.scoresLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2154, 1473);
            this.Controls.Add(this.scoresLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.executeComandButton);
            this.Controls.Add(this.commandsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timeoutTextBox);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.multipierTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.openGameButton);
            this.Controls.Add(this.boardBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardBox;
        private System.Windows.Forms.OpenFileDialog openGameDialog;
        private System.Windows.Forms.Button openGameButton;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox multipierTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.TextBox timeoutTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox commandsTextBox;
        private System.Windows.Forms.Button executeComandButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label scoresLabel;
    }
}