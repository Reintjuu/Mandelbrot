namespace Mandelbrot
{
	partial class Main
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
			this.canvas = new System.Windows.Forms.PictureBox();
			this.centerXLabel = new System.Windows.Forms.Label();
			this.centerXTextBox = new System.Windows.Forms.TextBox();
			this.centerYTextBox = new System.Windows.Forms.TextBox();
			this.centerYLabel = new System.Windows.Forms.Label();
			this.maxLabel = new System.Windows.Forms.Label();
			this.scaleLabel = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.maxInput = new System.Windows.Forms.NumericUpDown();
			this.scaleTextBox = new System.Windows.Forms.TextBox();
			this.presetsComboBox = new System.Windows.Forms.ComboBox();
			this.presetLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxInput)).BeginInit();
			this.SuspendLayout();
			// 
			// canvas
			// 
			this.canvas.Location = new System.Drawing.Point(0, 66);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(533, 400);
			this.canvas.TabIndex = 0;
			this.canvas.TabStop = false;
			this.canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
			// 
			// centerXLabel
			// 
			this.centerXLabel.AutoSize = true;
			this.centerXLabel.Location = new System.Drawing.Point(12, 15);
			this.centerXLabel.Name = "centerXLabel";
			this.centerXLabel.Size = new System.Drawing.Size(51, 13);
			this.centerXLabel.TabIndex = 1;
			this.centerXLabel.Text = "Center X:";
			// 
			// centerXTextBox
			// 
			this.centerXTextBox.Location = new System.Drawing.Point(69, 12);
			this.centerXTextBox.Name = "centerXTextBox";
			this.centerXTextBox.Size = new System.Drawing.Size(100, 20);
			this.centerXTextBox.TabIndex = 2;
			this.centerXTextBox.Text = "0";
			// 
			// centerYTextBox
			// 
			this.centerYTextBox.Location = new System.Drawing.Point(69, 38);
			this.centerYTextBox.Name = "centerYTextBox";
			this.centerYTextBox.Size = new System.Drawing.Size(100, 20);
			this.centerYTextBox.TabIndex = 4;
			this.centerYTextBox.Text = "0";
			// 
			// centerYLabel
			// 
			this.centerYLabel.AutoSize = true;
			this.centerYLabel.Location = new System.Drawing.Point(12, 41);
			this.centerYLabel.Name = "centerYLabel";
			this.centerYLabel.Size = new System.Drawing.Size(51, 13);
			this.centerYLabel.TabIndex = 3;
			this.centerYLabel.Text = "Center Y:";
			// 
			// maxLabel
			// 
			this.maxLabel.AutoSize = true;
			this.maxLabel.Location = new System.Drawing.Point(186, 41);
			this.maxLabel.Name = "maxLabel";
			this.maxLabel.Size = new System.Drawing.Size(30, 13);
			this.maxLabel.TabIndex = 7;
			this.maxLabel.Text = "Max:";
			// 
			// scaleLabel
			// 
			this.scaleLabel.AutoSize = true;
			this.scaleLabel.Location = new System.Drawing.Point(186, 15);
			this.scaleLabel.Name = "scaleLabel";
			this.scaleLabel.Size = new System.Drawing.Size(37, 13);
			this.scaleLabel.TabIndex = 5;
			this.scaleLabel.Text = "Scale:";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(290, 37);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(39, 23);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// maxInput
			// 
			this.maxInput.Location = new System.Drawing.Point(229, 38);
			this.maxInput.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.maxInput.Name = "maxInput";
			this.maxInput.Size = new System.Drawing.Size(55, 20);
			this.maxInput.TabIndex = 8;
			this.maxInput.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// scaleTextBox
			// 
			this.scaleTextBox.Location = new System.Drawing.Point(229, 12);
			this.scaleTextBox.Name = "scaleTextBox";
			this.scaleTextBox.Size = new System.Drawing.Size(100, 20);
			this.scaleTextBox.TabIndex = 6;
			this.scaleTextBox.Text = "1";
			// 
			// presetsComboBox
			// 
			this.presetsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.presetsComboBox.FormattingEnabled = true;
			this.presetsComboBox.Location = new System.Drawing.Point(400, 38);
			this.presetsComboBox.Name = "presetsComboBox";
			this.presetsComboBox.Size = new System.Drawing.Size(121, 21);
			this.presetsComboBox.TabIndex = 10;
			this.presetsComboBox.SelectedIndexChanged += new System.EventHandler(this.PresetsComboBox_SelectedIndexChanged);
			// 
			// presetLabel
			// 
			this.presetLabel.AutoSize = true;
			this.presetLabel.Location = new System.Drawing.Point(349, 41);
			this.presetLabel.Name = "presetLabel";
			this.presetLabel.Size = new System.Drawing.Size(40, 13);
			this.presetLabel.TabIndex = 11;
			this.presetLabel.Text = "Preset:";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(533, 467);
			this.Controls.Add(this.presetLabel);
			this.Controls.Add(this.presetsComboBox);
			this.Controls.Add(this.maxInput);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.maxLabel);
			this.Controls.Add(this.scaleTextBox);
			this.Controls.Add(this.scaleLabel);
			this.Controls.Add(this.centerYTextBox);
			this.Controls.Add(this.centerYLabel);
			this.Controls.Add(this.centerXTextBox);
			this.Controls.Add(this.centerXLabel);
			this.Controls.Add(this.canvas);
			this.Name = "Main";
			this.Text = "Mandelbrot";
			this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
			this.Resize += new System.EventHandler(this.Main_Resize);
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxInput)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.Label centerXLabel;
		private System.Windows.Forms.TextBox centerXTextBox;
		private System.Windows.Forms.TextBox centerYTextBox;
		private System.Windows.Forms.Label centerYLabel;
		private System.Windows.Forms.Label maxLabel;
		private System.Windows.Forms.Label scaleLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.NumericUpDown maxInput;
		private System.Windows.Forms.TextBox scaleTextBox;
		private System.Windows.Forms.ComboBox presetsComboBox;
		private System.Windows.Forms.Label presetLabel;
	}
}

