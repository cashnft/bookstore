namespace OnlineBookstore.Domain.Constants;

public static class CacheKeys
{
    private const string Prefix = "bookstore:";
    
    public static string BookDetails(int bookId) => $"{Prefix}book:{bookId}";
    public static string AuthorDetails(int authorId) => $"{Prefix}author:{authorId}";
    public static string UserSession(string userId) => $"{Prefix}session:{userId}";
    public static string ShoppingCart(string userId) => $"{Prefix}cart:{userId}";
}