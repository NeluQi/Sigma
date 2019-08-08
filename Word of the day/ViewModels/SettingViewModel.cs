namespace Word_of_the_day.ViewModels
{
	using DevExpress.Mvvm;

	using IniFiles;

	using Microsoft.Win32;

	using System.Windows;

	using Word_of_the_day.Models;
	using Word_of_the_day.Views;

	/// <summary>
	/// Defines the <see cref="SettingViewModel" />
	/// </summary>
	internal class SettingViewModel : ViewModelBase
	{
		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Defines the _Autorun
		/// </summary>
		private bool _Autorun;

		/// <summary>
		/// Gets or sets a value indicating whether Autorun
		/// </summary>
		public bool Autorun {
			get {
				return _Autorun;
			}

			set {
				_Autorun = value;
				INI.Write("Setting", "Autorun", value.ToString());
				if (value)
				{
					RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

					if (!IsStartupItem())
						// Add the value in the registry so that the application runs at startup
						rkApp.SetValue("Sigma", System.Reflection.Assembly.GetExecutingAssembly().Location.ToString());
				}
				else
				{
					RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

					if (IsStartupItem())
						// Remove the value from the registry so that the application doesn't start
						rkApp.DeleteValue("Sigma", false);
				}
			}
		}

		/// <summary>
		/// Defines the _LaunchMinimized
		/// </summary>
		private bool _LaunchMinimized;

		/// <summary>
		/// Gets or sets a value indicating whether LaunchMinimized
		/// </summary>
		public bool LaunchMinimized {
			get {
				return _LaunchMinimized;
			}

			set {
				_LaunchMinimized = value;
				INI.Write("Setting", "LaunchMinimized", value.ToString());
			}
		}

		/// <summary>
		/// Defines the _Theme
		/// </summary>
		private int _Theme;

		/// <summary>
		/// Gets or sets the Theme
		/// </summary>
		public int Theme {
			get {
				return _Theme;
			}

			set {
				_Theme = value;
				INI.Write("Setting", "Theme", value.ToString());
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingViewModel"/> class.
		/// </summary>
		public SettingViewModel()
		{
			_Autorun = IsStartupItem();

			if (INI.KeyExists("LaunchMinimized", "Setting"))
				_LaunchMinimized = bool.Parse(INI.ReadINI("Setting", "LaunchMinimized"));
			else
				_LaunchMinimized = false;

			if (INI.KeyExists("Theme", "Setting"))
				_Theme = int.Parse(INI.ReadINI("Setting", "Theme"));
			else
				_Theme = 1;
		}

		/// <summary>
		/// The IsStartupItem
		/// </summary>
		/// <returns>The <see cref="bool"/></returns>
		private bool IsStartupItem()
		{
			// The path to the key where Windows looks for startup applications
			RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (rkApp.GetValue("Sigma") == null)
				// The value doesn't exist, the application is not set to run at startup
				return false;
			else
				// The value exists, the application is set to run at startup
				return true;
		}

		/// <summary>
		/// Gets the DemoNotifications
		/// </summary>
		public DelegateCommand DemoNotifications {
			get {
				return new DelegateCommand(
					() =>
					{

						string Line = new Dictionary().GetWord();
						var WordProcessor = new WordProcessor(Line);
						Notification taskWindow = new Notification(WordProcessor.Word, WordProcessor.Tran);
						double screenWidth = SystemParameters.FullPrimaryScreenWidth;
						taskWindow.Show();
						taskWindow.Top = -200;
						taskWindow.Left = (screenWidth - taskWindow.Width) / 0x00000002;
					});
			}
		}

		/// <summary>
		/// Gets the GitHub
		/// </summary>
		public DelegateCommand GitHub {
			get {
				return new DelegateCommand(
					() =>
					{
						System.Diagnostics.Process.Start("https://github.com/NeluQi");
					});
			}
		}
	}
}
