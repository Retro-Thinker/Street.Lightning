using System.Text;
using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Commands.CreateCity;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, ResponseResult<Unit>>
{
    private readonly ICityRepository _cityRepository;

    public CreateCityCommandHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<Unit>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCityCommandValidator(_cityRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        var returnData = new ResponseResult<Unit>();

        if (!validationResult.IsValid)
        {
            var sb = new StringBuilder();
            
            foreach (var error in validationResult.Errors)
            {
                sb.Append(error + "\n");
            }
            
            returnData.Data = Unit.Value;
            returnData.Message = sb.ToString();
            returnData.OperationStatus = ResultEnums.InternalError;

            return returnData;
        }
        
        var newCity = new Domain.City
        {
            CityName = request.CityName,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            CountryId = request.CountryId
        };

        var response = await _cityRepository.CreateAsync(newCity);

        returnData.Data = Unit.Value;
        returnData.OperationStatus = ResultEnums.Success;

        return returnData;
    }
}