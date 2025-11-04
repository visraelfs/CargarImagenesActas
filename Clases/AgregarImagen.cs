using CargarImagenesActas.Model.ElaraMiddleware;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace CargarImagenesActas.Clases
{
    internal class AgregarImagen
    {

        private bool _esSiteSurvey;
        private int _idActa;
        private string _nombreImagen;
        private Stream _archivo {get; set;}

        public AgregarImagen(bool esSiteSurvey, int idActa, string nombreImagen, string rutaImagen)
        {
            _esSiteSurvey = esSiteSurvey;
            _idActa = idActa;
            _nombreImagen = nombreImagen;
            _archivo = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read);
        }

        

        public bool guardarImagenActa()
        {
            try
            {

                string tipo = _idActa + (_esSiteSurvey ? "_documentoSiteSurvey" : "_documentoActa");

                byte[] fileBytes;
                fileBytes = ReduceImageSizeToByteArray(_archivo, 60L, 800, 600);

                using (Elara_MiddlewareEntities dbCOntext = new Elara_MiddlewareEntities())
                {
                    tmpFiles tmp = new tmpFiles();
                    tmp.FileName = _nombreImagen + ".jpg";
                    tmp.Data = fileBytes;
                    tmp.Tipo = tipo;

                    dbCOntext.tmpFiles.Add(tmp);
                    dbCOntext.SaveChanges();
                    
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static byte[] ReduceImageSizeToByteArray(Stream inputStream, long quality, int width, int height)
        {
            using (var image = System.Drawing.Image.FromStream(inputStream))
            {
                // Redimensionar la imagen
                var resizedImage = new System.Drawing.Bitmap(image, new System.Drawing.Size(width, height));

                using (var ms = new MemoryStream())
                {
                    var jpegEncoder = GetEncoder(ImageFormat.Jpeg);
                    var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                    // Guardar la imagen en el MemoryStream
                    resizedImage.Save(ms, jpegEncoder, encoderParams);

                    // Devolver el contenido del MemoryStream como byte[]
                    return ms.ToArray();
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
