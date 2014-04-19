using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VirtualInput;
using System.Windows.Forms;
using System.Drawing;

namespace VirtualInputTest
{
	[TestFixture()]
    public class TestExecuteInput
    {
		public async Task MouseMoveCommand(int positionX, int positionY)
		{
			var eventList = new List<object>();
			eventList.Add(new MouseMoveCommand(positionX, positionY));

			var ei = new ExecuteInput();
			await ei.Execute(eventList);

			//実行後の座標を取得して比較
			var resultPosition = Cursor.Position;

			Assert.GreaterOrEqual(resultPosition.X, positionX - 1, "X座標が違う");
			Assert.LessOrEqual(resultPosition.X, positionX + 1, "X座標が違う");
			Assert.GreaterOrEqual(resultPosition.Y, positionY - 1, "Y座標が違う");
			Assert.LessOrEqual(resultPosition.Y, positionY + 1, "Y座標が違う");
		}

		[TestCase(500)]
		[TestCase(1000)]
		public async Task ThreadSleepCommand(int interval)
		{
			var eventList = new List<object>();
			eventList.Add(new ThreadSleepCommand(interval));

			var dateBefore = DateTime.Now;

			var ei = new ExecuteInput();
			await ei.Execute(eventList);

			var dateAfter = DateTime.Now;

			var ts = dateAfter - dateBefore;
			Console.WriteLine("処理時間:" + ts);
			Assert.GreaterOrEqual(ts.TotalMilliseconds, interval, "スリープしてないのでは？");
			Assert.LessOrEqual(ts.TotalMilliseconds, interval + 50, "余計な時間かかりすぎ");
		}

		[TestCase()]
		public void ExecuteInput_処理できないオブジェクトが入って例外()
		{
			var eventList = new List<object>();
			eventList.Add(this);

			var ei = new ExecuteInput();
			Assert.Catch<InvalidOperationException>(async () => await ei.Execute(eventList));
			//TODO:例外をTaskの外側に投げること
		}

		public async Task MouseClickCommand(Point p, MouseButtons mouseButtons)
		{
			//テスト手順
			//1.フォーム内のどこかに移動
			//  ※せめてボタンがない所に移動しないと左クリックのテストができない(ボタン押しまくるから)
			//2.クリックを実施
			//  このタイミングで元のフォーム側でクリックイベントが呼ばれるはず
			//  ここで結果を確かめる
			//  ※このメソッドで結果の確認は行わない(というよりできない)
			//3.1秒間スリープ
			//  フォーム側のクリックイベントが呼ばれる前に非同期処理が終了しちゃうと
			//  テストできないので

			var eventList = new List<object>();
			eventList.Add(new MouseMoveCommand(p.X, p.Y));
			eventList.Add(new MouseClickCommand(mouseButtons));
			eventList.Add(new ThreadSleepCommand(1000));

			var ei = new ExecuteInput();
			await ei.Execute(eventList);
		}

		[TestCase(MouseButtons.None)]
		public void MouseClickCommand_不正なクリック情報が入って例外(MouseButtons mouseButtons)
		{
			var eventList = new List<object>();
			eventList.Add(new MouseClickCommand(mouseButtons));

			var ei = new ExecuteInput();
			Assert.Catch<InvalidOperationException>(async () => await ei.Execute(eventList));
		}
    }
}
