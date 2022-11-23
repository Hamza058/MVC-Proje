using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail alanını boş geçemezsiniz");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu adı boş olamaz");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adını boş olamaz");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("En az 3 karakter olması lazım");
            RuleFor(x => x.UserName).MaximumLength(20).WithMessage("En fazla 20 karakter olması lazım");
            RuleFor(x => x.Subject).MaximumLength(50).WithMessage("En fazla 50 karakter olması lazım");
        }
    }
}
