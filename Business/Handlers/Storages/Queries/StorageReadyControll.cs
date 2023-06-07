
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

    public class StorageReadyControll : IRequest<IDataResult<Storage>>
    {
        public int ProductId { get; set; }
        public bool Status { get; set; }
        public class StorageReadyControllHandler : IRequestHandler<StorageReadyControll, IDataResult<Storage>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public StorageReadyControllHandler(IStorageRepository wareHouseRepository, IMediator mediator)
            {
                _storageRepository = wareHouseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Storage>> Handle(StorageReadyControll request, CancellationToken cancellationToken)
            {
                var storageReady = await _storageRepository.GetAsync(p => p.ProductId == request.ProductId && p.Status == request.Status);
                return new SuccessDataResult<Storage>(storageReady);
            }
        }
    }
}
