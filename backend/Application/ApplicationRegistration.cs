

using Application.Activities.Query;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection sc)
        {
            sc.AddAutoMapper(cfg => { }, typeof(ActivityQueryRequest).Assembly);
            sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ActivityQueryRequest).Assembly));
            sc.AddValidatorsFromAssemblyContaining<ActivityCommandValidator>();
            sc.AddFluentValidationAutoValidation();
            return sc;
        }
    }
}