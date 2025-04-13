using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Moq;
using src;
using src.Mock;
using src.Modules.ReservationModule.Domain.DomainServices;
using src.Modules.ReservationModule.Domain.DomainServices.Interfaces;
using src.Modules.ReservationModule.Domain.Entities.ReservationAggregate;
using src.Modules.ReservationModule.Domain.Entities.RoomAggregate;
using src.Modules.ReservationModule.Infrastructure.Data;
using src.Modules.ReservationModule.Infrastructure.Data.Repositories;
using src.Modules.ReservationModule.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Allow CORS for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins(["http://192.168.159.23:5173","http://localhost:5173"]) // your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});





builder.Services.AddOpenApi();
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISendEMailToUser, SendEmailToUserMock>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookingDomainService, BookingDomainService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseDefaultExceptionHandler(useGenericReason: true)
    .UseFastEndpoints()
    .UseSwaggerGen();

app.UseCors("AllowFrontend");

app.Run();