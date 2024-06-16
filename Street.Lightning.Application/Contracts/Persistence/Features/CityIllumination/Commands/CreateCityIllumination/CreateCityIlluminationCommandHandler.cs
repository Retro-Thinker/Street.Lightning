using AutoMapper;
using MediatR;
using Street.Lightning.Domain;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Commands.CreateCityIllumination;

public class CreateCityIlluminationCommandHandler : IRequestHandler<CreateCityIlluminationCommand, ResponseResult<Unit>>
{
    private readonly ICityIlluminationDetailsRepository _cityIlluminationDetailsRepository;
    private readonly IMapper _mapper;

    public CreateCityIlluminationCommandHandler(ICityIlluminationDetailsRepository cityIlluminationDetailsRepository, IMapper mapper)
    {
        _cityIlluminationDetailsRepository = cityIlluminationDetailsRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseResult<Unit>> Handle(CreateCityIlluminationCommand request, CancellationToken cancellationToken)
    {
        var returnData = new ResponseResult<Unit>();
        var newCityIllumination = _mapper.Map<CityIlluminationDetails>(request);

        var response = await _cityIlluminationDetailsRepository.CreateAsync(newCityIllumination);
        
        returnData.Data = Unit.Value;
        returnData.OperationStatus = ResultEnums.Success;

        return returnData;
    }
}