using MediatR;
using Street.Lightning.DTO.Features.Common.Illumination;
using Street.Lightning.DTO.Features.ResponseResult;

namespace Street.Lightning.Application.Contracts.Persistence.Features.Illumination.Queries.GetAllProviders;

public record GetAllProvidersQuery : IRequest<ResponseResult<IEnumerable<IlluminationDto>>>;