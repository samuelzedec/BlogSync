using backend.Data;
using backend.Repositories;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

/* ===============================================================================
 * O ASP.NET Core vai buscar a connectionString do appsettings.json
 * e usá-la para configurar a conexão do Entity Framework Core
 * =============================================================================== */
builder.Services.AddDbContext<BlogSyncDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging(false) // Evitar exibir dados sensíveis (SQL)
        .LogTo(Console.WriteLine, LogLevel.Error)); // Apenas erros, sem SQL

/* ===============================================================================
 * Aqui estamos fazendo com que o ASP.NET cuide para nós das dependências que
 * nossas classes precisam usando os serviços que foram registrados para ele
 * =============================================================================== */
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<PostRepository>();

var app = builder.Build();
app.MapControllers();
app.Run("https://localhost:8000/");