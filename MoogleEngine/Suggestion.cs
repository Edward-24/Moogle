namespace MoogleEngine;

public class Suggestion
{
    public static int index = 0;
    public static List<string> SuggestList= new List<string>();
    public static string Suggest = "";
    public Suggestion(){

        for (int i = 0; i < Search.QC; i++)
        {
            if(Search.Suggest[index] == 0)
            {
                Search.Query[index] = SuggestWord(Search.Query[index]);
            }
            SuggestList.Add(Search.Query[index]);
            index++;
        }
      index = 0;
      Suggest = string.Join(" ", SuggestList.ToArray());
    }

    public string SuggestWord(string word)
    {
        int distance = 10000000;
        string suggest = "";
        foreach(string word2 in Library.Allwords)
        {
            int aux = Levenshtein(word, word2);
            if(aux < distance) { 
                distance = aux; 
                suggest = word2;
            }
        }
        return suggest;
    }

    public int Levenshtein(string word, string word2) 
    {
        char[] charword = word.ToCharArray();
        char[] charword2 = word2.ToCharArray();
        int x = word.Length + 1;
        int y = word2.Length + 1;
        int[,] Levenshtein = new int[y,x];
        for(int i = 0; i < x; i++)
        {
            Levenshtein[0,i] = i;
        }
        for(int i = 0; i < y; i++)
        {
            Levenshtein[i, 0] = i;
        }
        for (int i = 1; i < y; i++)
        {
            for(int j = 1; j < x; j++)
            {
                if(charword2[i-1] == charword[j - 1])
                {
                    Levenshtein[i,j] = Math.Min(Math.Min(Levenshtein[i - 1, j - 1], Levenshtein[i, j - 1]), Levenshtein[i - 1, j]);
                }
                else
                {
                    Levenshtein[i,j] = Math.Min(Math.Min(Levenshtein[i-1,j-1] + 1, Levenshtein[i,j-1] + 1), Levenshtein[i-1,j] + 1);
                }
            }
        }
        return Levenshtein[y-1,x-1];
    }
}
