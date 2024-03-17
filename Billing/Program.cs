using Billing.Data;
using Billing.Entity;
using Billing.Helpers;
using Billing.Services;
using Billing.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<Usage>, UsageValidator>();
builder.Services.AddScoped<IValidator<OrderItem>, OrderItemValidator>();
builder.Services.Configure<TierSettings>(builder.Configuration.GetSection(TierSettings.Key));
builder.Services.Configure<TierQuantity>(builder.Configuration.GetSection(TierQuantity.Key));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<EnumSchemaFilter>();
});

builder.Services.AddScoped<IBillingService, BillingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
