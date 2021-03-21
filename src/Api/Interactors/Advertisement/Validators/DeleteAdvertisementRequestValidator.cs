﻿using AluguelIdeal.Api.Interactors.Advertisement.Request;
using FluentValidation;

namespace AluguelIdeal.Api.Interactors.Advertisement.Validators
{
    public class DeleteAdvertisementRequestValidator : AbstractValidator<DeleteAdvertisementRequest>
    {
        public DeleteAdvertisementRequestValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0);

        }
    }
}
