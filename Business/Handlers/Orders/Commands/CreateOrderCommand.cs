
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Orders.ValidationRules;
using System;
using Business.Handlers.Storages.Commands;
using Business.Handlers.WareHouses.Queries;
using DataAccess.Concrete.EntityFramework;

namespace Business.Handlers.Orders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderCommand : IRequest<IResult>
    {

        public int CreatedUserId { get; set; }
       
        public int LastUpdatedUserId { get; set; }
       
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Size { get; set; }


        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;
            public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {




                var storageControll = _orderRepository.Query().Any(P=>P.ProductId == request.ProductId && request.Status==true && request.isDeleted==false);

                if (storageControll != true)
                {
                    return new ErrorResult("Siparişiniz satışa uygun değildir");
                }

                var isOkey = await _mediator.Send(new StorageReadyControll { ProductId=request.ProductId,Status=request.Status});

                if (isOkey.Data.ProductId != request.ProductId )
                {
                    return new ErrorResult("Sipariş etmek istediğniz ürün depoda bulanamamıştır");

                }


                var storageRecord = await _mediator.Send(new GetWareHouseByProductIdAndSizeQuery { ProductId = request.ProductId, Size = request.Size });

                storageRecord.Data.UnitsInStock = storageRecord.Data.UnitsInStock - request.Amount;

                var updatedWareHouse = await _mediator.Send(new UpdateStorageCommand
                {
                    UnitsInStock = storageRecord.Data.UnitsInStock,
                    CreatedDate = storageRecord.Data.CreatedDate,
                    CreatedUserId = storageRecord.Data.CreatedUserId,
                    Id = storageRecord.Data.Id,
                    isDeleted = storageRecord.Data.isDeleted,
                    Status = storageRecord.Data.UnitsInStock != 0,
                    IsReady = storageRecord.Data.IsReady,
                    LastUpdatedDate = storageRecord.Data.LastUpdatedDate,
                    LastUpdatedUserId = storageRecord.Data.LastUpdatedUserId,
                    ProductId = storageRecord.Data.ProductId,
                    
                });

                if (updatedWareHouse.Success)
                {
                    var addedOrder = new Order
                    {
                        CreatedUserId = request.CreatedUserId,
                        CreatedDate = DateTime.Now,
                        LastUpdatedUserId = request.LastUpdatedUserId,
                        LastUpdatedDate = DateTime.Now,
                        Status = request.Status,
                        isDeleted = request.isDeleted,
                        CustomerId = request.CustomerId,
                        ProductId = request.ProductId,
                        Size = request.Size,
                        Amount = request.Amount,

                    };
                    _orderRepository.Add(addedOrder);
                    await _orderRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Added);
                }
                return new ErrorResult(updatedWareHouse.Message);

           


            }












        
        }
    }
}