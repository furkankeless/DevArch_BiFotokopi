
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Storages.Queries
{

    public class GetStoragesQuery : IRequest<IDataResult<IEnumerable<Storage>>>
    {
        public class GetStoragesQueryHandler : IRequestHandler<GetStoragesQuery, IDataResult<IEnumerable<Storage>>>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public GetStoragesQueryHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Storage>>> Handle(GetStoragesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Storage>>(await _storageRepository.GetListAsync());
            }
        }
    }
}