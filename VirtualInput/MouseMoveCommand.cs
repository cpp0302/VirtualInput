using System;
using System.Windows.Forms;
using System.Drawing;
using VirtualInput.Win32;

namespace VirtualInput
{
	public class MouseMoveCommand : ISendInputCommandInternal
	{
		public Point Point { get; set; }

		public MouseMoveCommand()
		{
		}

		public MouseMoveCommand(int x, int y)
		{
			this.Point = new Point(x, y);
		}

		SendInputParameter[] ISendInputCommandInternal.GetSendInputParameter()
		{
			var parameters = new SendInputParameter[1];

			//スクリーンの範囲は (0, 0) - (dx, dy) - (65535, 65535) で定義されているらしい
			parameters[0].type = User32.INPUT_MOUSE;
			parameters[0].mi.dwFlags = User32.MOUSEEVENTF_MOVE | User32.MOUSEEVENTF_ABSOLUTE;
			parameters[0].mi.dx = (int)Math.Round(this.Point.X * (65535.0 / Screen.PrimaryScreen.Bounds.Width));
			parameters[0].mi.dy = (int)Math.Round(this.Point.Y * (65535.0 / Screen.PrimaryScreen.Bounds.Height));
			parameters[0].mi.mouseData = 0;
			parameters[0].mi.dwExtraInfo = 0;
			parameters[0].mi.time = 0;

			return parameters;
		}
	}
}
