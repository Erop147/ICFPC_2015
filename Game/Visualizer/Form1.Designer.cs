﻿namespace ICFPC2015.Visualizer
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
            this.gameIndexCombobox = new System.Windows.Forms.ComboBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.stopExecuteCommandButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.unitNumberLabel = new System.Windows.Forms.Label();
            this.moveWestButton = new System.Windows.Forms.Button();
            this.moveEastButton = new System.Windows.Forms.Button();
            this.moveSouthWestButton = new System.Windows.Forms.Button();
            this.moveSouthEastButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.turnCounterClockwiseButton = new System.Windows.Forms.Button();
            this.turnClockwiseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).BeginInit();
            this.SuspendLayout();
            // 
            // boardBox
            // 
            this.boardBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boardBox.Location = new System.Drawing.Point(544, 35);
            this.boardBox.Name = "boardBox";
            this.boardBox.Size = new System.Drawing.Size(475, 291);
            this.boardBox.TabIndex = 0;
            this.boardBox.TabStop = false;
            // 
            // openGameDialog
            // 
            this.openGameDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openGameDialog_FileOk);
            // 
            // openGameButton
            // 
            this.openGameButton.Location = new System.Drawing.Point(285, 35);
            this.openGameButton.Name = "openGameButton";
            this.openGameButton.Size = new System.Drawing.Size(171, 55);
            this.openGameButton.TabIndex = 1;
            this.openGameButton.Text = "Open game";
            this.openGameButton.UseVisualStyleBackColor = true;
            this.openGameButton.Click += new System.EventHandler(this.openGameButton_Click);
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.AutoSize = true;
            this.gameNameLabel.Location = new System.Drawing.Point(23, 50);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Size = new System.Drawing.Size(145, 25);
            this.gameNameLabel.TabIndex = 2;
            this.gameNameLabel.Text = "Choose game";
            // 
            // gameIndexCombobox
            // 
            this.gameIndexCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameIndexCombobox.Enabled = false;
            this.gameIndexCombobox.FormattingEnabled = true;
            this.gameIndexCombobox.Location = new System.Drawing.Point(285, 131);
            this.gameIndexCombobox.Name = "gameIndexCombobox";
            this.gameIndexCombobox.Size = new System.Drawing.Size(171, 33);
            this.gameIndexCombobox.TabIndex = 3;
            this.gameIndexCombobox.SelectedValueChanged += new System.EventHandler(this.gameIndexCombobox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose game number";
            // 
            // multipierTextBox
            // 
            this.multipierTextBox.Enabled = false;
            this.multipierTextBox.Location = new System.Drawing.Point(285, 199);
            this.multipierTextBox.Name = "multipierTextBox";
            this.multipierTextBox.Size = new System.Drawing.Size(171, 31);
            this.multipierTextBox.TabIndex = 5;
            this.multipierTextBox.Text = "10";
            this.multipierTextBox.TextChanged += new System.EventHandler(this.multipierTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Choose multiplier";
            // 
            // startGameButton
            // 
            this.startGameButton.Enabled = false;
            this.startGameButton.Location = new System.Drawing.Point(28, 274);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(428, 49);
            this.startGameButton.TabIndex = 7;
            this.startGameButton.Text = "RESTART GAME";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // timeoutTextBox
            // 
            this.timeoutTextBox.Location = new System.Drawing.Point(285, 388);
            this.timeoutTextBox.Name = "timeoutTextBox";
            this.timeoutTextBox.Size = new System.Drawing.Size(171, 31);
            this.timeoutTextBox.TabIndex = 8;
            this.timeoutTextBox.Text = "200";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Timeout";
            // 
            // commandsTextBox
            // 
            this.commandsTextBox.Location = new System.Drawing.Point(33, 454);
            this.commandsTextBox.Multiline = true;
            this.commandsTextBox.Name = "commandsTextBox";
            this.commandsTextBox.Size = new System.Drawing.Size(423, 175);
            this.commandsTextBox.TabIndex = 10;
            // 
            // executeComandButton
            // 
            this.executeComandButton.Enabled = false;
            this.executeComandButton.Location = new System.Drawing.Point(28, 664);
            this.executeComandButton.Name = "executeComandButton";
            this.executeComandButton.Size = new System.Drawing.Size(428, 52);
            this.executeComandButton.TabIndex = 11;
            this.executeComandButton.Text = "EXECUTE";
            this.executeComandButton.UseVisualStyleBackColor = true;
            this.executeComandButton.Click += new System.EventHandler(this.executeComandButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 802);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "SCORES:";
            // 
            // scoresLabel
            // 
            this.scoresLabel.AutoSize = true;
            this.scoresLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scoresLabel.Location = new System.Drawing.Point(140, 790);
            this.scoresLabel.Name = "scoresLabel";
            this.scoresLabel.Size = new System.Drawing.Size(36, 37);
            this.scoresLabel.TabIndex = 13;
            this.scoresLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 845);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "STATE:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stateLabel.Location = new System.Drawing.Point(153, 836);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(0, 37);
            this.stateLabel.TabIndex = 15;
            // 
            // stopExecuteCommandButton
            // 
            this.stopExecuteCommandButton.Enabled = false;
            this.stopExecuteCommandButton.Location = new System.Drawing.Point(28, 723);
            this.stopExecuteCommandButton.Name = "stopExecuteCommandButton";
            this.stopExecuteCommandButton.Size = new System.Drawing.Size(428, 54);
            this.stopExecuteCommandButton.TabIndex = 16;
            this.stopExecuteCommandButton.Text = "STOP";
            this.stopExecuteCommandButton.UseVisualStyleBackColor = true;
            this.stopExecuteCommandButton.Click += new System.EventHandler(this.stopExecuteCommandButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 894);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 25);
            this.label6.TabIndex = 17;
            this.label6.Text = "UNIT №";
            // 
            // unitNumberLabel
            // 
            this.unitNumberLabel.AutoSize = true;
            this.unitNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unitNumberLabel.Location = new System.Drawing.Point(140, 885);
            this.unitNumberLabel.Name = "unitNumberLabel";
            this.unitNumberLabel.Size = new System.Drawing.Size(0, 37);
            this.unitNumberLabel.TabIndex = 18;
            // 
            // moveWestButton
            // 
            this.moveWestButton.Location = new System.Drawing.Point(13, 928);
            this.moveWestButton.Name = "moveWestButton";
            this.moveWestButton.Size = new System.Drawing.Size(127, 56);
            this.moveWestButton.TabIndex = 19;
            this.moveWestButton.Text = "WEST";
            this.moveWestButton.UseVisualStyleBackColor = true;
            this.moveWestButton.Click += new System.EventHandler(this.moveWestButton_Click);
            // 
            // moveEastButton
            // 
            this.moveEastButton.Location = new System.Drawing.Point(314, 928);
            this.moveEastButton.Name = "moveEastButton";
            this.moveEastButton.Size = new System.Drawing.Size(142, 56);
            this.moveEastButton.TabIndex = 22;
            this.moveEastButton.Text = "EAST";
            this.moveEastButton.UseVisualStyleBackColor = true;
            this.moveEastButton.Click += new System.EventHandler(this.moveEastButton_Click);
            // 
            // moveSouthWestButton
            // 
            this.moveSouthWestButton.Location = new System.Drawing.Point(64, 1096);
            this.moveSouthWestButton.Name = "moveSouthWestButton";
            this.moveSouthWestButton.Size = new System.Drawing.Size(142, 56);
            this.moveSouthWestButton.TabIndex = 23;
            this.moveSouthWestButton.Text = "SW";
            this.moveSouthWestButton.UseVisualStyleBackColor = true;
            this.moveSouthWestButton.Click += new System.EventHandler(this.moveSouthWestButton_Click);
            // 
            // moveSouthEastButton
            // 
            this.moveSouthEastButton.Location = new System.Drawing.Point(230, 1096);
            this.moveSouthEastButton.Name = "moveSouthEastButton";
            this.moveSouthEastButton.Size = new System.Drawing.Size(142, 56);
            this.moveSouthEastButton.TabIndex = 24;
            this.moveSouthEastButton.Text = "SE";
            this.moveSouthEastButton.UseVisualStyleBackColor = true;
            this.moveSouthEastButton.Click += new System.EventHandler(this.moveSouthEastButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(917, 620);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(8, 8);
            this.button1.TabIndex = 25;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(230, 1180);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(226, 56);
            this.nextButton.TabIndex = 26;
            this.nextButton.Text = "NEXT";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(13, 1180);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(193, 56);
            this.previousButton.TabIndex = 27;
            this.previousButton.Text = "PREV";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // turnCounterClockwiseButton
            // 
            this.turnCounterClockwiseButton.Location = new System.Drawing.Point(96, 1002);
            this.turnCounterClockwiseButton.Name = "turnCounterClockwiseButton";
            this.turnCounterClockwiseButton.Size = new System.Drawing.Size(99, 56);
            this.turnCounterClockwiseButton.TabIndex = 28;
            this.turnCounterClockwiseButton.Text = "TCCW";
            this.turnCounterClockwiseButton.UseVisualStyleBackColor = true;
            this.turnCounterClockwiseButton.Click += new System.EventHandler(this.turnCounterClockwiseButton_Click);
            // 
            // turnClockwiseButton
            // 
            this.turnClockwiseButton.Location = new System.Drawing.Point(230, 1002);
            this.turnClockwiseButton.Name = "turnClockwiseButton";
            this.turnClockwiseButton.Size = new System.Drawing.Size(97, 56);
            this.turnClockwiseButton.TabIndex = 29;
            this.turnClockwiseButton.Text = "TCW";
            this.turnClockwiseButton.UseVisualStyleBackColor = true;
            this.turnClockwiseButton.Click += new System.EventHandler(this.turnClockwiseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2154, 1473);
            this.Controls.Add(this.turnClockwiseButton);
            this.Controls.Add(this.turnCounterClockwiseButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.moveSouthEastButton);
            this.Controls.Add(this.moveSouthWestButton);
            this.Controls.Add(this.moveEastButton);
            this.Controls.Add(this.moveWestButton);
            this.Controls.Add(this.unitNumberLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.stopExecuteCommandButton);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.label5);
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
            this.Controls.Add(this.gameIndexCombobox);
            this.Controls.Add(this.gameNameLabel);
            this.Controls.Add(this.openGameButton);
            this.Controls.Add(this.boardBox);
            this.Name = "Form1";
            this.Text = "Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.boardBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardBox;
        private System.Windows.Forms.OpenFileDialog openGameDialog;
        private System.Windows.Forms.Button openGameButton;
        private System.Windows.Forms.Label gameNameLabel;
        private System.Windows.Forms.ComboBox gameIndexCombobox;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button stopExecuteCommandButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label unitNumberLabel;
        private System.Windows.Forms.Button moveWestButton;
        private System.Windows.Forms.Button moveEastButton;
        private System.Windows.Forms.Button moveSouthWestButton;
        private System.Windows.Forms.Button moveSouthEastButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button turnCounterClockwiseButton;
        private System.Windows.Forms.Button turnClockwiseButton;
    }
}