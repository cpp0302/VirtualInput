using System.Runtime.InteropServices;

namespace VirtualInput.Win32
{
	/// <summary>
	/// 各種イベント(SendInputの引数データ)
	/// </summary>
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
