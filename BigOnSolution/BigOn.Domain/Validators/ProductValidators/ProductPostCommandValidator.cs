using BigOn.Domain.Business.ProductModule;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Validators.ProductValidators
{
    public class ProductPostCommandValidator : AbstractValidator<ProductPostCommand>
    {
        public ProductPostCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Məhsul adı boş buraxıla bilməz!");
            RuleFor(p => p.StockKeepingUnit).NotEmpty().WithMessage("Məhsul SKU-i boş buraxıla bilməz!");
            RuleFor(p => p.Price).GreaterThan(0).WithMessage("Məhsul qiyməti müsbət olmalıdır");
            RuleFor(p => p.ShortDescription).NotEmpty().WithMessage("Məhsul açıqlaması boş buraxıla bilməz!");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Məhsul haqqında məlumat boş buraxıla bilməz!").MinimumLength(30).WithMessage("Məhsul haqqında məlumat 30 simvoldan az ola bilməz!");
            RuleFor(p => p.BrandId).GreaterThanOrEqualTo(1).WithMessage("Məhsul brendi seçilməyib!");
            RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(1).WithMessage("Məhsul kateqoriyası seçilməyib!");
            RuleFor(p => p.Images).Custom((list, context) =>
            {
                if (list == null)
                {
                    context.AddFailure("Şəkil seçilməyib!");
                }
                else if(list.Count(l=>l.IsMain == true) == 0)
                {
                    context.AddFailure("Əsas şəkil seçilməyib");
                }
            });
            RuleForEach(p => p.Images).ChildRules(m =>
            {
                m.RuleFor(i => i.File != null);
            });
        }
    }
}
