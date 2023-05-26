
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
namespace Business.Handlers.WareHouses.Queries
{

    public class ExistsProductQuery : IRequest<IDataResult<bool>>
    {
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Amount { get; set; }
        public class ExistsProductQueryHandler : IRequestHandler<ExistsProductQuery, IDataResult<bool>>
        {
            private readonly IStorageRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public ExistsProductQueryHandler(IStorageRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<bool>> Handle(ExistsProductQuery request, CancellationToken cancellationToken)
            {

                return new SuccessDataResult<bool>(await _wareHouseRepository.ExistsProduct(request.ProductId, request.Size, request.Amount));
            }
        }
    }
}
