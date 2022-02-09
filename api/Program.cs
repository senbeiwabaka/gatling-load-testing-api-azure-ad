using load_testing_api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");

builder.Services.AddControllers();

builder.Services.AddDbContextPool<Context>(options =>
{
    options.EnableDetailedErrors(true);
    options.EnableSensitiveDataLogging(true);
    options.ConfigureWarnings(warnings => warnings.Throw());
    options.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking);

    options.UseInMemoryDatabase("test");
});

builder.Services.AddTransient<IPersonRepository, PersonRepository>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<Context>();

    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Persons.Add(new load_testing_api.Repository.Entities.Person { Id = 1, GivenName = "test", FamilyName = "test" });

    context.SaveChanges();
}

app.Run();
