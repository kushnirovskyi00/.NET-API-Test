using Domain.Abstractions;
using MediatR;



namespace Application.BlogPost.Commands.CreateBlogComment
{
    internal sealed class CreateBlogCommentCommandHandler : IRequestHandler<CreateBlogCommentCommand, Guid>
    {
        ICommentBlogRepository _iCommentBlogRepository;

        public CreateBlogCommentCommandHandler(ICommentBlogRepository iCommentBlogRepository)
        {
            _iCommentBlogRepository = iCommentBlogRepository;
        }

        public Task<Guid> Handle(CreateBlogCommentCommand request, CancellationToken cancellationToken)
        {
            var BlogComment = _iCommentBlogRepository.Insert(new Domain.Entities.BlogComment(Guid.NewGuid(), request.Content));
            return Task.FromResult(BlogComment);
        }
    }


}
