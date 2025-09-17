using EcoInspira.Communication.Requests;
using EcoInspira.Exceptions;
using FluentValidation;

namespace EcoInspira.Application.UseCases.Post
{
    public class PostValidator : AbstractValidator<RequestPostJson>
    {
       public PostValidator()
        {
            RuleFor(post => post.Title).NotEmpty().WithMessage(ResourceMessagesException.POST_TITLE_EMPTY);
            RuleForEach(post => post.Comments).ChildRules(commentRules =>
            {
                commentRules.RuleFor(comment => comment.Content).NotEmpty().WithMessage(ResourceMessagesException.COMMENT_EMPTY);
            });
        }
    }
}
