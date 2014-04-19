using System.Runtime.InteropServices;

namespace VirtualInput.Win32
{
	/// <summary>
	/// キーボードイベント(keybd_eventの引数と同様のデータ)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct KeyboardInput
	{
		public short wVk;
		public short wScan;
		public int dwFlags;
		public int time;
		public int dwExtraInfo;
	};
}
