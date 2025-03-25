using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using src.Modules.ReservationModule.Infrastructure.Data.Repositories;
using src.Modules.ReservationModule.Shared.Interfaces;

namespace src.Modules.ReservationModule.Infrastructure.Data;

public class UnitOfWork(ApplicationDbContext context, IReservationRepository reservationRepository, IRoomRepository roomRepository) : IUnitOfWork
{
    private IDbContextTransaction? transaction;
    public IReservationRepository Reservations { get; } = reservationRepository;
    public IRoomRepository Rooms { get; } = roomRepository;
    public async Task BeginTransactionAsync(CancellationToken cancellationToken) =>
        transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable,cancellationToken);
    
    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction == null)
            throw new NullReferenceException("The transaction has already been completed or it was never started.");
        
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }

        await transaction.CommitAsync(cancellationToken);
        await transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction == null)
            throw new NullReferenceException("The transaction has already been completed or it was never started.");
        
        await transaction.RollbackAsync(cancellationToken);
        await transaction.DisposeAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}