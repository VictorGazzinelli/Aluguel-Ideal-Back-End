using AluguelIdeal.Application.Interactors.Districts.Queries;
using AluguelIdeal.Application.Repositories;
using FluentValidation;
using System;

namespace AluguelIdeal.Application.Interactors.Districts.Validators
{
    public class GetDistrictsQueryValidator : AbstractValidator<GetDistrictsQuery>
    {
        public GetDistrictsQueryValidator(ICityRepository cityRepository)
        {
            RuleFor(query => query.CityId)
                .MustAsync(async (cityId, cancellationToken) => ( await cityRepository.GetByIdAsync((Guid)cityId , cancellationToken)) != null)
                .WithMessage((query, cityId) => $"Invalid {nameof(query.CityId)}")
                .When(query => query.CityId != null);
        }
    }
}
