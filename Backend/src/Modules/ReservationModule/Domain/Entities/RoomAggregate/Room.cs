using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;

namespace src.Modules.ReservationModule.Domain.Entities.RoomAggregate;

public class Room : IAggregateRoot
{
    public Guid Id { get; }
    public string Name { get; }
    public OpenRules OpenRules { get; }
    public List<Device> Devices { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; } 

    public Room( string name, DateTime? defaultOpenDate = null, DateTime? defaultCloseDate = null, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        OpenRules = new OpenRules(
            defaultOpenDate ?? DateTime.Now,
            defaultCloseDate ?? DateTime.Now.AddMonths(6));
        Devices =  new List<Device>();
        
        var dateTimeNow = DateTime.Now;
        CreatedAt = dateTimeNow;
        UpdatedAt = dateTimeNow;
    }
    
    public bool UpdateRoom(string name)
    {
        throw new NotImplementedException();
    }

    public bool IsDeviceWithIdInDevicesList(Guid id)
    {
        return Devices.Any(device => device.Id == id);
    }
    
    public bool AddDevice(string name, string deviceType, string description)
    {
        if (Devices.Any(device => device.Name == name))
        {
            return false; // Device with the same name already exists
        }
        
        var newDevice = new Device(name, deviceType, description);
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
}
