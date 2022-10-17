using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.Graphtypes;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Queries;

public class OwnerQuery: ObjectGraphType
{
    private readonly IAutoDatabase db;

    public OwnerQuery(IAutoDatabase context)
    {
        this.db = context;
        Field<OwnerGraphType>("owner", "Запрос получения данных о владельце",
            new QueryArguments(MakeNonNullStringArgument("email", "ФИО владельца")),
            resolve: GetOwner);
    }
    
    private Owner GetOwner(IResolveFieldContext<object> context)
    {
        var email = context.GetArgument<string>("email");
        return db.FindOwnerByEmail(email);
    }
    
    private QueryArgument MakeNonNullStringArgument(string name, string description) {
        return new QueryArgument<NonNullGraphType<StringGraphType>> {
            Name = name, Description = description
        };
    }
}