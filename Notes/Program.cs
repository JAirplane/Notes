using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Notes_Model.PostgresDB;
using Notes_Model.Repository;
using Notes_ViewModel;
using NLog;
using NLog.Web;
using Majorsoft.Blazor.Components.CssEvents;
using Majorsoft.Blazor.Components.Notifications;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	builder.Services.AddRazorPages();
	builder.Services.AddServerSideBlazor();
	builder.Services.AddScoped<UserState>();
	builder.Services.AddScoped<AuthenticatedUserHandler_VM>();
	builder.Services.AddScoped<IRepository, NotesRepository>();
	builder.Services.AddScoped<INotificationCancellation, NotificationManager>();
	builder.Services.AddCssEvents();
	builder.Services.AddNotifications();
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	{
		app.UseExceptionHandler("/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();

	app.UseStaticFiles();

	app.UseRouting();

	app.MapBlazorHub();
	app.MapFallbackToPage("/_Host");

	app.Run();
}
catch (Exception exception)
{
	// NLog: catch setup errors
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
	LogManager.Shutdown();
}
