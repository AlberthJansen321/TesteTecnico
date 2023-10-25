var builder = WebApplication.CreateBuilder(args);


#region Banco de dados
builder.Services.AddDbContext<ApiContext>(
    context => context.UseSqlServer( builder.Configuration.GetConnectionString("Default")));
#endregion


// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#region Application
builder.Services.AddScoped<IProdutoApplication, ProdutoApplication>();
#endregion
#region Repository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IGeneralRepository, GeneralRepository>();
#endregion



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions 
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
    Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto 
});

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseStatusCodePages();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
