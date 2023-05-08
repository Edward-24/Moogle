using System.Text.RegularExpressions;
namespace MoogleEngine;

public class Library
{
    public static List<Documents> Docs = new List<Documents>();
    readonly static string Folder = Path.Combine(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 13), "Content"); 
    static public List<string> paths = Directory.GetFiles(Folder, "*.txt").ToList();
    static public int CantidadDoc = paths.Count;
    public static List<string> Allwords = new List<string>();
    public static int TotalWords;
    public Library()
    {
     foreach (string path in paths)
        {
            string aux = File.ReadAllText(path);
            string[] words = Regex.Split(aux.ToLower(), @"\W+");
            Docs.Add(new Documents(path, Path.GetFileNameWithoutExtension(path), aux , words , new List<double>(), new List<double>(),"", 0));
            Allwords.AddRange(words);
        } 
      Allwords = Allwords.Distinct().ToList();
      TotalWords = Allwords.Count;
    }
    public static void Clean()
    {
        foreach (Documents doc in Docs)
        {
            doc.TF.Clear();
            doc.TFIDF.Clear();  
        }
        Search.QueryTF.Clear();
        Search.Dictionary.Clear();
        Search.Docxword.Clear();
        Search.QueryOP.Clear();
        Search.Suggest.Clear();
        Suggestion.SuggestList.Clear();
    }
}