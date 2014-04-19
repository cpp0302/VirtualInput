using System.Runtime.InteropServices;

namespace VirtualInput.Win32
{
	/// <summary>
	/// ハードウェアイベント
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct HardwareInput
	{
		public int uMsg;
		public short wParamL;
		public short wParamH;
	};
}
