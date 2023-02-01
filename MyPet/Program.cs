using MyPet.Repository;
using MyPet.Repository.Context;
using MyPet.Repository.Interfaces;
using MyPet.Repository.TutorRepository;
using MyPet.Services.EnderecoServices;
using MyPet.Services.TutorServices;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
