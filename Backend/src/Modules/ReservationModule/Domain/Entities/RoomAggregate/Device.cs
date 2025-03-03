namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Device 
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string DeviceType { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; } 
    public DateTime UpdatedAt { get; private set; } 

    public Device(string name, string deviceType, string description)
    {
        if (name.Length is < 1 or > 100)
            throw new ArgumentException("Name must be between 1 and 100 characters long");

        if (deviceType.Length is < 1 or > 100)
            throw new ArgumentException("DeviceType must be between 1 and 100 characters long");

        if (description.Length >= 500)
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
        if (deviceId != Id)
            return false; // Device ID does not match

        if (!string.IsNullOrWhiteSpace(name))
        {
            if (name.Length is < 1 or > 100)
                throw new ArgumentException("Name must be between 1 and 100 characters long");
            Name = name;
        }

        if (!string.IsNullOrWhiteSpace(deviceType))
        {
            if (deviceType.Length is < 1 or > 100)
                throw new ArgumentException("DeviceType must be between 1 and 100 characters long");
            DeviceType = deviceType;
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            if (description.Length >= 500)
                throw new ArgumentException("Description must be under 500 characters long");
            Description = description;
        }
        
        UpdatedAt = DateTime.Now;
        return true;
    }
}
