using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.Common.CityIllumination;
using Street.Lightning.DTO.Features.Common.Illumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.CityIllumination.Queries.GetAllCityIlluminations;

public class GetAllCityIlluminationsQueryHandler : IRequestHandler<GetAllCityIlluminationsQuery, ResponseResult<IEnumerable<CityIlluminationDto>>>
{
    private readonly ICityIlluminationDetailsRepository _cityIlluminationDetailsRepository;
    private readonly IMapper _mapper;

    public GetAllCityIlluminationsQueryHandler(ICityIlluminationDetailsRepository cityIlluminationDetailsRepository , IMapper mapper)
    {
        _cityIlluminationDetailsRepository = cityIlluminationDetailsRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseResult<IEnumerable<CityIlluminationDto>>> Handle(GetAllCityIlluminationsQuery request, CancellationToken cancellationToken)
    {
        var cityIlluminationsConnections = await _cityIlluminationDetailsRepository.GetAllAsync();
        var returnData = new ResponseResult<IEnumerable<CityIlluminationDto>>
        {
            Data = _mapper.Map<IEnumerable<CityIlluminationDto>>(cityIlluminationsConnections),
            OperationStatus = ResultEnums.Success
        };

        return returnData;
    }
}