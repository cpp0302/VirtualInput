using System;
using System.Windows.Forms;
using VirtualInput.Win32;

namespace VirtualInput
{
	public class MouseClickCommand : ISendInputCommandInternal
	{
		MouseButtons ClickButton { get; set; }

		public MouseClickCommand()
		{
		}

		public MouseClickCommand(MouseButtons clickButton)
		{
			this.ClickButton = clickButton;
		}

		SendInputParameter[] ISendInputCommandInternal.GetSendInputParameter()
		{
			var parameters = new SendInputParameter[2];

			int dwFlagsDown = int.MaxValue;
			int dwFlagsUp = int.MaxValue;

			//TODO:ナンセンス
			switch (this.ClickButton)
			{
				case MouseButtons.Left:
					dwFlagsDown = User32.MOUSEEVENTF_LEFTDOWN;
					dwFlagsUp = User32.MOUSEEVENTF_LEFTUP;
					break;
				case MouseButtons.Right:
					dwFlagsDown = User32.MOUSEEVENTF_RIGHTDOWN;
					dwFlagsUp = User32.MOUSEEVENTF_RIGHTUP;
					break;
				case MouseButtons.Middle:
					dwFlagsDown = User32.MOUSEEVENTF_MIDDLEDOWN;
					dwFlagsUp = User32.MOUSEEVENTF_MIDDLEUP;
					break;
				default:
					throw new InvalidOperationException(String.Format("不正なクリック状態 {0}", this.ClickButton));
			}

			//ボタンを押す
			parameters[0].type = User32.INPUT_MOUSE;
			parameters[0].mi.dwFlags = dwFlagsDown;
			parameters[0].mi.dx = 0;
			parameters[0].mi.dy = 0;
			parameters[0].mi.mouseData = 0;
			parameters[0].mi.dwExtraInfo = 0;
			parameters[0].mi.time = 0;

			//ボタンを離す
			parameters[1].type = User32.INPUT_MOUSE;
			parameters[1].mi.dwFlags = dwFlagsUp;
			parameters[1].mi.dx = 0;
			parameters[1].mi.dy = 0;
			parameters[1].mi.mouseData = 0;
			parameters[1].mi.dwExtraInfo = 0;
			parameters[1].mi.time = 0;

			return parameters;
		}
	}
}
