using BigOn.Domain.Business.BlogPostModule;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Validators.BlogPostValidators
{
    public class BlogPostPostCommandValidator : AbstractValidator<BlogPostPostCommand>
    {
        public BlogPostPostCommandValidator()
        {
            RuleFor(bp=>bp.Title).NotEmpty().WithMessage("Başlıq boş buraxıla bilməz!");
            RuleFor(bp => bp.Body).NotEmpty().WithMessage("Məzmun boş buraxıla bilməz!")
                .MinimumLength(30).WithMessage("Məzmun 30 simvoldan az ola bilməz!");
            RuleFor(bp => bp.CategoryId).GreaterThanOrEqualTo(1).WithMessage("Kategorya seçilməyib!");
            RuleFor(bp => bp.Image).NotNull().WithMessage("Şəkil seçilməyib!");
        }
    }
}
