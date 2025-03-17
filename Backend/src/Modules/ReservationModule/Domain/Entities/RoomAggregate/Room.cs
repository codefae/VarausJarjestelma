using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Room : IAggregateRoot
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public OpenRules OpenRules { get; }
    public List<Device> Devices { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Room(string name, DateTime? defaultOpenDate = null, DateTime? defaultCloseDate = null, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = TrimAndValidateName(name);
        OpenRules = new OpenRules(
            defaultOpenDate ?? DateTime.Now,
            defaultCloseDate ?? DateTime.Now.AddMonths(6));
        Devices = [];
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public bool UpdateRoom(string name)
    {
        name = TrimAndValidateName(name);
        if (name == Name)
        {
            return false;
        }

        Name = name;
        return true;
    }

    public bool IsDeviceWithIdInDevicesList(Guid id) =>
        Devices.Any(device => device.Id == id);
    
    public bool AddDevice(string name, string deviceType, string description)
    {
        var newDevice = new Device(name, deviceType, description);
        if (Devices.Any(device => device.Name == newDevice.Name))
        {
            return false; // Device with the same name already exists
        }

        Devices.Add(newDevice);
        UpdatedAt = DateTime.Now;
        return true;
    }

    public bool RemoveDevice(Guid deviceId)
    {
        var device = Devices.FirstOrDefault(d => d.Id == deviceId);
        if (device == null)
        {
            return false; // Device not found
        }

        Devices.Remove(device);
        UpdatedAt = DateTime.Now;
        return true;
    }

    private static string TrimAndValidateName(string name)
    {
        name = name.Trim();
        if (name.Length is < 1 or > 100)
            throw new ArgumentException("Name must be between 1 and 100 characters long");

        return name;
    }
}