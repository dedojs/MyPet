namespace MyPet.Services.TutorServices
{
    public interface ITutorService
    {
        Task<object> ValidateCep(string cep);
    }
}
