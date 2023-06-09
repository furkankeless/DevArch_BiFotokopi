using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Storages.Queries
{
    public class AmountControllQuery : IRequest<IDataResult<bool>>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public class AmountControllQueryHandler : IRequestHandler<AmountControllQuery, IDataResult<bool>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public AmountControllQueryHandler(IStorageRepository wareHouseRepository, IMediator mediator)
            {
                _storageRepository = wareHouseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<bool>> Handle(AmountControllQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<bool>(await _storageRepository.AmountControll(request.ProductId, request.Amount));

            }
        }
    }
}
