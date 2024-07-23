using Grpc.Core;

namespace GrpcService1.Services;

public class SumerService : Sumer.SumerBase
{
    public override Task<SumReply> Getsumer(SumRequest request, ServerCallContext context)
    {
        return Task.FromResult(new SumReply
        {
            Result = request.Name + request.B
        });
    }
}