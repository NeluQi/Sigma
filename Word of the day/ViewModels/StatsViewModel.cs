namespace Word_of_the_day.ViewModels
{
	using DevExpress.Mvvm;

	using IniFiles;

	using Word_of_the_day.Models;

	/// <summary>
	/// Defines the <see cref="StatsViewModel" />
	/// </summary>
	internal class StatsViewModel : ViewModelBase
	{
		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Gets or sets the Total
		/// </summary>
		public long Total { get; set; }

		/// <summary>
		/// Gets or sets the Learn
		/// </summary>
		public long Learn { get; set; }

		/// <summary>
		/// Gets or sets the Left
		/// </summary>
		public long Left { get; set; }

		/// <summary>
		/// Gets or sets the Timeout
		/// </summary>
		public long Timeout { get; set; }

		/// <summary>
		/// Gets or sets the Week
		/// </summary>
		public long Week { get; set; }

		/// <summary>
		/// Gets or sets the Mounth
		/// </summary>
		public long Mounth { get; set; }

		/// <summary>
		/// Defines the DisplayUpdate
		/// </summary>
		public static System.Timers.Timer DisplayUpdate = new System.Timers.Timer();

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsViewModel"/> class.
		/// </summary>
		public StatsViewModel()
		{
			Timeout = 0;
			DisplayUpdate.Elapsed += new System.Timers.ElapsedEventHandler(DisplayTimeEvent);
			DisplayUpdate.Interval = 1000; // 1000 ms is one second
			DisplayUpdate.Start();
		}

		/// <summary>
		/// The DisplayTimeEvent
		/// </summary>
		/// <param name="source">The source<see cref="object"/></param>
		/// <param name="e">The e<see cref="System.Timers.ElapsedEventArgs"/></param>
		private void DisplayTimeEvent(object source, System.Timers.ElapsedEventArgs e)
		{
			Timeout = MainWindow.Timeout - MainWindow._Timeout;
			int time;
			if (INI.KeyExists("Timeout", "Word"))
				time = int.Parse(INI.ReadINI("Word", "Timeout"));
			else
				time = 30;
			Week = (long)1680 / time;
			Mounth = (long)7200 / time;

			var dic = new Dictionary();
			Total = dic.Dict.Count;
			Learn = dic.DictUsed.Count;
			Left = dic.Dict.Count;
		}
	}
}
