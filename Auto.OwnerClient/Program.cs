using Auto.OwnerServer;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7138");
var grpcClient = new OwnerInfo.OwnerInfoClient(channel);
Console.WriteLine("Ready! Press any key to send a gRPC request (or Ctrl-C to quit):");

Console.WriteLine("Enter email:");
var email = Console.ReadLine();
var request = new InfoRequest
{
    Email = email
};

var reply = grpcClient.GetOwnerInfo(request);
Console.WriteLine($"Owner info: {reply.Firstname} {reply.Midname} {reply.Lastname}\nVehicle registration number:{reply.Registration}");