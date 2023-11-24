using Domain.Entities;


namespace Domain.Abstractions
{
    public interface IBlogPostRepository
    {
        Guid Insert(BlogPost blogPost);
    }
}
