
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Storages.ValidationRules;


namespace Business.Handlers.Storages.Commands
{


    public class UpdateStorageCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public int ProductId { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsReady { get; set; }

        public class UpdateStorageCommandHandler : IRequestHandler<UpdateStorageCommand, IResult>
        {
            private readonly IStorageRepository _storageRepository;
            private readonly IMediator _mediator;

            public UpdateStorageCommandHandler(IStorageRepository storageRepository, IMediator mediator)
            {
                _storageRepository = storageRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateStorageValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
            {
                var isThereStorageRecord = await _storageRepository.GetAsync(u => u.Id == request.Id);


                isThereStorageRecord.CreatedUserId = request.CreatedUserId;
                isThereStorageRecord.CreatedDate = request.CreatedDate;
                isThereStorageRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereStorageRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereStorageRecord.Status = request.Status;
                isThereStorageRecord.isDeleted = request.isDeleted;
                isThereStorageRecord.ProductId = request.ProductId;
                isThereStorageRecord.UnitsInStock = request.UnitsInStock;
                isThereStorageRecord.IsReady = request.IsReady;


                _storageRepository.Update(isThereStorageRecord);
                await _storageRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

