var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin", builder =>
	{
		builder.WithOrigins("https://localhost:7130")
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .AllowCredentials(); // Allows the use of cookies in CORS requests
	});
});

builder.Services.AddHttpClient();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
