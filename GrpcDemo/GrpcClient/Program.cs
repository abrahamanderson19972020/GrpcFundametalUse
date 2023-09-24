using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

Console.WriteLine("Hello World");

//HelloRequest request = new HelloRequest()
//{
//    Name = "Prophet Mohammad"
//};
//var channel = GrpcChannel.ForAddress("https://localhost:7030");
////Grpc Service Call to create client that will use this communication channel
//var client = new Greeter.GreeterClient(channel);
//var reply = await client.SayHelloAsync(request);
//Console.WriteLine(reply);
//Console.WriteLine(reply.Message);
CustomerRequest request = new CustomerRequest()
{
    UserId = 3
};
var channel = GrpcChannel.ForAddress("https://localhost:7030");
var client = new Customer.CustomerClient(channel);
var response = client.GetCustomerInfo(request);
Console.WriteLine(response.ToString());
Console.WriteLine($"Customer Id:{request.UserId} Customer Name: {response.FirstName} {response.LastName} with age {response.Age} and address: {response.Address}. Is alive: {response.IsAlive}");
Console.WriteLine("..................Customer List.................");
List<CustomerResponse> responseList = new List<CustomerResponse>();
using (var call = client.GetAllCustomers(new AllCustomersRequest()))
{
    while(await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"Customer Id:{currentCustomer.CustomerId} Customer Name: {currentCustomer.FirstName} {currentCustomer.LastName} with age {currentCustomer.Age} and address: {currentCustomer.Address}. Is alive: {currentCustomer.IsAlive}");
        responseList.Add(currentCustomer);
    }
}
Console.ReadLine();