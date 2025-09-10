using BtOtp.Api.Middleware;
using BtOtp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();           
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SecureOtpGenerator>();
builder.Services.AddSingleton<IOtpService, OtpService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:5173") // Vite dev server URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors("AllowFrontend");

// Swagger only in Development
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