

/*
dotnet add package SharpGrip.FluentValidation.AutoValidation.Mvc --version 1.5.0
dotnet add package FluentValidation.DependencyInjectionExtensions
*/

using System.Globalization;
using Application.ViewModels;
using FluentValidation;

namespace Application.Validators
{
    public class ActivityCommandValidator : AbstractValidator<ActivityCommandViewModel>
    {
        public ActivityCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description cannot be empty");
            RuleFor(x => x.Venue).NotNull().NotEmpty().WithMessage("Venue cannot be empty");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City cannot be empty");
            RuleFor(x => x.Category).NotNull().NotEmpty().WithMessage("Category cannot be empty");
            RuleFor(x => x.Venue).NotNull().NotEmpty().WithMessage("Venue cannot be empty");
            RuleFor(x => x.EventDate).Custom((ed, vc) =>
            {
                var eventDate = DateTime.ParseExact(ed.Trim('Z'), "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
                if (eventDate.DayOfYear < DateTime.UtcNow.DayOfYear)
                {
                    vc.AddFailure("Event date cannot be less than current date");
                }
            });
        }
    }
}