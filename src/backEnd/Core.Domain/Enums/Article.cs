using System.Reflection.Metadata.Ecma335;

namespace Core.Domain.Enums;

public enum Article
{
    Article33 = 33,
    Article34 = 34,
    Article35 = 35,
}

public static class ArticleExtension
{
    public static IEnumerable<Article> GetAll() => Enum.GetValues<Article>().Cast<Article>();

    public static string GetName(this Article article)
    {
        return "Articulo " + (int)article;
    }
}
