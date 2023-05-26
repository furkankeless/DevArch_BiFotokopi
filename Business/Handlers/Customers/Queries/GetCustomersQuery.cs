
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

namespace Business.Handlers.Customers.Queries
{

    public class GetCustomersQuery : IRequest<IDataResult<IEnumerable<Customer>>>
    {
        public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IDataResult<IEnumerable<Customer>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Customer>>(await _customerRepository.GetListAsync());
            }
        }
    }
}