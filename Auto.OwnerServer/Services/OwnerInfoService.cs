using Auto.Data;
using Grpc.Core;

namespace Auto.OwnerServer.Services;

public class OwnerInfoService : OwnerInfo.OwnerInfoBase
{
    private readonly ILogger<OwnerInfoService> _logger;
    private readonly IAutoDatabase _db;
    public OwnerInfoService(ILogger<OwnerInfoService> logger,IAutoDatabase db) {
        _logger = logger;
        _db = db;
    }
    public override Task<InfoReply> GetOwnerInfo(InfoRequest request, ServerCallContext context) {
        var owner = _db.FindOwnerByEmail(request.Email);
        return Task.FromResult(new InfoReply() {Firstname = owner.Firstname, Midname = owner.Midname,Lastname = owner.Lastname, Registration = owner.OwnersVehicle.Registration});
    }
}
