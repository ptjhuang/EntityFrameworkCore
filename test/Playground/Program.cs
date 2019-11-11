using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}

public class BloggingContext : DbContext
{
    //private static readonly ILoggerFactory Logger = LoggerFactory.Create(x => x.AddConsole());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().ToTable("Blog", "s1");
        modelBuilder.Entity<Post>()
            .ToTable("Post", "s2")
            .HasQueryFilter(p => p.Blog.Id == 0);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Debug)
            //.UseLoggerFactory(Logger)
            //.UseInMemoryDatabase(@"Test");
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0");
}

public class Program
{
    public static void Main()
    {
        using (var context = new BloggingContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Blog
            {
                Posts = new List<Post>
                {
                    new Post(),
                    new Post()
                }
            });

            context.SaveChanges();
        }

        using (var context = new BloggingContext())
        {
            var blogs = context.Set<Blog>().Include(e => e.Posts).ToList();
//            var posts = await context.Set<Post>().Include(e => e.Blog).ToListAsync();
        }

//        using (var context = new BloggingContext())
//        {
//            var blogs = await context.Set<Blog>().Include(e => e.Posts).ToListAsync();
//            var posts = await context.Set<Post>().Include(e => e.Blog).ToListAsync();
//        }
    }

}
