
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
using Business.Handlers.Products.ValidationRules;
using System;

namespace Business.Handlers.Products.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCommand : IRequest<IResult>
    {

        public int CreatedUserId { get; set; }
       
        public int LastUpdatedUserId { get; set; }
      
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }

        public string Size { get; set; }



        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = _productRepository.Query().Any(u => u.ProductName == request.ProductName);

                if (isThereProductRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProduct = new Product
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = DateTime.Now,
                    Status = request.Status,
                    isDeleted =request.isDeleted,
                    ProductName = request.ProductName,
                    ProductColor = request.ProductColor,
                    Size = request.Size,



                };

                _productRepository.Add(addedProduct);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}