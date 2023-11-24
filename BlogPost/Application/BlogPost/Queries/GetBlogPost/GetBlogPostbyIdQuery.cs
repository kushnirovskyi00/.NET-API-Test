using MediatR;

namespace Application.BlogPost.Queries.GetBlogPost;

public sealed record GetBlogPostbyIdQuery(Guid Id) : IRequest<Domain.Entities.BlogPost>;


