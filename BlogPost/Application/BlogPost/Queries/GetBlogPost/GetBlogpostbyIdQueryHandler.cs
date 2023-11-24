using MediatR;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace Application.BlogPost.Queries.GetBlogPost
{
    public sealed class GetBlogpostbyIdQueryHandler : IRequestHandler<GetBlogPostbyIdQuery, Domain.Entities.BlogPost>
    {       

        public async Task<Domain.Entities.BlogPost> Handle(GetBlogPostbyIdQuery request, CancellationToken cancellationToken)
        {
            string connectionString = "your_connection_string";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT 
                        bp.*,
                        bc.*
                    FROM 
                        BlogPost bp
                    LEFT JOIN 
                        BlogComment bc ON bp.Id = bc.PostId
                    WHERE 
                        bp.Id = @Id";

                var result = connection.Query<Domain.Entities.BlogPost,
                    Domain.Entities.BlogComment, Domain.Entities.BlogPost>(
                    sql,
                    (post, comment) =>
                    {
                        if (comment != null)
                            post.Comments.Add(comment);
                        return post;
                    },
                    new { Id = request.Id },
                    splitOn: "PostId"
                );

                var blogPost = result.FirstOrDefault();

                if (blogPost is null)
                {
                    throw new Exception("BlogPost not Found");
                }

                 return blogPost;
             }
        
        }
    }
}
