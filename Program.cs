using System.Text;
using System.Text.Json.Serialization;
using BackendApi2.Contracts;
using BackendApi2.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.Setup().LoadConfigurationFromFile(Directory.GetCurrentDirectory()+"/nlog.config");
builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositry();
builder.Services.AddAutoMapper(typeof(Program));
 builder.Services.AddControllers(config=>
 {
    config.ReturnHttpNotAcceptable=true;
    config.RespectBrowserAcceptHeader = true;
 }).AddXmlSerializerFormatters().AddJsonOptions(options=>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.ConfigureSqlServer(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(optn=>
 {
     optn.TokenValidationParameters =  new TokenValidationParameters()
     {
       ValidateIssuerSigningKey = true,
       ValidateIssuer = true,
       ValidateAudience = false,
       ValidateLifetime = true,
       IssuerSigningKey =  new SymmetricSecurityKey( Encoding.UTF8.GetBytes
       (builder.Configuration.GetSection("JwtSettings:SignKey").Value)),
       ValidIssuer =  builder.Configuration.GetSection("JwtSettings:Issuer").Value

     };
 });
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
   // app.UseSwagger();
   // app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);


app.Use( async ( context, next ) => {
    await next();

    if( context.Response.StatusCode == 404 && !Path.HasExtension( context.Request.Path.Value ) ) {
        context.Request.Path = "/index.html";
        await next();
    }
});

app.UseStaticFiles();
app.UseFileServer();

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{ 
    ForwardedHeaders = ForwardedHeaders.All 
});

app.UseCors("corsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
