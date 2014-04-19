using System.Runtime.InteropServices;

namespace VirtualInput.Win32
{
	internal class User32
	{
		#region 定数定義

		public const int INPUT_MOUSE = 0;                  // マウスイベント
		public const int INPUT_KEYBOARD = 1;               // キーボードイベント
		public const int INPUT_HARDWARE = 2;               // ハードウェアイベント

		public const int MOUSEEVENTF_MOVE = 0x1;           // マウスを移動する
		public const int MOUSEEVENTF_ABSOLUTE = 0x8000;    // 絶対座標指定
		public const int MOUSEEVENTF_LEFTDOWN = 0x2;       // 左　ボタンを押す
		public const int MOUSEEVENTF_LEFTUP = 0x4;         // 左　ボタンを離す
		public const int MOUSEEVENTF_RIGHTDOWN = 0x8;      // 右　ボタンを押す
		public const int MOUSEEVENTF_RIGHTUP = 0x10;       // 右　ボタンを離す
		public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;    // 中央ボタンを押す
		public const int MOUSEEVENTF_MIDDLEUP = 0x40;      // 中央ボタンを離す
		public const int MOUSEEVENTF_WHEEL = 0x800;        // ホイールを回転する
		public const int WHEEL_DELTA = 120;                // ホイール回転値

		public const int KEYEVENTF_KEYDOWN = 0x0;          // キーを押す
		public const int KEYEVENTF_KEYUP = 0x2;            // キーを離す
		public const int KEYEVENTF_EXTENDEDKEY = 0x1;      // 拡張コード
		public const int VK_SHIFT = 0x10;                  // SHIFTキー

		#endregion

		/// <summary>
		/// キー操作、マウス操作をシミュレート(擬似的に操作する)
		/// </summary>
		/// <param name="nInputs"></param>
		/// <param name="pInputs"></param>
		/// <param name="cbsize"></param>
		[DllImport("user32.dll")]
		public extern static void SendInput(int nInputs, ref SendInputParameter pInputs, int cbsize);

		/// <summary>
		/// 仮想キーコードをスキャンコードに変換
		/// </summary>
		/// <param name="wCode"></param>
		/// <param name="wMapType"></param>
		/// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
		public extern static int MapVirtualKey(int wCode, int wMapType);

		/// <summary>
		/// キー操作、マウス操作をシミュレート(擬似的に操作する)
		/// メソッド呼び出し元でMarshal.SizeOfを呼びたくないのでラッパーを作成
		/// </summary>
		/// <param name="nInputs"></param>
		/// <param name="pInputs"></param>
		/// <param name="cbSizeObj"></param>
		public static void SendInput(int nInputs, ref SendInputParameter pInputs, object cbSizeObj)
		{
			SendInput(nInputs, ref pInputs, Marshal.SizeOf(cbSizeObj));
		}
	}
}
