using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Street.Lightning.DTO.Features.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCityDailyUsage;

public class GetCityDailyPowerUsageQueryHandler : IRequestHandler<GetCityDailyPowerUsageQuery, ResponseResult<DailyPowerUsageDto>>
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;
    private readonly IConfiguration _configuration;

    public GetCityDailyPowerUsageQueryHandler(IMapper mapper, ICityRepository cityRepository,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
        _configuration = configuration;
    }
    
    public async Task<ResponseResult<DailyPowerUsageDto>> Handle(GetCityDailyPowerUsageQuery request, CancellationToken cancellationToken)
    {
        var cityWithIlluminationDetails = await _cityRepository
            .GetCityWithIlluminationDetails(request.cityId);

        var sunsetAPI = _configuration.GetConnectionString("API");

        throw new NotImplementedException();
    }
}