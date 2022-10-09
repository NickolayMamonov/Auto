using Auto.Data.Entities;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Graphtypes;

public class ModelGraphType: ObjectGraphType<Model>
{
    public ModelGraphType() {
        Name = "model";
        Field(m => m.Name).Description("The name of this model");
        Field(m => m.Manufacturer, type:
            typeof(ManufacturerGraphType)).Description("The make of this model of car");
    }
}