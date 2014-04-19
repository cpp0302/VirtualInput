namespace VirtualInputTest
{
	partial class TestForm
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
			this.components = new System.ComponentModel.Container();
			this.buttonMouseMove = new System.Windows.Forms.Button();
			this.textBoxPositionX = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPositionY = new System.Windows.Forms.TextBox();
			this.textBoxInfo = new System.Windows.Forms.TextBox();
			this.timerGetCursor = new System.Windows.Forms.Timer(this.components);
			this.buttonRightClick = new System.Windows.Forms.Button();
			this.buttonLeftClick = new System.Windows.Forms.Button();
			this.buttonMiddleClick = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// buttonMouseMove
			// 
			this.buttonMouseMove.Location = new System.Drawing.Point(12, 162);
			this.buttonMouseMove.Name = "buttonMouseMove";
			this.buttonMouseMove.Size = new System.Drawing.Size(92, 23);
			this.buttonMouseMove.TabIndex = 4;
			this.buttonMouseMove.Text = "マウスを動かす";
			this.buttonMouseMove.UseVisualStyleBackColor = true;
			this.buttonMouseMove.Click += new System.EventHandler(this.buttonMouseMove_Click);
			// 
			// textBoxPositionX
			// 
			this.textBoxPositionX.Location = new System.Drawing.Point(30, 9);
			this.textBoxPositionX.Name = "textBoxPositionX";
			this.textBoxPositionX.Size = new System.Drawing.Size(36, 19);
			this.textBoxPositionX.TabIndex = 1;
			this.textBoxPositionX.Text = "72";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(12, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "X";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(72, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(12, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y";
			// 
			// textBoxPositionY
			// 
			this.textBoxPositionY.Location = new System.Drawing.Point(90, 9);
			this.textBoxPositionY.Name = "textBoxPositionY";
			this.textBoxPositionY.Size = new System.Drawing.Size(36, 19);
			this.textBoxPositionY.TabIndex = 3;
			this.textBoxPositionY.Text = "78";
			// 
			// textBoxInfo
			// 
			this.textBoxInfo.Location = new System.Drawing.Point(12, 291);
			this.textBoxInfo.Multiline = true;
			this.textBoxInfo.Name = "textBoxInfo";
			this.textBoxInfo.ReadOnly = true;
			this.textBoxInfo.Size = new System.Drawing.Size(411, 64);
			this.textBoxInfo.TabIndex = 5;
			this.textBoxInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBoxInfo_MouseDown);
			this.textBoxInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBoxInfo_MouseUp);
			// 
			// timerGetCursor
			// 
			this.timerGetCursor.Interval = 10;
			this.timerGetCursor.Tick += new System.EventHandler(this.timerGetCursor_Tick);
			// 
			// buttonRightClick
			// 
			this.buttonRightClick.Location = new System.Drawing.Point(110, 162);
			this.buttonRightClick.Name = "buttonRightClick";
			this.buttonRightClick.Size = new System.Drawing.Size(75, 23);
			this.buttonRightClick.TabIndex = 6;
			this.buttonRightClick.Text = "右クリック";
			this.buttonRightClick.UseVisualStyleBackColor = true;
			this.buttonRightClick.Click += new System.EventHandler(this.buttonClick_Click);
			// 
			// buttonLeftClick
			// 
			this.buttonLeftClick.Location = new System.Drawing.Point(191, 162);
			this.buttonLeftClick.Name = "buttonLeftClick";
			this.buttonLeftClick.Size = new System.Drawing.Size(75, 23);
			this.buttonLeftClick.TabIndex = 6;
			this.buttonLeftClick.Text = "左クリック";
			this.buttonLeftClick.UseVisualStyleBackColor = true;
			this.buttonLeftClick.Click += new System.EventHandler(this.buttonClick_Click);
			// 
			// buttonMiddleClick
			// 
			this.buttonMiddleClick.Location = new System.Drawing.Point(272, 162);
			this.buttonMiddleClick.Name = "buttonMiddleClick";
			this.buttonMiddleClick.Size = new System.Drawing.Size(75, 23);
			this.buttonMiddleClick.TabIndex = 6;
			this.buttonMiddleClick.Text = "中央クリック";
			this.buttonMiddleClick.UseVisualStyleBackColor = true;
			this.buttonMiddleClick.Click += new System.EventHandler(this.buttonClick_Click);
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 367);
			this.Controls.Add(this.buttonMiddleClick);
			this.Controls.Add(this.buttonLeftClick);
			this.Controls.Add(this.buttonRightClick);
			this.Controls.Add(this.textBoxInfo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxPositionY);
			this.Controls.Add(this.textBoxPositionX);
			this.Controls.Add(this.buttonMouseMove);
			this.Name = "TestForm";
			this.Text = "TestForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonMouseMove;
		private System.Windows.Forms.TextBox textBoxPositionX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxPositionY;
		private System.Windows.Forms.TextBox textBoxInfo;
		private System.Windows.Forms.Timer timerGetCursor;
		private System.Windows.Forms.Button buttonRightClick;
		private System.Windows.Forms.Button buttonLeftClick;
		private System.Windows.Forms.Button buttonMiddleClick;
	}
}