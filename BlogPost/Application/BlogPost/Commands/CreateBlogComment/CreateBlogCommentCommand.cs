using MediatR;

namespace Application.BlogPost.Commands.CreateBlogComment;
public sealed record CreateBlogCommentCommand(string Content) : IRequest<Guid>;
