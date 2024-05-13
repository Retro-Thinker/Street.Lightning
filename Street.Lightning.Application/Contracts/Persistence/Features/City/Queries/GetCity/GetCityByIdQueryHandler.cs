using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.Common.City;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.City.Queries.GetCity;

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, ResponseResult<CityDto>>
{
    private readonly IMapper _mapper;
    private readonly ICityRepository _cityRepository;

    public GetCityByIdQueryHandler(IMapper mapper, ICityRepository cityRepository)
    {
        _mapper = mapper;
        _cityRepository = cityRepository;
    }
    
    public async Task<ResponseResult<CityDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.Id, "Country");
        var dataResult = new ResponseResult<CityDto>
        {
            Data = _mapper.Map<CityDto>(city),
            Message = null,
            OperationStatus = ResultEnums.Success
        };

        return dataResult;
    }
}