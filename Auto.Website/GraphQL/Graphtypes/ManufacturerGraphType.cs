using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Graphtypes;

public class ManufacturerGraphType:ObjectGraphType<Manufacturer>
{
    public ManufacturerGraphType() {
        Name = "manufacturer";
        Field(c => c.Name).Description("The name of the manufacturer");
    }
}