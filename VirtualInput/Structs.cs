using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualInput
{
	// マウスイベント(mouse_eventの引数と同様のデータ)
	[StructLayout(LayoutKind.Sequential)]
	internal struct MouseInput
	{
		public int dx;
		public int dy;
		public int mouseData;
		public int dwFlags;
		public int time;
		public int dwExtraInfo;
	};

	// キーボードイベント(keybd_eventの引数と同様のデータ)
	[StructLayout(LayoutKind.Sequential)]
	internal struct KeyboardInput
	{
		public short wVk;
		public short wScan;
		public int dwFlags;
		public int time;
		public int dwExtraInfo;
	};

	// ハードウェアイベント
	[StructLayout(LayoutKind.Sequential)]
	internal struct HardwareInput
	{
		public int uMsg;
		public short wParamL;
		public short wParamH;
	};

	// 各種イベント(SendInputの引数データ)
	[StructLayout(LayoutKind.Explicit)]
	internal struct SendInputParameter
	{
		[FieldOffset(0)]
		public int type;
		[FieldOffset(4)]
		public MouseInput mi;
		[FieldOffset(4)]
		public KeyboardInput ki;
		[FieldOffset(4)]
		public HardwareInput hi;
	};
}