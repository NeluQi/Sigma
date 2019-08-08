namespace Word_of_the_day.ViewModels
{
	using DevExpress.Mvvm;

	using System.Windows.Controls;

	/// <summary>
	/// Defines the <see cref="MainViewModel" />
	/// </summary>
	internal class MainViewModel : ViewModelBase
	{
		/// <summary>
		/// Defines the Stat
		/// </summary>
		private Page Stat;

		/// <summary>
		/// Defines the Setting
		/// </summary>
		private Page Setting;

		/// <summary>
		/// Defines the Word
		/// </summary>
		private Page Word;

		/// <summary>
		/// Gets or sets the SelectPage
		/// </summary>
		public Page SelectPage { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		public MainViewModel()
		{
			Stat = new Views.Stats();
			Setting = new Views.Setting();
			Word = new Views.Word();
			SelectPage = Stat;
		}

		/// <summary>
		/// Gets the SelectSettingPage
		/// </summary>
		public DelegateCommand SelectSettingPage {
			get {
				return new DelegateCommand(
					() =>
					{
						SelectPage = Setting;
					});
			}
		}

		/// <summary>
		/// Gets the SelectWordPage
		/// </summary>
		public DelegateCommand SelectWordPage {
			get {
				return new DelegateCommand(
					() =>
					{
						SelectPage = Word;
					});
			}
		}

		/// <summary>
		/// Gets the SelectStatPage
		/// </summary>
		public DelegateCommand SelectStatPage {
			get {
				return new DelegateCommand(
					() =>
					{
						SelectPage = Stat;
					});
			}
		}
	}
}
