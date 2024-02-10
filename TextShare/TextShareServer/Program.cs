using TextShareServer;
using TextShareServer.Services;
using CommandLine;

var clOptions = Parser.Default.ParseArguments<CommandLineOptions>(args).Value;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

if (clOptions.UseSwagger)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

builder.Services.AddScoped<FileService>();
builder.Services.AddSingleton<CommandLineOptions>(clOptions);

builder.WebHost.UseUrls(clOptions.GetHostUrls());

var app = builder.Build();

if (clOptions.UseSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    ConsoleInfo.WriteInfo("Swagger UI enabled.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

if (clOptions.UseHttps)
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
