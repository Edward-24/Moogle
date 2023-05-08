namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query) {
        Library.Clean();
        if (query == "")
        {
            SearchItem[] items = new SearchItem[1];
            items[0] = new SearchItem("Ingrese un valor para buscar", "" , 0);
            return new SearchResult(items, query);
        }
        else
        {
            Search.query = query.ToLower();
            Search search = new Search();
            Suggestion suggestion = new Suggestion();
            if (Search.OrdDocs.Count == 0)
            {
                SearchItem[] items = new SearchItem[1];
                items[0] = new SearchItem("La busqueda no coincide con ningun documento", "", 0);
                return new SearchResult(items, Suggestion.Suggest);
            }
            else {
                int r = Math.Min(Search.OrdDocs.Count, 5);
                SearchItem[] items = new SearchItem[r];
                for (int i = 0; i < r; i++)
                {
                    items[i] = new SearchItem(Search.OrdDocs[i].Title, Search.OrdDocs[i].Snippet, Search.OrdDocs[i].Score);
                }
                return new SearchResult(items, Suggestion.Suggest);
            }   
        }
    }
}
