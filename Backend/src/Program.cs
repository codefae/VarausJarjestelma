using FastEndpoints;
using FastEndpoints.Swagger;
using Moq;
using src.Modules.ReservationModule.Domain.DomainServices;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

// TODO her for now but should be moved to a separate file
#region ReservationRepositoryMock

var mockReservationRepository = new Mock<IReservationRepository>();
var cancellationToken = CancellationToken.None;
// Setup GetAsync method
mockReservationRepository.Setup(repo => repo.GetAsync(It.IsAny<Guid>(), cancellationToken))
    .ReturnsAsync((Guid id) => new Reservation(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1), TimeSpan.FromHours(10), TimeSpan.FromHours(12)));

// Setup GetAllAsync method
mockReservationRepository.Setup(repo => repo.GetAllAsync(cancellationToken))
    .ReturnsAsync(new List<Reservation>
    {
        new Reservation(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(1), TimeSpan.FromHours(10), TimeSpan.FromHours(12)),
        new Reservation(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddDays(2), TimeSpan.FromHours(14), TimeSpan.FromHours(16))
    });

// Setup AddAndMakeSureRoomIsNotChangedAsync method
mockReservationRepository.Setup(repo => repo.AddAndMakeSureRoomIsNotChangedAsync(It.IsAny<Reservation>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Setup UpdateAsync method
mockReservationRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Reservation>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Setup DeleteAsync method
mockReservationRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Reservation>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Setup GetByRoomAsync method
mockReservationRepository.Setup(repo => repo.GetByRoomAsync(It.IsAny<Guid>(), cancellationToken))
    .ReturnsAsync((Guid roomId) => new List<Reservation>
    {
        new Reservation(Guid.NewGuid(), roomId, DateTime.Now.AddDays(1), TimeSpan.FromHours(10), TimeSpan.FromHours(12))
    });

// Setup GetByUserAsync method
mockReservationRepository.Setup(repo => repo.GetByUserAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), cancellationToken))
    .ReturnsAsync((Guid userId, DateTime date) => new List<Reservation>
    {
        new Reservation(userId, Guid.NewGuid(), date, TimeSpan.FromHours(10), TimeSpan.FromHours(12))
    });

// Setup GetByRoomAndDateAsync method
mockReservationRepository.Setup(repo => repo.GetByRoomAndDateAsync(It.IsAny<Guid>(), It.IsAny<DateTime>(), cancellationToken))
    .ReturnsAsync((Guid roomId, DateTime date) => new List<Reservation>
    {
        new Reservation(Guid.NewGuid(), roomId, date, TimeSpan.FromHours(10), TimeSpan.FromHours(12))
    });

// Use the mock object
var reservationRepository = mockReservationRepository.Object;

#endregion
 
// TODO her for now but should be moved to a separate file
#region RoomRepositoryMock

var mockRoomRepository = new Mock<IRoomRepository>();

// Setup GetRoomByIdAsync method
mockRoomRepository.Setup(repo => repo.GetRoomByIdAsync(It.IsAny<Guid>(), cancellationToken))
    .ReturnsAsync((Guid id) => new Room( "Mock Room"));

// Setup GetRoomsAsync method
mockRoomRepository.Setup(repo => repo.GetRoomsAsync(cancellationToken))
    .ReturnsAsync(new List<Room>
    {
        new Room( "Mock Room 1", DateTime.Now, DateTime.Now.AddMonths(1), id: Guid.Parse("4a9a8b03-9205-41cc-836a-65a588780cf0")),
        new Room( "Mock Room 2", DateTime.Now, DateTime.Now.AddMonths(1))
    });

// Setup AddRoomAsync method
mockRoomRepository.Setup(repo => repo.AddRoomAsync(It.IsAny<Room>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Setup UpdateRoomAsync method
mockRoomRepository.Setup(repo => repo.UpdateRoomAsync(It.IsAny<Room>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Setup DeleteRoomAsync method
mockRoomRepository.Setup(repo => repo.DeleteRoomAsync(It.IsAny<Room>(), cancellationToken))
    .Returns(Task.CompletedTask);

// Use the mock object
var roomRepository = mockRoomRepository.Object;
#endregion

builder.Services.AddTransient<IRoomRepository>(sp => roomRepository);
builder.Services.AddTransient<IReservationRepository>(sp => reservationRepository);
builder.Services.AddSingleton<IBookingDomainService, BookingDomainService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDefaultExceptionHandler(useGenericReason: true)
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();