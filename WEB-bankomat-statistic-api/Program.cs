using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using DotNet.RateLimiter.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankomatContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:ValidAudience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Jwt:Secret").Value))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
        factory: partition => new FixedWindowRateLimiterOptions
        {
            AutoReplenishment = true,
            PermitLimit = 10,
            QueueLimit = 0,
            Window = TimeSpan.FromMinutes(1)
        }));
});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standaard Authorization header using the Bearer scheme (\"bearer {tooken}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();

    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Bank API",
            Description = "Platform an ASP.NET CORE 6. API Developed by Mike Vazowski. This API is still in development, minimal functionality has been implemented",
            Version = "v1",
            //TermsOfService = new Uri("http://t.me/black6berry"),
            Contact = new OpenApiContact
            {
                Name = "Mike Vazowski",
                Email = "mikevazoskii@yandex.com"
            },
            //License = new OpenApiLicense
            //{
            //    Name = "Employee API Bank",
            //    Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0"),
            //}

        });


    //c.CustomOperationIds(apiDesc =>
    //{
    //    return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
    //});
    //// reading xml comments + turn in assembling project xml file docs
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});

//builder.Services.AddCors();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(builder => builder.WithOrigins("https://localhost:7003/"));
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
