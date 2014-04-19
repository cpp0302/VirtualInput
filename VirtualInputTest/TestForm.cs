using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualInput;
using NUnit.Framework;

namespace VirtualInputTest
{
	public partial class TestForm : Form
	{
		Point mouseCursor;
		bool isClickTest;
		MouseButtons clickMouseButtons;

		public TestForm()
		{
			InitializeComponent();
			timerGetCursor.Start();
		}

		private async void buttonMouseMove_Click(object sender, EventArgs e)
		{
			if (!validatePosition())
			{
				MessageBox.Show("座標を入力して");
				return;
			}

			var x = int.Parse(this.textBoxPositionX.Text);
			var y = int.Parse(this.textBoxPositionY.Text);

			TestExecuteInput tei = new TestExecuteInput();
			//選択しているラジオボタンによって呼ぶテストメソッドを決める
			if (radioButtonEventList.Checked)
			{
				await tei.MouseMoveCommand(x, y);
			}
			else if (radioButtonWrapperCommon.Checked || radioButtonWrapperEachOther.Checked)
			{
				await tei.MouseMoveCommand_Wrapper(x, y);
			}

			this.mouseCursor = Cursor.Position;
			UpdateTextInfo();
		}

		private void UpdateTextInfo()
		{
			this.textBoxInfo.Text = "";

			string str = "";
			str += string.Format("カーソルの位置：{0}", this.mouseCursor);

			this.textBoxInfo.Text = str;
		}

		private bool validatePosition()
		{
			int dummy;
			if (this.textBoxPositionX.Text == "") return false;
			if (!int.TryParse(this.textBoxPositionX.Text, out dummy)) return false;

			if (this.textBoxPositionY.Text == "") return false;
			if (!int.TryParse(this.textBoxPositionY.Text, out dummy)) return false;

			return true;
		}

		private void timerGetCursor_Tick(object sender, EventArgs e)
		{
			this.mouseCursor = Cursor.Position;
			this.UpdateTextInfo();
		}

		private async void buttonClick_Click(object sender, EventArgs e)
		{
			MouseButtons mouseButtons = System.Windows.Forms.MouseButtons.None;

			switch (((Button)sender).Name)
			{
				case "buttonRightClick":
					mouseButtons = System.Windows.Forms.MouseButtons.Right;
					break;
				case "buttonLeftClick":
					mouseButtons = System.Windows.Forms.MouseButtons.Left;
					break;
				case "buttonMiddleClick":
					mouseButtons = System.Windows.Forms.MouseButtons.Middle;
					break;
				default:
					MessageBox.Show("なんかおかしい");
					return;
			}

			//カーソルをテキストボックスの位置に持ってくる
			Point point = Point.Add(Point.Subtract(Cursor.Position, new Size(this.textBoxInfo.PointToClient(Cursor.Position))), new Size(10, 10));

			this.isClickTest = true;
			this.clickMouseButtons = mouseButtons;

			var tei = new TestExecuteInput();

			//選択しているラジオボタンによって呼ぶテストメソッドを決める
			if (radioButtonEventList.Checked)
			{
				await tei.MouseClickCommand(point, mouseButtons);
			}
			else if (radioButtonWrapperCommon.Checked)
			{
				await tei.MouseClickCommand_Wrapper_Common(point, mouseButtons);
			}
			else if (radioButtonWrapperEachOther.Checked)
			{
				await tei.MouseClickCommand_Wrapper_EachOther(point, mouseButtons);
			}

			this.isClickTest = false;
		}

		private void textBoxInfo_MouseDown(object sender, MouseEventArgs e)
		{
			if (this.isClickTest)
			{
				Assert.AreEqual(e.Button, this.clickMouseButtons);
				Console.WriteLine(String.Format("{0} MouseDownテストOK!!!", this.clickMouseButtons));
			}
		}

		private void textBoxInfo_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.isClickTest)
			{
				Assert.AreEqual(e.Button, this.clickMouseButtons);
				Console.WriteLine(String.Format("{0} MouseUpテストOK!!!", this.clickMouseButtons));
			}
		}
	}
}
