namespace IniFiles
{
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text;

	/// <summary>
	/// Defines the <see cref="IniFile" />
	/// </summary>
	internal class IniFile
	{
		/// <summary>
		/// Defines the Path
		/// </summary>
		internal string Path;//Имя файла.

		/// <summary>
		/// The WritePrivateProfileString
		/// </summary>
		/// <param name="Section">The Section<see cref="string"/></param>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <param name="Value">The Value<see cref="string"/></param>
		/// <param name="FilePath">The FilePath<see cref="string"/></param>
		/// <returns>The <see cref="long"/></returns>
		[DllImport("kernel32")] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
		internal static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

		/// <summary>
		/// The GetPrivateProfileString
		/// </summary>
		/// <param name="Section">The Section<see cref="string"/></param>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <param name="Default">The Default<see cref="string"/></param>
		/// <param name="RetVal">The RetVal<see cref="StringBuilder"/></param>
		/// <param name="Size">The Size<see cref="int"/></param>
		/// <param name="FilePath">The FilePath<see cref="string"/></param>
		/// <returns>The <see cref="int"/></returns>
		[DllImport("kernel32")] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
		internal static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

		// С помощью конструктора записываем пусть до файла и его имя.
		/// <summary>
		/// Initializes a new instance of the <see cref="IniFile"/> class.
		/// </summary>
		/// <param name="IniPath">The IniPath<see cref="string"/></param>
		public IniFile(string IniPath)
		{
			Path = new FileInfo(IniPath).FullName.ToString();
		}

		//Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
		/// <summary>
		/// The ReadINI
		/// </summary>
		/// <param name="Section">The Section<see cref="string"/></param>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <returns>The <see cref="string"/></returns>
		public string ReadINI(string Section, string Key)
		{
			var RetVal = new StringBuilder(255);
			GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
			return RetVal.ToString();
		}

		//Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
		/// <summary>
		/// The Write
		/// </summary>
		/// <param name="Section">The Section<see cref="string"/></param>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <param name="Value">The Value<see cref="string"/></param>
		public void Write(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, Path);
		}

		//Удаляем ключ из выбранной секции.
		/// <summary>
		/// The DeleteKey
		/// </summary>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <param name="Section">The Section<see cref="string"/></param>
		public void DeleteKey(string Key, string Section = null)
		{
			Write(Section, Key, null);
		}

		//Удаляем выбранную секцию
		/// <summary>
		/// The DeleteSection
		/// </summary>
		/// <param name="Section">The Section<see cref="string"/></param>
		public void DeleteSection(string Section = null)
		{
			Write(Section, null, null);
		}

		//Проверяем, есть ли такой ключ, в этой секции
		/// <summary>
		/// The KeyExists
		/// </summary>
		/// <param name="Key">The Key<see cref="string"/></param>
		/// <param name="Section">The Section<see cref="string"/></param>
		/// <returns>The <see cref="bool"/></returns>
		public bool KeyExists(string Key, string Section = null)
		{
			return ReadINI(Section, Key).Length > 0;
		}
	}
}
