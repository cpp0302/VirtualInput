using System.Threading;

namespace VirtualInput
{
	public class ThreadSleepCommand : IOtherCommand
	{
		/// <summary>
		/// スレッドがスリープする時間(ミリ秒)
		/// </summary>
		public int SleepInterval { get; set; }

		public ThreadSleepCommand()
		{
		}

		public ThreadSleepCommand(int interval)
		{
			this.SleepInterval = interval;
		}

		void IOtherCommand.ExecuteCommand()
		{
			Thread.Sleep(this.SleepInterval);
		}
	}
}
