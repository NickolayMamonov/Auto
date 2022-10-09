using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Graphtypes;

public class OwnerGraphType:ObjectGraphType<Owner>
{
    public OwnerGraphType() {
        Name = "owner";
        Field(c => c.Firstname, nullable:false).Description("Firstname of owner");
        Field(c => c.Midname, nullable:false).Description("Midname of owner");
        Field(c => c.Lastname, nullable:false).Description("Lastname of owner");
        Field(c => c.Email, nullable: false).Description("Email of owner");
        Field(c => c.OwnersVehicle, nullable: false, type: typeof(VehicleGraphType)).Description("Vehicle of owner");
    }
}