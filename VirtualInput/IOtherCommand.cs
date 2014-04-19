using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualInput
{
	/// <summary>
	/// SendInput経由しないで何かしらの挙動を実行するインタフェース
	/// </summary>
	public interface IOtherCommand
	{
		void ExecuteCommand();
	}
}
