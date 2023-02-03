using QRCoder;

namespace MyPet.Services.PetServices
{
    public interface IPetService
    {
        QRCodeData GenerateQrCode(string info);
    }
}
