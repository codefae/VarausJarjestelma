namespace src.Modules.ReservationModule.Shared.Interfaces;

public interface IUnitOfWork<T> : IDisposable 
{
    IReservationRepository ReservationRepository { get; }
    IRoomRepository SomeEntities { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
