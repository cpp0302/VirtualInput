using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualInput.Win32;

namespace VirtualInput
{
	/// <summary>
	/// SendInput経由で何かしらの挙動を実行することができるインタフェース
	/// ※DLLを呼ぶ側に見られたくないものはこちらに記述
	/// </summary>
	internal interface ISendInputCommandInternal : ISendCommandInput
	{
		SendInputParameter[] GetSendInputParameter();
	}
}
