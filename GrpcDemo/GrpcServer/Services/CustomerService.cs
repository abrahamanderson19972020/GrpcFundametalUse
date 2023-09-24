using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        public readonly ILogger<CustomerService> _logger;
        List<CustomerResponse> customers = new List<CustomerResponse>()
        {
            new CustomerResponse()
            {
                 FirstName = "Abraham",
                 LastName = "Anderson",
                 Address = "Kristiansand/Norway",
                 Age = 39,
                 Email = "abraham@gmail.com",
                 IsAlive = true,
                 CustomerId = 1
            },
            new CustomerResponse()
            {
                 FirstName = "Karen",
                 LastName = "Anderson",
                 Address = "Kristiansand/Norway",
                 Age = 35,
                 Email = "karen@gmail.com",
                 IsAlive = true,
                 CustomerId = 2
            },
            new CustomerResponse()
            {
                 FirstName = "Naim",
                 LastName = "Anderson",
                 Address = "Kristiansand/Norway",
                 Age = 11,
                 Email = "naim@gmail.com",
                 IsAlive = true,
                 CustomerId = 3
            },
            new CustomerResponse()
            {
                 FirstName = "Pinar",
                 LastName = "Anderson",
                 Address = "Kristiansand/Norway",
                 Age = 7,
                 Email = "pinar@gmail.com",
                 IsAlive = true,
                 CustomerId = 4
            },
        };
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerResponse> GetCustomerInfo(CustomerRequest request, ServerCallContext context)
        {
            CustomerResponse customerResponse = new CustomerResponse();

            if(request.UserId == 1)
            {
                customerResponse = customers.FirstOrDefault(c => c.CustomerId == 1);
            }
            else if(request.UserId == 2)
            {
                customerResponse = customers.FirstOrDefault(c => c.CustomerId == 2);
            }
            else  if(request.UserId==3)
            {
                customerResponse = customers.FirstOrDefault(c => c.CustomerId == 3);
            }
            else
            {
                customerResponse = customers.FirstOrDefault(c => c.CustomerId == 4);
            }
            _logger.LogInformation("Customer Response is ok:", customerResponse.ToString());
            return Task.FromResult(customerResponse);
        }

        public override async Task GetAllCustomers(AllCustomersRequest request, IServerStreamWriter<CustomerResponse> responseStream, ServerCallContext context)
        {
           foreach(CustomerResponse customer in customers)
            {
                await Task.Delay(2000);
                    await responseStream.WriteAsync(customer);
            }
        }

    }
}
