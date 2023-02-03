using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyPet.Infra.Data.Context;
using MyPet.Infra.Data.Repository.EnderecoRepository;
using MyPet.Infra.Data.Repository.PetRepository;
using MyPet.Infra.Data.Repository.TutorRepository;
using MyPet.Services;
using MyPet.Services.EnderecoServices;
using MyPet.Services.TutorServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyPetContext>();
builder.Services.AddScoped<IMyPetContext, MyPetContext>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

// Add Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient<ITutorService, TutorService>();
builder.Services.AddHttpClient<IEnderecoService, EnderecoService>();

// Add services Token
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret))
    };
});

// Adicionar POLICY CLAIMS
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Login", policy =>
//    {
//        policy.RequireClaim("tutorClaims", new string[] );
//    });

//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adicionar uso de autenticação
app.UseAuthentication();

// Adicionar uso de autorização
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
