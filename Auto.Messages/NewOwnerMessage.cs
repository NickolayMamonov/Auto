namespace Auto.Messages;

public class NewOwnerMessage
{
    public string Firstname { get; set; }
    public string Midname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string OwnersVehicle { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
        
}