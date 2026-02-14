using Auth.Grpc;
using ProtoBuf.Grpc;

namespace Auth.API.Features.Persons;

public class PersonGrpcService : IPersonService
{
    public ValueTask<GetPersonResponse> GetPersonByUserIdAsync(GetPersonByUserIdRequest request, CallContext context = default)
    {
        throw new NotImplementedException();
    }
}