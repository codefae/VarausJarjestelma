namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IUnitOfWork : IDisposable 
{
    IReservationRepository Reservations{ get; }
    IRoomRepository Rooms { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}
