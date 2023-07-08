using BigOn.Domain.Business.BlogPostModule;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Validators.BlogPostValidators
{
    public class BlogPostPutCommandValidator : AbstractValidator<BlogPostPutCommand>
    {
        public BlogPostPutCommandValidator()
        {
            RuleFor(bp => bp.Title).NotEmpty().WithMessage("Başlıq boş buraxıla bilməz!");
            RuleFor(bp => bp.Body).NotEmpty().WithMessage("Məzmun boş buraxıla bilməz!")
                .MinimumLength(30).WithMessage("Məzmun 30 simvoldan az ola bilməz!");
        }
    }
}
