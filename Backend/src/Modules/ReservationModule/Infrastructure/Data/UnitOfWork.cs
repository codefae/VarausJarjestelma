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
    public async Task BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (transaction == null)
            throw new NullReferenceException("The transaction has already been completed or it was never started.");
        
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        await transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (transaction == null)
            throw new NullReferenceException("The transaction has already been completed or it was never started.");
        
        await transaction.RollbackAsync();
        await transaction.DisposeAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}