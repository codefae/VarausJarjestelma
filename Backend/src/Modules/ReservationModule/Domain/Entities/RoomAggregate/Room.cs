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
        Devices =  [];
        
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
        throw new NotImplementedException();
    }
    
    public bool AddDevice(string name, string deviceType, string description)
    {
        throw new NotImplementedException();
    }

    public bool RemoveDevice(Guid deviceId)
    {
        throw new NotImplementedException();
    }
}