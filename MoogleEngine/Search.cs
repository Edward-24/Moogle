using System.Text.RegularExpressions;
using Porter2Stemmer;
namespace MoogleEngine;
public class Search
{
   public static string? query;
   public static List<string> QueryOP = new List<string>();
   public static List<string> Query = new List<string>();
   public static List<double> QueryTF = new List<double>();
   public static List<int> Docxword = new List<int>();
   public static List<Documents> OrdDocs = new List<Documents>();
   public static List<int> Suggest = new List<int>();
   public static List<string> Dictionary = new List<string>();
   public static int QC = 0;
   int index = 0;
   int snippetwords = 2000;

    public Search()
    {
        List<string> StemmerQuery = new List<string>();
        if (query != null)
        {
            Query = new List<string>(Regex.Split(query, @"\W+"));
            Query.Remove("");
            QueryOP = Regex.Split(query, " ").ToList();
            QC = Query.Count;
            foreach (string word in Query)
            {
                StemmerQuery.Add(Stemming(word));
            }
            Query.AddRange(StemmerQuery);
            List<double> OperatorImportance = new List<double>();
            List<double> OperatorExist = new List<double>();
            foreach (string item in QueryOP)
            {
                if (item.StartsWith("*"))
                {
                    OperatorImportance.Add(2);
                    OperatorExist.Add(1);
                }
                else if (item.StartsWith("-"))
                {
                    OperatorImportance.Add(0.5);
                    OperatorExist.Add(1);
                }else if (item.StartsWith("^"))
                {
                    OperatorImportance.Add(1);
                    OperatorExist.Add(2);
                }
                else if(item.StartsWith("!")){
                    OperatorImportance.Add(1);
                    OperatorExist.Add(0);
                }
                else
                {
                    OperatorImportance.Add(1);
                    OperatorExist.Add(1);
                }
            }
            for (int i = 0; i < Query.Count - QC; i++)
            {
                OperatorImportance.Add(OperatorImportance[i]);
                OperatorExist.Add(OperatorExist[i]);
            }
            if (Query.Count > OperatorImportance.Count)
            {
               OperatorImportance = Enumerable.Repeat(1.0, Query.Count).ToList();
               OperatorExist = Enumerable.Repeat(1.0, Query.Count).ToList();
            }

            foreach (string word in Query)
            {
                bool aux = Library.Allwords.Contains(word);
                if (aux == true)
                {
                    QueryTF.Add(OperatorImportance[index]);
                    Suggest.Add(1);
                    Dictionary.Add(word);
                }
                else
                {
                    QueryTF.Add(0);
                    Suggest.Add(0);
                    Dictionary.Add(word);
                }
                index++;
            }
            index = 0;
            Docxword = Enumerable.Repeat(0, QueryTF.Count).ToList();
            foreach (double binary in QueryTF)
            {
                foreach (Documents doc in Library.Docs)
                {
                    double cantidad = doc.Words.Count(s => s == Dictionary[index]);
                    if (cantidad > 0)
                    {
                        Docxword[index] += 1;
                        doc.TF.Add((1 + Math.Log10(cantidad)) * binary);
                    }
                    else
                    {
                        doc.TF.Add(0);
                    }
                }
                index++;
            }
            foreach (Documents doc in Library.Docs)
            {
                int indexoperator = 0;
                foreach (int aux in OperatorExist)
                {
                    if (aux == 0 && doc.TF[indexoperator] != 0)
                    {
                        doc.TF = Enumerable.Repeat(0.0, QueryTF.Count).ToList();
                    }
                    if (aux == 2 && doc.TF[indexoperator] == 0)
                    {
                        doc.TF = Enumerable.Repeat(0.0, QueryTF.Count).ToList();
                    }
                    indexoperator++;
                }
                int index = 0;
                float score = 0;
                foreach (double tf in doc.TF)
                {
                    doc.TFIDF.Add(doc.TF[index] * Math.Log10((double)Library.CantidadDoc / (1 + Docxword[index])));
                    score += (float)doc.TFIDF[index];
                    index++;
                }
                doc.Score = score;
                if (score > 0)
                {
                    double max = doc.TFIDF.Max();
                    if (max != 0)
                    {
                        string HighestTFIDF = Dictionary[(doc.TFIDF.IndexOf(max))];
                        doc.Snippet = HighestTFIDF;
                    }
                }
            }
            OrdDocs = Library.Docs.OrderByDescending(a => a.Score).ToList();
            OrdDocs.RemoveAll(a => a.Score <= 0);
            for (int i = 0; i < OrdDocs.Count; i++)
            {
                OrdDocs[i].Snippet = WordsAround(OrdDocs[i].Content, OrdDocs[i].Snippet, snippetwords);
                if (OrdDocs[i].Snippet == "")
                {
                    int aux = OrdDocs[i].Content.Length;
                    if (aux > 2000)
                    {
                        aux = 2000;
                    }
                    OrdDocs[i].Snippet = OrdDocs[i].Content.Substring(0,aux);
                }
            }
        }
    }
        public static string WordsAround(string input, string word, int numW)
        {

            foreach (Match match in Regex.Matches(input, @$"\W+{word}\W+", RegexOptions.None))
            {
                int index = match.Index;
                int start = 0;
                if (index > numW / 2)
                {
                    start = index - numW / 2;
                }
                int aux = input.Length;
                int end = numW;
                if (start + numW > aux)
                {
                    end = aux - start;
                }
                string result = input.Substring(start, end);
                SearchSnippet.snippets.Add(new Snippets(result, Regex.Split(result.ToLower(), @"\W+"), new List<double>(), 0));
            }

            SearchSnippet searchSnippet = new SearchSnippet();
            return SearchSnippet.Result;
        }
    public static string Stemming(string input)
    {
        EnglishPorter2Stemmer stemming = new EnglishPorter2Stemmer();
        string result = stemming.Step0RemoveSPluralSuffix(input);
        result = stemming.Step1ARemoveOtherSPluralSuffixes(input);
        return result;
    }
}