using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Products.Queries
{
    public class GetSizeQuery : IRequest<IDataResult<bool>>
    {
        public int ProductId { get; set; }

        public string Size { get; set; }


        public class GetSizeQueryHandler : IRequestHandler<GetSizeQuery, IDataResult<bool>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public GetSizeQueryHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<bool>> Handle(GetSizeQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<bool>(await _productRepository.GetSize(request.ProductId, request.Size));


            }


        }
    }
}
