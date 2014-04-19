using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualInput.Win32;

namespace VirtualInput
{
	/// <summary>
	/// VirtualInputのラッパークラス
	/// EventListにデータを突っ込んで実行するのではなく、メソッドベースで実行したい場合はこちら。
	/// </summary>
	public class VirtualInputWrapper
	{
		public VirtualInputWrapper()
		{

		}

		public void RightButtonClick()
		{
			this.MouseClick(MouseButtons.Right);
		}

		public void LeftButtonClick()
		{
			this.MouseClick(MouseButtons.Left);
		}

		public void MiddleButtonClick()
		{
			this.MouseClick(MouseButtons.Middle);
		}

		public void MouseClick(MouseButtons clickButton)
		{
			ISendInputCommandInternal command = new MouseClickCommand(clickButton);
			this.SendInputCore(command);
		}

		public void MouseMove(Point p)
		{
			this.MouseMove(p.X, p.Y);
		}

		public void MouseMove(int x, int y)
		{
			ISendInputCommandInternal command = new MouseMoveCommand(x, y);
			this.SendInputCore(command);
		}

		private void SendInputCore(ISendInputCommandInternal command)
		{
			SendInputParameter[] parameters = command.GetSendInputParameter();
			User32.SendInput(parameters.Length, ref parameters[0], parameters[0]);
		}

		public void ThreadSleep(int milliSeconds)
		{
			IOtherCommand command = new ThreadSleepCommand(milliSeconds);
			this.OtherCommandCore(command);
		}

		private void OtherCommandCore(IOtherCommand command)
		{
			command.ExecuteCommand();
		}
	}
}
