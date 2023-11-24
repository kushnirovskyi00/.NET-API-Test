using Domain.Premitives;

namespace Domain.Entities
{
    public sealed class BlogComment : Entity
    {
        public BlogComment(Guid id, string content) {
            Content = content;
        }

        public string Content { get; private set;}
    }
}
