using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.Common.Country;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Country.Queries.GetAllCountries;

public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, ResponseResult<IEnumerable<CountryDto>>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;


    public GetAllCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseResult<IEnumerable<CountryDto>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetAllAsync();
        var returnData = new ResponseResult<IEnumerable<CountryDto>>
        {
            Data = _mapper.Map<IEnumerable<CountryDto>>(countries),
            Message = null,
            OperationStatus = ResultEnums.Success
        };

        return returnData;
    }
}