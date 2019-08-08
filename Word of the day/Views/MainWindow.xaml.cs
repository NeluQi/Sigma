namespace Word_of_the_day
{
	using IniFiles;

	using System;
	using System.Diagnostics;
	using System.Windows;

	using Word_of_the_day.Models;
	using Word_of_the_day.ViewModels;
	using Word_of_the_day.Views;

	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Defines the ni
		/// </summary>
		private System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();

		/// <summary>
		/// Defines the TimerNotification
		/// </summary>
		public static System.Timers.Timer TimerNotification = new System.Timers.Timer();

		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Defines the Timeout
		/// </summary>
		public static int Timeout;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow()
		{
			string procName = Process.GetCurrentProcess().ProcessName;

			// get the list of all processes by the "procName"       
			Process[] processes = Process.GetProcessesByName(procName);

			if (processes.Length > 1)
			{
				MessageBox.Show(procName + " already running");
				ni.Visible = false;
				StatsViewModel.DisplayUpdate.Stop();
				this.Close();
				return;
			}


			InitializeComponent();
			Application.Current.MainWindow = this;
			ni.Icon = Properties.Resources.NotifyIcon;
			ni.Visible = true;
			ni.DoubleClick +=
				delegate (object sender, EventArgs args)
				{
					StatsViewModel.DisplayUpdate.Start();
					this.Show();
					this.WindowState = WindowState.Normal;
				};

			TimerNotification.Elapsed += new System.Timers.ElapsedEventHandler(TimeEvent);
			TimerNotification.Interval = 1000; // 1000 ms is one second
			TimerNotification.Start();


			if (INI.KeyExists("Timeout", "Word"))
				Timeout = int.Parse(INI.ReadINI("Word", "Timeout")) * 60;
			else
				Timeout = 30 * 60;

			bool LaunchMinimized;
			if (INI.KeyExists("LaunchMinimized", "Setting"))
				LaunchMinimized = bool.Parse(INI.ReadINI("Setting", "LaunchMinimized"));
			else
				LaunchMinimized = false;

			if (LaunchMinimized)
			{
				this.Hide();
				StatsViewModel.DisplayUpdate.Stop();
			}
		}

		/// <summary>
		/// Defines the _Timeout
		/// </summary>
		public static int _Timeout = 0;

		/// <summary>
		/// The TimeEvent
		/// </summary>
		/// <param name="source">The source<see cref="object"/></param>
		/// <param name="e">The e<see cref="System.Timers.ElapsedEventArgs"/></param>
		private void TimeEvent(object source, System.Timers.ElapsedEventArgs e)
		{
			_Timeout++;
			if (_Timeout >= Timeout)
			{
				Application.Current.Dispatcher.Invoke((Action)delegate
				{
					string Line = new Dictionary().GetWordAndExclusion();
					var WordProcessor = new WordProcessor(Line);
					Notification taskWindow = new Notification(WordProcessor.Word, WordProcessor.Tran);
					double screenWidth = SystemParameters.FullPrimaryScreenWidth;
					taskWindow.Show();
					taskWindow.Top = -200;
					taskWindow.Left = (screenWidth - taskWindow.Width) / 0x00000002;
				});
				_Timeout = 0;

			}
		}

		/// <summary>
		/// The Grid_MouseDown
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="System.Windows.Input.MouseButtonEventArgs"/></param>
		private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DragMove();
		}

		/// <summary>
		/// The WindowClose_Click
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void WindowClose_Click(object sender, RoutedEventArgs e)
		{
			ni.Visible = false;
			StatsViewModel.DisplayUpdate.Stop();
			Application.Current.Shutdown();
		}

		/// <summary>
		/// The WindowMinimize_Click
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void WindowMinimize_Click(object sender, RoutedEventArgs e)
		{
			this.Hide();
			StatsViewModel.DisplayUpdate.Stop();
		}
	}
}
