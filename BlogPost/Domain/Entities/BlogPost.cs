using Domain.Premitives;

namespace Domain.Entities
{
    public sealed class BlogPost : Entity
    {
        public BlogPost(Guid id, string blogTitle,string author, string content)
        {
            BlogTitle=blogTitle;
            Author=author;
            Content=content;
            Comments = new List<BlogComment>();
        }

        public string BlogTitle { get; private set; }
        public string Author { get; private set; }

        public string Content { get; private set; }

        public List<BlogComment> Comments { get; private set; }
    }
}
