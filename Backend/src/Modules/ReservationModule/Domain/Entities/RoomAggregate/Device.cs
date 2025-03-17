namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Device
{
    internal Guid UserId;

    public Guid Id { get; }
    public string Name { get; private set; }
    public string DeviceType { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Guid RoomId { get; private set; }

    public Device(string name, string deviceType, string description)
    {
        Id = Guid.NewGuid();
        Name = TrimAndValidateName(name);
        DeviceType = TrimAndValidateDeviceType(deviceType);
        Description = TrimAndValidateDescription(description);
        CreatedAt =  DateTime.Now;
        UpdatedAt =  DateTime.Now;
    }

    public bool UpdateDevice(string? name = null, string? deviceType = null, string? description = null)
    {
        var isUpdated = false;

        if (name != null)
        {
            name = TrimAndValidateName(name);
            if (name == Name)
            {
                isUpdated = false;
            }
            else
            {
                Name = name;
                isUpdated = true;
            }
        }

        if (deviceType != null)
        {
            deviceType = TrimAndValidateDeviceType(deviceType);
            if (deviceType == DeviceType)
            {
                isUpdated = false;
            }
            else
            {
                DeviceType = deviceType;
                isUpdated = true;
            }
        }

        if (description != null)
        {
            description = TrimAndValidateDescription(description);
            if (description == Description)
            {
                isUpdated = false;
            }
            else
            {
                Description = description;
                isUpdated = true;
            }
        }

        if (isUpdated)
        {
            UpdatedAt = DateTime.Now;
        }

        return isUpdated;
    }

    private static string TrimAndValidateName(string name)
    {
        name = name.Trim();
        if (name.Length is < 1 or > 100)
            throw new ArgumentException("Name must be between 1 and 100 characters long");

        return name;
    }

    private static string TrimAndValidateDeviceType(string deviceType)
    {
        deviceType = deviceType.Trim();
        if (deviceType.Length is < 1 or > 100)
            throw new ArgumentException("DeviceType must be between 1 and 100 characters long");

        return deviceType;
    }

    private static string TrimAndValidateDescription(string description)
    {
        description = description.Trim();
        if (description.Length >= 500)
            throw new ArgumentException("Description must be under 500 characters long");

        return description;
    }
}