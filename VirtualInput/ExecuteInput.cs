using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualInput.Win32;

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

						User32.SendInput(parameters.Length, ref parameters[0], parameters[0]);
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

		public async Task Execute(Action<VirtualInputWrapper> exe, Action<Task> continueWithTask = null)
		{
			if (continueWithTask == null)
			{
				continueWithTask = (t => { });
			}

			await Task.Run(() => ExecuteTaskRun(exe)).ContinueWith(continueWithTask);
		}

		private void ExecuteTaskRun(Action<VirtualInputWrapper> exe)
		{
			try
			{
				VirtualInputWrapper viw = new VirtualInputWrapper();
				exe(viw);
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
