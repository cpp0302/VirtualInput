using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
					dwFlagsDown = ExecuteInput.MOUSEEVENTF_LEFTDOWN;
					dwFlagsUp = ExecuteInput.MOUSEEVENTF_LEFTUP;
					break;
				case MouseButtons.Right:
					dwFlagsDown = ExecuteInput.MOUSEEVENTF_RIGHTDOWN;
					dwFlagsUp = ExecuteInput.MOUSEEVENTF_RIGHTUP;
					break;
				case MouseButtons.Middle:
					dwFlagsDown = ExecuteInput.MOUSEEVENTF_MIDDLEDOWN;
					dwFlagsUp = ExecuteInput.MOUSEEVENTF_MIDDLEUP;
					break;
				default:
					throw new InvalidOperationException(String.Format("不正なクリック状態 {0}", this.ClickButton));
			}

			//ボタンを押す
			parameters[0].type = ExecuteInput.INPUT_MOUSE;
			parameters[0].mi.dwFlags = dwFlagsDown;
			parameters[0].mi.dx = 0;
			parameters[0].mi.dy = 0;
			parameters[0].mi.mouseData = 0;
			parameters[0].mi.dwExtraInfo = 0;
			parameters[0].mi.time = 0;

			//ボタンを離す
			parameters[1].type = ExecuteInput.INPUT_MOUSE;
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
