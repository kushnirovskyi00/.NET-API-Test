using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ICommentBlogRepository
    {
        Guid Insert(BlogComment blogComment);
    }
}
