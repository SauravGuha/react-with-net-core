

using System.Globalization;
using Application.ViewModels;
using FluentValidation;

namespace Application.Validators
{
    public class ActivityCommandValidator : AbstractValidator<ActivityCommandViewModel>
    {
        public ActivityCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();//.WithMessage("Title cannot be empty");
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Venue).NotNull().NotEmpty();
            RuleFor(x => x.City).NotNull().NotEmpty();
            RuleFor(x => x.Category).NotNull().NotEmpty();
            RuleFor(x => x.EventDate).Custom((ed, vc) =>
            {
                var eventDate = DateTime.ParseExact(ed.Trim('Z'), "yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
                if (eventDate.Ticks < DateTime.UtcNow.Ticks)
                {
                    vc.AddFailure($"'{nameof(ActivityCommandViewModel.EventDate)}' cannot be less than or equal to current date");
                }
            });
            RuleFor(x => x.Latitude)
            .NotNull().NotEmpty()
            .InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude)
            .NotNull().NotEmpty()
            .InclusiveBetween(-180, 180);
        }
    }
}