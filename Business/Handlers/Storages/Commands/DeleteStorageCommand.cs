
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Storages.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteStorageCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteStorageCommandHandler : IRequestHandler<DeleteStorageCommand, IResult>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public DeleteStorageCommandHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteStorageCommand request, CancellationToken cancellationToken)
            {
                var storageToDelete = _storageRepository.Get(p => p.Id == request.Id);

                _storageRepository.Delete(storageToDelete);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

