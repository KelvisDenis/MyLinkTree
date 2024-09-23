using LinkTree.Application.Middleware;
using LinkTree.Application.Services.Implementation;
using LinkTree.Application.Services.Interfaces;
using LinkTree.Infrastruture.Data;
using LinkTree.Infrastruture.Repository.Implementation;
using LinkTree.Infrastruture.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add Database InMemory
builder.Services.AddDbContext<AppDbContext>(op => op.UseInMemoryDatabase("LinkTree"));


// add repository scopped
builder.Services.AddScoped<ILinkRepository, LinkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// add service scopped
builder.Services.AddScoped<ILinksService, LinksServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthUser, AuthUser>();
builder.Services.AddScoped<ILinksService, LinksServices>();



// Add cors especify
builder.Services.AddCors(op => {op.AddPolicy(
    "AllowSpecifyOrigin", builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Ensure CORS is enabled before routing
app.UseCors("AllowSpecifyOrigin");
app.UseHttpsRedirection();
// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();
// Map controllers and endpoints
app.MapControllers();

app.Run();
