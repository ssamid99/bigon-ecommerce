using BigOn.Domain.Business.ContactPostModule;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.Validators.ContactPostValidators
{
    public class ContactPostPostCommandValidator : AbstractValidator<ContactPostPostCommand>
    {
        public ContactPostPostCommandValidator()
        {
            RuleFor(entity => entity.Name).NotEmpty().WithMessage("Boş buraxıla bilməz!").MaximumLength(20).WithMessage("Bu hissənin uzunluğu maksimum 20 olmalıdır!");
            RuleFor(entity => entity.Subject).NotEmpty().WithMessage("Boş buraxıla bilməz!").MaximumLength(35).WithMessage("Bu hissənin uzunluğu maksimum 35 olmalıdır!");
            RuleFor(entity => entity.Email).NotEmpty().WithMessage("Boş buraxıla bilməz!").EmailAddress().WithMessage("Yanlış email adresi!").MaximumLength(45).WithMessage("Bu hissənin uzunluğu maksimum 45 olmalıdır!");
            RuleFor(entity => entity.Message).NotEmpty().WithMessage("Boş buraxıla bilməz!");
        }
    }
}
