
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
using Business.Handlers.Customers.ValidationRules;


namespace Business.Handlers.Customers.Commands
{


    public class UpdateCustomerCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public string CustomerName { get; set; }
        public int CustomerCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateCustomerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerRecord = await _customerRepository.GetAsync(u => u.Id == request.Id);


                isThereCustomerRecord.CreatedUserId = request.CreatedUserId;
                isThereCustomerRecord.CreatedDate = request.CreatedDate;
                isThereCustomerRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereCustomerRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereCustomerRecord.Status = request.Status;
                isThereCustomerRecord.isDeleted = request.isDeleted;
                isThereCustomerRecord.CustomerName = request.CustomerName;
                isThereCustomerRecord.CustomerCode = request.CustomerCode;
                isThereCustomerRecord.Address = request.Address;
                isThereCustomerRecord.Phone = request.Phone;
                isThereCustomerRecord.Email = request.Email;


                _customerRepository.Update(isThereCustomerRecord);
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

