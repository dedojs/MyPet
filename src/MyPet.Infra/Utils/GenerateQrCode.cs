using System.Drawing;
using MyPet.Application.Dtos.PetDtos;
using Newtonsoft.Json;
using QRCoder;


namespace MyPet.Utils
{
    public static class GenerateQrCodeForData
    {
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream.ToArray();
        }

        public static byte[] CreateQrCode(PetDtoWithTutor info)
        {
            var qrCodeGenerator = new QRCodeGenerator();

            var infoJson = JsonConvert.SerializeObject(info);

            var qrCodeData = qrCodeGenerator.CreateQrCode(infoJson, QRCodeGenerator.ECCLevel.Q);

            var qrCode = new QRCode(qrCodeData);

            var qrCodeImage = qrCode.GetGraphic(10);

            var bitmapBytes = BitmapToBytes(qrCodeImage);

            return bitmapBytes;
        }


    }
}
