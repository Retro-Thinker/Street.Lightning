using AutoMapper;
using MediatR;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Commands.CreateIlluminationProvider;

public class CreateIlluminationProviderCommandHandler : IRequestHandler<CreateIlluminationProviderCommand, ResponseResult<Unit>>
{
    private readonly IIlluminationRepository _illuminationRepository;
    private readonly IMapper _mapper;

    public CreateIlluminationProviderCommandHandler(IIlluminationRepository illuminationRepository, IMapper mapper)
    {
        _illuminationRepository = illuminationRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseResult<Unit>> Handle(CreateIlluminationProviderCommand request, CancellationToken cancellationToken)
    {
        var returnData = new ResponseResult<Unit>();
        var newIlluminationProvider = _mapper.Map<Domain.Illumination>(request);

        var response = await _illuminationRepository.CreateAsync(newIlluminationProvider);

        returnData.Data = Unit.Value;
        returnData.OperationStatus = ResultEnums.Success;

        return returnData;
    }
}