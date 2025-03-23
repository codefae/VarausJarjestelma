namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IUnitOfWork : IDisposable 
{
    IReservationRepository Reservations{ get; }
    IRoomRepository Rooms { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
