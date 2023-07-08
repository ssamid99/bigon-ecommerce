using BigOn.Domain.Business.BrandModule;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Validators.BrandValidators
{
    public class BrandCreateCommandValidator : AbstractValidator<BrandPostCommand>
    {
        public BrandCreateCommandValidator()
        {
            RuleFor(b=>b.name).NotEmpty().WithMessage("Ad boş buraxıla bilməz!")
                .MinimumLength(2).WithMessage("Ad ən azı 2 simvoldan ibarət olmalıdır!");
        }
    }
}
