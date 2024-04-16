using FluentValidation;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Commands.CreateCity;

public class CreateCityCommandValidator : 
    AbstractValidator<CreateCityCommand>
{
    private readonly ICityRepository _cityRepository;

    public CreateCityCommandValidator(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;

        RuleFor(p => p.CityName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull().WithMessage("{PropertyName} is required")
            .MinimumLength(1).WithMessage("{PropertyName} need to be at least 1 character long")
            .MaximumLength(50).WithMessage("{PropertyName} need to have less than 50 characters");

        RuleFor(p => p)
            .MustAsync(IsCityNameUnique).WithMessage("This City already exists in the database");
    }

    private async Task<bool> IsCityNameUnique(CreateCityCommand command, 
                                        CancellationToken cancellationToken)
    {
        return await _cityRepository.IsCityNameUnique(command.CityName, cancellationToken);
    }
}