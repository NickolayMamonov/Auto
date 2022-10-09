using Auto.Data;
using Auto.Data.Entities;
using Auto.Website.GraphQL.Graphtypes;
using GraphQL;
using GraphQL.Types;

namespace Auto.Website.GraphQL.Queries;

public class OwnerMutation: ObjectGraphType
{
    private readonly IAutoDatabase db;

    public OwnerMutation(IAutoDatabase db)
    {
        this.db = db;

        Field<OwnerGraphType>("createOwner", "Создание нового владельца", arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Firstname"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Midname"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Lastname"},
                new QueryArgument<NonNullGraphType<StringGraphType>>{Name = "Email"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Registration"}
            ),
            resolve: ownerContex =>
            {
                var firstname = ownerContex.GetArgument<string>("Firstname");
                var midname = ownerContex.GetArgument<string>("Midname");
                var lastname = ownerContex.GetArgument<string>("Lastname");
                var email = ownerContex.GetArgument<string>("Email");
                var registration = ownerContex.GetArgument<string>("Registration");

                var newVehicle = db.FindVehicle(registration);

                var newOwner = new Owner()
                {
                    Firstname = firstname,
                    Midname = midname,
                    Lastname = lastname,
                    Email = email,
                    OwnersVehicle = newVehicle
                };
                
                db.CreateOwner(newOwner);
                
                return new Owner
                {
                    Firstname = firstname,
                    Midname = midname,
                    Lastname = lastname
                };
            });

        Field<OwnerGraphType>("updateOwner", "Обновление владельца", arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Firstname"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Midname"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Lastname"},
                new QueryArgument<NonNullGraphType<StringGraphType>>{Name = "Email"},
                new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Registration"}
            ),resolve: ownerContex =>
            {
                var firstname = ownerContex.GetArgument<string>("Firstname");
                var midname = ownerContex.GetArgument<string>("Midname");
                var lastname = ownerContex.GetArgument<string>("Lastname");
                var email = ownerContex.GetArgument<string>("Email");
                var registration = ownerContex.GetArgument<string>("Registration");
                
                var newVehicle = db.FindVehicle(registration);

                var updateOwner = new Owner()
                {
                    Firstname = firstname,
                    Midname = midname,
                    Lastname = lastname,
                    Email = email,
                    OwnersVehicle = newVehicle
                };
                
                db.UpdateOwner(updateOwner,email);
                
                
                return updateOwner;
            });
        
        Field<OwnerGraphType>("deleteOwner", "Удаление владельца", arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Firstname"},
            new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Midname"},
            new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Lastname"},
            new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "Registration"}
        ),resolve: ownerContex =>
        {
            var firstname = ownerContex.GetArgument<string>("Firstname");
            var midname = ownerContex.GetArgument<string>("Midname");
            var lastname = ownerContex.GetArgument<string>("Lastname");
            var registration = ownerContex.GetArgument<string>("Registration");
                
            var newVehicle = db.FindVehicle(registration);

            var deleteOwner = new Owner()
            {
                Firstname = firstname,
                Midname = midname,
                Lastname = lastname,
                OwnersVehicle = newVehicle
            };
                
            db.DeleteOwner(deleteOwner);
                
                
            return deleteOwner;
        });

    }
}