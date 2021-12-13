using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BarcodeLib;
using System.Drawing;

namespace BarCodeGenerator
{
    public static class GenerateBarCode
    {
        [FunctionName("GenerateBarCode")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int bcHeight;
            int bcWidth;
            string bcValue = req.Query["value"];

            //all querystring values must be treated as string
            string bcHeightString = req.Query["height"];
            string bcWidthString = req.Query["width"];

            if (!int.TryParse(bcHeightString, out bcHeight))
                bcHeight = 28;
            if (!int.TryParse(bcWidthString, out bcWidth))
                bcWidth = 288;


            Barcode barcode = new Barcode();

            Image image = barcode.Encode(TYPE.CODE39, bcValue ,Color.Black,Color.Transparent, bcWidth, bcHeight); 

            return new FileContentResult(ImageToByteArray(image), "image/jpeg");
        }

        private static byte[] ImageToByteArray(Image image)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(image, typeof(byte[]));

        }
    }
}
