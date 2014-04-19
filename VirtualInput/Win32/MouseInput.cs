using System.Runtime.InteropServices;

namespace VirtualInput.Win32
{
	/// <summary>
	/// マウスイベント(mouse_eventの引数と同様のデータ)
	/// </summary>
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
}
