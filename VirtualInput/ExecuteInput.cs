using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VirtualInput
{
	public class ExecuteInput
	{
		//イベントはラムダ式(というよりデリゲート)で実行か？
		//やりたいことをデリゲートで渡してあとは非同期で実行
		//問題はシナリオをどうやって表現するか
		//シナリオ例
		//1人目の変更をクリック
		//変更したい人のところまでページスクロール
		//変更したい人をクリック
		//以下5回繰り返し。。。
		//どうやって表現するか・・・

		//SendInputを呼び出すときは、INPUT構造体にデータを突っ込んでそれを渡している
		//INPUT構造体の中に以下の3つの構造体が入っている(INPUT.typeによって、どれを実行するのかを判別しているっぽい)
		// ・MOUSEINPUT
		// ・KEYBDINPUT
		// ・HARDWAREINPUT

		//必須条件
		// ・非同期で実行したほうがよい
		//
		//案1 デリゲート
		//実行する際にデリゲートを定義。その関数の中で何をやるのかを記述
		//こっちだと、結局デリートの中でSendInputとか呼び出さないといけないから微妙
		//
		//案2 動作を規定するオブジェクトを作成
		//マウスクリックをするオブジェクト、キーボードを押すオブジェクト、何秒か待つオブジェクトなどを作成して
		//それをリストに入れて渡してやる
		//こっちだったら、各オブジェクトにINPUTメソッドを返すインタフェースを実装してもらえば
		//非同期実行側でリストをforeachで回して実行するだけだからよさそう
		//
		//案2で。




		//TODO:仮想キーコードをスキャンコードに変換するDLLの呼び出しはどこにかくか？
		//ExecuteInputに書いてしまうとキーボードオブジェクトから呼び出せない
		//仕方ないからExecuteInputでラッピングさせるか？

		//ゆくゆくは作りたい
		//TODO:ダブルクリック用のオブジェクトを作成
		//TODO:キーボード押下用のオブジェクトを作成

		#region DLLImport

		// キー操作、マウス操作をシミュレート(擬似的に操作する)
        [DllImport("user32.dll")]
        private extern static void SendInput(
            int nInputs, ref SendInputParameter pInputs, int cbsize);

        // 仮想キーコードをスキャンコードに変換
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        private extern static int MapVirtualKey(
            int wCode, int wMapType);

		#endregion DLLImport

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

		public async Task Execute(List<Object> eventList, Action<Task> continueWithTask = null)
		{
			if(continueWithTask == null)
			{
				continueWithTask = (t => {});
			}

			await Task.Run(() => ExecuteTaskRun(eventList)).ContinueWith(continueWithTask);
		}

		/// <summary>
		/// Task.Runの中身。長いから分割した
		/// </summary>
		private void ExecuteTaskRun(List<object> eventList)
		{
			try
			{
				foreach (var obj in eventList)
				{
					if (obj is ISendInputCommandInternal)
					{
						ISendInputCommandInternal command = (ISendInputCommandInternal)obj;
						SendInputParameter[] parameters = command.GetSendInputParameter();

						SendInput(parameters.Length, ref parameters[0], Marshal.SizeOf(parameters[0]));
					}
					else if (obj is IOtherCommand)
					{
						IOtherCommand command = (IOtherCommand)obj;
						command.ExecuteCommand();
					}
					else
					{
						throw new InvalidOperationException(String.Format("無効なオブジェクト [{0}]", obj.GetType()));
					}
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (AggregateException ae)
			{
				//foreachを並列で実施しているため、キャンセル要求を複数受け取ってしまう可能性もある
				foreach (var exception in ae.InnerExceptions)
				{
					if (!(exception is OperationCanceledException)) throw new Exception("タスクにて何かしらの例外が発生", ae);
				}
				//ここまで来ている場合はただのキャンセル
			}
		}
	}
}
