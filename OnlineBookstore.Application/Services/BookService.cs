using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.Interfaces;
using OnlineBookstore.Domain.Constants;
using OnlineBookstore.Infrastructure.Data;

namespace OnlineBookstore.Application.Services;

public class BookService
{
    private readonly BookstoreContext _context;
    private readonly ICacheService _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(24);

    public BookService(BookstoreContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Authors)
            .ToListAsync();
    }

    public async Task<Book?> GetBookAsync(int bookId)
    {
        var cacheKey = CacheKeys.BookDetails(bookId);
        
        // Try to get from cache first
        var cachedBook = await _cache.GetAsync<Book>(cacheKey);
        if (cachedBook != null)
            return cachedBook;

        // If not in cache, get from database
        var book = await _context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.BookId == bookId);

        // Store in cache if found
        if (book != null)
            await _cache.SetAsync(cacheKey, book, CacheDuration);

        return book;
    }

    public async Task UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        // Invalidate cache
        var cacheKey = CacheKeys.BookDetails(book.BookId);
        await _cache.RemoveAsync(cacheKey);
    }
}