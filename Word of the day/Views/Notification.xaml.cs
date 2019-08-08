namespace Word_of_the_day.Views
{
	using System;
	using System.Runtime.InteropServices;
	using System.Windows;
	using System.Windows.Interop;

	using Word_of_the_day.ViewModels;

	/// <summary>
	/// Interaction logic for Notification.xaml
	/// </summary>
	public partial class Notification : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Notification"/> class.
		/// </summary>
		/// <param name="Word">The Word<see cref="String"/></param>
		/// <param name="Tran">The Tran<see cref="String"/></param>
		public Notification(String Word, String Tran)
		{
			try
			{
				NotificationViewModel.Word = Word;
				NotificationViewModel.Tran = Tran;
				InitializeComponent();
				NotificationViewModel vm = (NotificationViewModel)this.DataContext;

				if (vm.CloseAction == null)
					vm.CloseAction = new Action(() =>
					{
						this.Dispatcher.Invoke(() =>
						{
							this.Close();
						});

					}).Invoke;
				SetWindowPos(new WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
				this.Topmost = true;

			}
			catch
			{
				this.Close();
			}
		}

		/// <summary>
		/// The OnSourceInitialized
		/// </summary>
		/// <param name="e">The e<see cref="EventArgs"/></param>
		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			//Set the window style to noactivate.
			WindowInteropHelper helper = new WindowInteropHelper(this);
			SetWindowLong(helper.Handle, GWL_EXSTYLE,
				GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
		}

		/// <summary>
		/// Defines the GWL_EXSTYLE
		/// </summary>
		private const int GWL_EXSTYLE = -20;

		/// <summary>
		/// Defines the WS_EX_NOACTIVATE
		/// </summary>
		private const int WS_EX_NOACTIVATE = 0x08000000;

		/// <summary>
		/// The SetWindowLong
		/// </summary>
		/// <param name="hWnd">The hWnd<see cref="IntPtr"/></param>
		/// <param name="nIndex">The nIndex<see cref="int"/></param>
		/// <param name="dwNewLong">The dwNewLong<see cref="int"/></param>
		/// <returns>The <see cref="IntPtr"/></returns>
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		/// <summary>
		/// The GetWindowLong
		/// </summary>
		/// <param name="hWnd">The hWnd<see cref="IntPtr"/></param>
		/// <param name="nIndex">The nIndex<see cref="int"/></param>
		/// <returns>The <see cref="int"/></returns>
		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		/// <summary>
		/// The Window_Loaded
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="RoutedEventArgs"/></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			OnSourceInitialized(e);
			this.Topmost = true;
		}

		/// <summary>
		/// The Window_Deactivated
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="EventArgs"/></param>
		private void Window_Deactivated(object sender, EventArgs e)
		{
			OnSourceInitialized(e);
			SetWindowPos(new WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
			this.Topmost = true;
		}

		/// <summary>
		/// Defines the HWND_TOPMOST
		/// </summary>
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		/// <summary>
		/// Defines the SWP_NOSIZE
		/// </summary>
		private const UInt32 SWP_NOSIZE = 0x0001;

		/// <summary>
		/// Defines the SWP_NOMOVE
		/// </summary>
		private const UInt32 SWP_NOMOVE = 0x0002;

		/// <summary>
		/// Defines the TOPMOST_FLAGS
		/// </summary>
		private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

		/// <summary>
		/// The SetWindowPos
		/// </summary>
		/// <param name="hWnd">The hWnd<see cref="IntPtr"/></param>
		/// <param name="hWndInsertAfter">The hWndInsertAfter<see cref="IntPtr"/></param>
		/// <param name="X">The X<see cref="int"/></param>
		/// <param name="Y">The Y<see cref="int"/></param>
		/// <param name="cx">The cx<see cref="int"/></param>
		/// <param name="cy">The cy<see cref="int"/></param>
		/// <param name="uFlags">The uFlags<see cref="uint"/></param>
		/// <returns>The <see cref="bool"/></returns>
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		/// <summary>
		/// The Window_FocusableChanged
		/// </summary>
		/// <param name="sender">The sender<see cref="object"/></param>
		/// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/></param>
		private void Window_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			SetWindowPos(new WindowInteropHelper(this).Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
			this.Topmost = true;
		}
	}
}
