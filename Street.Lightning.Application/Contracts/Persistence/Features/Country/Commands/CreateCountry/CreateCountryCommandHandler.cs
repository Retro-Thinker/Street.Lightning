using MediatR;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Country.Commands.CreateCountry;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, ResponseResult<Unit>>
{
    private readonly ICountryRepository _countryRepository;

    public CreateCountryCommandHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    
    public async Task<ResponseResult<Unit>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var returnData = new ResponseResult<Unit>();

        var newCountry = new Domain.Country()
        {
            CountryName = request.CountryName
        };

        var response = await _countryRepository.CreateAsync(newCountry);

        returnData.Data = Unit.Value;
        returnData.OperationStatus = ResultEnums.Success;

        return returnData;
    }
}