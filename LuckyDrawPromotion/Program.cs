using Microsoft.EntityFrameworkCore;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LuckyDrawPromotionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LuckyDrawPromotionContext") ?? throw new InvalidOperationException("Connection string 'LuckyDrawPromotionContext' not found.")));

// Add services to the container.
builder.Services.AddControllers();
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// configure DI for application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<ISizeProgramService, SizeProgramService>();
builder.Services.AddScoped<ICharsetService, CharsetService>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IRepeatScheduleService, RepeatScheduleService>();
builder.Services.AddScoped<IBarCodeService, BarCodeService>();
builder.Services.AddScoped<ICodeGiftCampaignService, CodeGiftCampaignService>();
builder.Services.AddScoped<IRuleService, RuleService>();
builder.Services.AddScoped<IWinnerService, WinnerService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// global cors policy
app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
