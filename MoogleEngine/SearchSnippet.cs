using System.Text.RegularExpressions;
namespace MoogleEngine;

public class SearchSnippet
{ 
	public static List<Snippets> snippets = new List<Snippets>();
	public static int indexr = 0;
    public static string Result = "";
	public SearchSnippet()
	{
        foreach (double binary in Search.QueryTF)
        {
            foreach (Snippets doc in snippets)
            {
                    double cantidad = doc.Words.Count(s => s == Search.Dictionary[indexr]);
                    if (cantidad > 0)
                    {
                        doc.TFIDF.Add(Search.QueryTF[indexr] * (1 + Math.Log10(cantidad)) * Math.Log10((double)Library.CantidadDoc / (1 + Search.Docxword[indexr])));
                        doc.Score += (float)doc.TFIDF[indexr];
                    }
                    else
                    {
                        doc.TFIDF.Add(0);
                    }
            }
            indexr++;
        }
       snippets = snippets.OrderByDescending(s => s.Score).ToList();
        if (snippets.Count > 0)
        {
            Result = snippets[0].Snippet;
        }
        else
        {
            Result = "";
        }
        snippets.Clear();
        indexr = 0;
    }
}
