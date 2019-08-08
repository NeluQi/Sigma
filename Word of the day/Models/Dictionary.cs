namespace Word_of_the_day.Models
{
	using IniFiles;

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// Defines the <see cref="Dictionary" />
	/// </summary>
	internal class Dictionary
	{
		/// <summary>
		/// Gets or sets the Dict
		/// </summary>
		public List<string> Dict { get; set; }

		/// <summary>
		/// Gets or sets the DictUsed
		/// </summary>
		public List<string> DictUsed { get; set; }

		/// <summary>
		/// Defines the NameDict
		/// </summary>
		private string NameDict = "Dict.bin";

		/// <summary>
		/// Defines the NameDictUsed
		/// </summary>
		private string NameDictUsed = "DictUsed.bin";

		/// <summary>
		/// Defines the INI
		/// </summary>
		internal IniFile INI = new IniFile("config.ini");

		/// <summary>
		/// Initializes a new instance of the <see cref="Dictionary"/> class.
		/// </summary>
		public Dictionary()
		{
			if (!File.Exists(NameDict))
			{
				using (TextWriter tw = new StreamWriter(NameDict))
				{
					List<string> List = Properties.Resources.dic.Split(new[] { "\r\n" }, StringSplitOptions.None)
							 .ToList();
					foreach (String s in List)
						tw.WriteLine(s);
				}
			}
			else
			{
				var FileDict = File.ReadAllLines(NameDict);
				Dict = new List<string>(FileDict);
			}

			if (!File.Exists(NameDictUsed))
			{
				File.CreateText(NameDictUsed);
			}
			else
			{
				var FileDict = File.ReadAllLines(NameDictUsed);
				this.DictUsed = new List<string>(FileDict);
			}
		}

		/// <summary>
		/// The GetWordAndExclusion
		/// </summary>
		/// <returns>The <see cref="string"/></returns>
		public string GetWordAndExclusion()
		{

			if (Dict.Count < 1)
			{
				System.Windows.Forms.MessageBox.Show("The dictionary is corrupted. Restore the standard dictionary");
				return "Error3301=Ошибка";
			}

			int ID = new Random().Next(0, Dict.Count);
			string Word = Dict[ID];

			bool Advancedlearning;
			if (INI.KeyExists("Advancedlearning", "Word"))
				Advancedlearning = bool.Parse(INI.ReadINI("Word", "Advancedlearning"));
			else
				Advancedlearning = false;

			if (!Advancedlearning)
			{
				Dict.RemoveAt(ID);
				DictUsed.Add(Word);
				Save();
			}

			string[] WordAndTran = Word.Split('=');
			if (WordAndTran.Length != 2)
			{
				System.Windows.Forms.MessageBox.Show($"The line in the dictionary is incorrect or does not match the pattern (Line number - {ID})");
				return "Error3301=Ошибка";
			}
			if (WordAndTran[0].Length > 22 || WordAndTran[0].Length == 0 || WordAndTran[1].Length > 22 || WordAndTran[1].Length == 0)
			{
				Word = GetWordAndExclusion();
			}

			return Word;
		}

		/// <summary>
		/// The GetWord
		/// </summary>
		/// <returns>The <see cref="string"/></returns>
		public string GetWord()
		{
			if (Dict.Count < 1)
			{
				System.Windows.Forms.MessageBox.Show("The dictionary is corrupted. Restore the standard dictionary");
				return "Error3301=Ошибка";
			}
			int ID = new Random().Next(0, Dict.Count);
			string Word = Dict[ID];

			string[] WordAndTran = Word.Split('=');
			if (WordAndTran.Length != 2)
			{
				System.Windows.Forms.MessageBox.Show($"The line in the dictionary is incorrect or does not match the pattern (Line number - {ID})");
				return "Error3301=Ошибка";
			}
			if (WordAndTran[0].Length > 22 || WordAndTran[0].Length == 0 || WordAndTran[1].Length > 22 || WordAndTran[1].Length == 0)
			{
				Dict.RemoveAt(ID);
				Word = GetWord();
			}

			return Word;
		}

		/// <summary>
		/// The Save
		/// </summary>
		private void Save()
		{
			File.WriteAllLines(NameDict, Dict);
			File.WriteAllLines(NameDictUsed, DictUsed);
		}
	}
}
