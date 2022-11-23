using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator: AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş olamaz");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soy adı boş olamaz");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Yazar Hakkında boş olamaz");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Yazar Unvanı boş olamaz");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("En az 2 karakter olması lazım");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("En fazla 50 karakter olması lazım");
        }
    }
}
