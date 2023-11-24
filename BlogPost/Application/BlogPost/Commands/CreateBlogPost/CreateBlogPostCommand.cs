using MediatR;

namespace Application.BlogPost.Commands.CreateBlogPost;
public sealed record CreateBlogPostCommand(string BlogTitle,string Author, string Content) : IRequest<Guid>;
