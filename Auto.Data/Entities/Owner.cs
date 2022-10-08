using Auto.Data.Entities;

namespace Auto.Data.Entities
{
    public partial class Owner
    {

        public string Firstname { get; set; }

        public string Midname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Vehicle OwnersVehicle { get; set; }


    }
}