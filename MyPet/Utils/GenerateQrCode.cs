using System.Drawing;
using MyPet.Models.Dtos.PetDto;
using MyPet.Models.Entidades;
using Newtonsoft.Json;
using QRCoder;


namespace MyPet.Utils
{
    public static class GenerateQrCodeForData
    {
        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                return stream.ToArray();
            }
        }

        public static byte[] CreateQrCode(PetDto info)
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
