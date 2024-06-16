using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.Common.Illumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Queries.GetAllProviders;

public class GetAllProvidersQueryHandler : IRequestHandler<GetAllProvidersQuery, ResponseResult<IEnumerable<IlluminationDto>>>
{
    private readonly IIlluminationRepository _illuminationRepository;
    private readonly IMapper _mapper;

    public GetAllProvidersQueryHandler(IIlluminationRepository illuminationRepository, IMapper mapper)
    {
        _illuminationRepository = illuminationRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseResult<IEnumerable<IlluminationDto>>> Handle(GetAllProvidersQuery request, CancellationToken cancellationToken)
    {
        var illuminationProviders = await _illuminationRepository.GetAllAsync();
        var returnData = new ResponseResult<IEnumerable<IlluminationDto>>
        {
            Data = _mapper.Map<IEnumerable<IlluminationDto>>(illuminationProviders),
            OperationStatus = ResultEnums.Success
        };

        return returnData;
    }
}