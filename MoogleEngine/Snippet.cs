namespace MoogleEngine;

public class Snippets
{
	public Snippets(string snippet, string[] words,List<double> TFIDF, float score)
	{
		this.Snippet = snippet;
		this.Words = words;
		this.TFIDF = TFIDF;
		this.Score = score;

	}
	public string Snippet { get; set; }
	public string[] Words { get; set; }
	public List<double> TFIDF { get; set; }
	public float Score { get; set; }
}
