namespace Word_of_the_day.ViewModels
{
	using DevExpress.Mvvm;

	using IniFiles;

	using System;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="NotificationViewModel" />
	/// </summary>
	internal class NotificationViewModel : ViewModelBase
	{
		/// <summary>
		/// Gets or sets the CloseAction
		/// </summary>
		public Action CloseAction { get; set; }

		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Gets or sets the Width
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Gets or sets the Height
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the X
		/// </summary>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets the Y
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		/// Gets or sets the ContentWord
		/// </summary>
		public string ContentWord { get; set; }

		/// <summary>
		/// Gets or sets the OpWord
		/// </summary>
		public float OpWord { get; set; }

		/// <summary>
		/// Gets or sets the MarginWord
		/// </summary>
		public string MarginWord { get; set; }

		/// <summary>
		/// Gets or sets the ContentTran
		/// </summary>
		public string ContentTran { get; set; }

		/// <summary>
		/// Gets or sets the OpLine
		/// </summary>
		public float OpLine { get; set; }

		/// <summary>
		/// Gets or sets the Progress
		/// </summary>
		public float Progress { get; set; }

		/// <summary>
		/// Defines the Word
		/// </summary>
		public static string Word;

		/// <summary>
		/// Defines the Tran
		/// </summary>
		public static string Tran;

		/// <summary>
		/// Gets or sets the ColorText
		/// </summary>
		public string ColorText { get; set; }

		/// <summary>
		/// Gets or sets the ColorBG
		/// </summary>
		public string ColorBG { get; set; }

		/// <summary>
		/// The SetTheme
		/// </summary>
		private void SetTheme()
		{
			int Theme = -1;
			if (INI.KeyExists("Theme", "Setting"))
				Theme = int.Parse(INI.ReadINI("Setting", "Theme"));

			switch (Theme)
			{
				case 0:
					White();
					break;
				case 1:
					Black();
					break;
				case 2:
					Red();
					break;
				case 3:
					Blue();
					break;

				default:
					Black();
					break;
			}
		}

		/// <summary>
		/// The White
		/// </summary>
		private void White()
		{
			ColorBG = "#FFF1F1F1";
			ColorText = "#FF0E0E0E";
		}

		/// <summary>
		/// The Black
		/// </summary>
		private void Black()
		{
			ColorBG = "#FF181819";
			ColorText = "#FFF7F7F7";
		}

		/// <summary>
		/// The Red
		/// </summary>
		private void Red()
		{
			ColorBG = "#FFBB3A3A";
			ColorText = "WhiteSmoke";
		}

		/// <summary>
		/// The Blue
		/// </summary>
		private void Blue()
		{
			ColorBG = "#FF2B2B85";
			ColorText = "WhiteSmoke";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotificationViewModel"/> class.
		/// </summary>
		public NotificationViewModel()
		{
			Task.Factory.StartNew(() =>
			{
				SetTheme();
				int TimeDisplay;
				if (INI.KeyExists("TimeDisplay", "Word"))
					TimeDisplay = int.Parse(INI.ReadINI("Word", "TimeDisplay"));
				else
					TimeDisplay = 30;

				int HeightWindow = -200;
				Y = HeightWindow;
				Width = 90;
				Height = 90;
				ContentWord = Word[0].ToString();
				OpWord = 1;
				OpLine = 0;
				MarginWord = "0";
				ContentTran = Tran;
				if (Word == "Error3301") { return; }


				for (Y = HeightWindow; Y < 15;)
				{
					Y += 0.5F;


					Thread.Sleep(1);
				}
				OpWord = 0;
				ContentWord = Word;

				for (; Width <= 680;)
				{
					Width += 2;
					if (Height <= 500)
						Height++;


					if (Width > 100)
					{
						OpWord += 0.005F;
						Thread.Sleep(1);
					}
					else
						Thread.Sleep(10);
				}
				OpWord = 1;

				for (int i = 0; i < 680 / 2; i++)
				{
					MarginWord = $"0,0,{i},0";
					if (i > 700 / 4)
						OpLine += 0.01F;
					Thread.Sleep(1);
				}



				float time = TimeDisplay;
				float step = 1 / time;
				Progress = 100;
				while ((Progress -= step) > 0)
				{
					Thread.Sleep(10);
				}

				for (; Y > HeightWindow;)
				{
					Y -= 1F;
					Thread.Sleep(1);
				}

				CloseAction();
			});
		}
	}
}
