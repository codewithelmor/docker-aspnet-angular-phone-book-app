using FluentValidation;
using phone_book_app.Server.InputModels;

namespace phone_book_app.Server.Validators
{
    public class ContactInputModelValidator : AbstractValidator<ContactInputModel>
    {
        public ContactInputModelValidator()
        {
            RuleFor(x => x.GivenName)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(20);

            RuleFor(x => x.FamilyName)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(20);

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .Matches("^\\+639[0-9]{9}$")
                .MaximumLength(20);

            RuleFor(x => x.Label)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(20);

            When(x => !string.IsNullOrWhiteSpace(x.BirthDate), () => {
                RuleFor(x => x.BirthDate)
                    .NotEmpty()
                    .Matches("^[0-9]{4}\\-[0-9]{2}\\-[0-9]{2}$")
                    .MaximumLength(20);
            });
        }
    }
}
