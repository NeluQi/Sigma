namespace Word_of_the_day.ViewModels
{
	using DevExpress.Mvvm;

	using IniFiles;

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.IO;
	using System.Linq;
	using System.Threading;

	/// <summary>
	/// Defines the <see cref="WordViewModel" />
	/// </summary>
	internal class WordViewModel : ViewModelBase
	{
		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Gets or sets a value indicating whether Snackbar
		/// </summary>
		public bool Snackbar { get; set; }

		/// <summary>
		/// Defines the _Timeout
		/// </summary>
		private int _Timeout;

		/// <summary>
		/// Gets or sets the Timeout
		/// </summary>
		public int Timeout {
			get {
				return _Timeout;
			}

			set {
				if (value >= 1 && value < 1440)
				{
					_Timeout = value;
					MainWindow.Timeout = value * 60;
					INI.Write("Word", "Timeout", value.ToString());
				}
			}
		}

		/// <summary>
		/// Defines the _TimeDisplay
		/// </summary>
		private int _TimeDisplay;

		/// <summary>
		/// Gets or sets the TimeDisplay
		/// </summary>
		public int TimeDisplay {
			get {
				return _TimeDisplay;
			}

			set {
				if (value >= 1 && value < 100)
				{
					_TimeDisplay = value;
					INI.Write("Word", "TimeDisplay", value.ToString());
				}
			}
		}

		/// <summary>
		/// Defines the _Advancedlearning
		/// </summary>
		private bool _Advancedlearning;

		/// <summary>
		/// Gets or sets a value indicating whether Advancedlearning
		/// </summary>
		public bool Advancedlearning {
			get {
				return _Advancedlearning;
			}

			set {
				_Advancedlearning = value;
				INI.Write("Word", "Advancedlearning", value.ToString());
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WordViewModel"/> class.
		/// </summary>
		public WordViewModel()
		{
			if (INI.KeyExists("Timeout", "Word"))
				_Timeout = int.Parse(INI.ReadINI("Word", "Timeout"));
			else
				_Timeout = 30;

			if (INI.KeyExists("TimeDisplay", "Word"))
				_TimeDisplay = int.Parse(INI.ReadINI("Word", "TimeDisplay"));
			else
				_TimeDisplay = 30;

			if (INI.KeyExists("Advancedlearning", "Word"))
				_Advancedlearning = bool.Parse(INI.ReadINI("Word", "Advancedlearning"));
			else
				_Advancedlearning = false;
		}

		/// <summary>
		/// Gets the Editdictionary
		/// </summary>
		public DelegateCommand Editdictionary {
			get {
				return new DelegateCommand(
					() =>
					{
						Process.Start("notepad.exe", "Dict.bin");
					});
			}
		}

		/// <summary>
		/// Gets the Resetdictionary
		/// </summary>
		public DelegateCommand Resetdictionary {
			get {
				return new DelegateCommand(
					() =>
					{
						using (TextWriter tw = new StreamWriter("Dict.bin"))
						{
							List<string> List = Properties.Resources.dic.Split(new[] { "\r\n" }, StringSplitOptions.None)
									 .ToList();
							foreach (String s in List)
								tw.WriteLine(s);
						}
						Snackbar = true;
						new Thread(() =>
						{
							Thread.Sleep(2000);
							Snackbar = false;
						}).Start();
					});
			}
		}

		/// <summary>
		/// Gets the Resetusedword
		/// </summary>
		public DelegateCommand Resetusedword {
			get {
				return new DelegateCommand(
					() =>
					{
						using (StreamWriter writer = new System.IO.StreamWriter("Dict.bin", true))
						{
							List<string> List = File.ReadAllLines("DictUsed.bin", System.Text.Encoding.Default)
									 .ToList();
							foreach (String s in List)
								writer.WriteLine(s);
							File.WriteAllText("DictUsed.bin", String.Empty);
						}
						Snackbar = true;
						new Thread(() =>
						{
							Thread.Sleep(2000);
							Snackbar = false;
						}).Start();
					});
			}
		}
	}
}
