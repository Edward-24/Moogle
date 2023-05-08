namespace MoogleEngine;

public class Documents
{
    public Documents(string path, string title, string content, string[] words, List<double>TF, List<double>TFIDF, string snippet, float score)
    {
        this.Path = path;
        this.Title = title;
        this.Content = content;
        this.TF = TF;
        this.TFIDF = TFIDF;
        this.Snippet = snippet;
        this.Words = words;
        this.Score = score;

    }
    public string Path { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public List<double> TF { get; set; }
    public List<double> TFIDF { get; private set; }
    public string Snippet { get; set; }
    public string[] Words { get; private set; }
    public float Score { get; set; }
}
 
