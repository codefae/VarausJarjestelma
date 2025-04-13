using System.ComponentModel.DataAnnotations;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Room 
{
    public Guid Id { get; private set; }
    [MaxLength(100)]
    public string Name { get; private set; }
    public OpenRules OpenRules { get; private set; }
    public ICollection<Device> Devices { get; private set; } = new List<Device>();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Parameterless constructor for EF Core
#pragma warning disable CS8618, CS9264
    public Room()
    {
    }
#pragma warning restore CS8618, CS9264

    public Room(string name, DateTime defaultOpenDate, DateTime defaultCloseDate, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = TrimAndValidateName(name);
        OpenRules = new OpenRules(
            defaultOpenDate,
            defaultCloseDate);
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

    public bool AddDevice(Device newDevice)
    {
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