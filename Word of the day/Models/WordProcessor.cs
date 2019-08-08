using System;

/// <summary>
/// Defines the <see cref="WordProcessor" />
/// </summary>
internal class WordProcessor
{
	/// <summary>
	/// Defines the Line
	/// </summary>
	private string Line;

	/// <summary>
	/// Defines the Word
	/// </summary>
	public string Word;

	/// <summary>
	/// Defines the Tran
	/// </summary>
	public string Tran;

	/// <summary>
	/// Initializes a new instance of the <see cref="WordProcessor"/> class.
	/// </summary>
	/// <param name="Line">The Line<see cref="string"/></param>
	public WordProcessor(string Line)
	{
		this.Line = Line;
		GetProcessor();
	}

	/// <summary>
	/// The GetProcessor
	/// </summary>
	public void GetProcessor()
	{
		string[] WordAndTran = Line.Split('=');
		string Word = WordAndTran[0];
		string Tran = WordAndTran[1];

		this.Word = Char.ToUpper(Word[0]) + Word.Substring(1);
		this.Tran = Char.ToUpper(Tran[0]) + Tran.Substring(1);
	}
}
