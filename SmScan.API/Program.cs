using SmScan.API;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);

//builder.UseSerilog((context, loggerConfig) =>
//              loggerConfig.ReadFrom.Configuration(context.Configuration));
startup.ConfigureServices(builder.Services, builder.Host);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
