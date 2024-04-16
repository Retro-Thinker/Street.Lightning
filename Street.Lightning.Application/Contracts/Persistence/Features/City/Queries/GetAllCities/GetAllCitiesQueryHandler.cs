using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetAllCities;

public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, 
    ResponseResult<IEnumerable<CityDto>>>
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public GetAllCitiesQueryHandler(IMapper mapper, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<IEnumerable<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAllAsync();
        var returnData = new ResponseResult<IEnumerable<CityDto>>
        {
            Data = _mapper.Map<List<CityDto>>(cities),
            Message = null,
            OperationStatus = ResultEnums.Success
        };

        return returnData;
    }
}