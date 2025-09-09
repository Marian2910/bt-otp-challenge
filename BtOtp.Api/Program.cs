using BtOtp.Api.Middleware;
using BtOtp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();           
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SecureOtpGenerator>();
builder.Services.AddSingleton<IOtpService, OtpService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OTP API v1");
        c.RoutePrefix = "swagger";
    });
}

app.MapControllers();

app.Run();