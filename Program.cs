using Weatherapp.Components;
using MudBlazor;
using MudBlazor.Services;
using WSService.Services;
using WRModel.Models;
using Blazored.SessionStorage;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddBlazoredSessionStorage();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddScoped<MongoUserService>();

// builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<SearchHistoryService>();
builder.Services.AddScoped<SearchHistoryService>();
builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
   
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
