namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Device 
{
    public Guid Id { get; }
    public string Name { get; }
    public string DeviceType { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; private set; } 
    public DateTime UpdatedAt { get; private set; } 

    public Device(string name, string deviceType, string description)
    {
        if (name.Length is < 1 or > 100)
            throw new ArgumentException("Name must be between 1 and 100 characters long");

        if (deviceType.Length is < 1 or > 100)
            throw new ArgumentException("DeviceType must be between 1 and 100 characters long");

        if (description.Length is 500)
            throw new ArgumentException("Description must be under 500 characters long");

        Id = Guid.NewGuid();
        Name = name;
        DeviceType = deviceType;
        Description = description;
        
        var dateTimeNow = DateTime.Now;
        CreatedAt = dateTimeNow;
        UpdatedAt = dateTimeNow;
    }
    
    public bool UpdateDevice(Guid deviceId, string? name = null, string? deviceType = null, string? description = null)
    {
        throw new NotImplementedException();
    }
}