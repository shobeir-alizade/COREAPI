//using WebAPI.Core.Domain;
//using WebAPI.Data;
//using WebAPI.Service.DTOs;
//using WebAPI.Service.Extentions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using WebAPI.Data.Repository;

//namespace WebAPI.Service.Media
//{
//    public class PictureService : IPictureService
//    {
//        #region Fields
//        private readonly IRepository<Picture> _repositoryPicture; // Removed null assignment to fix CS8625
//        #endregion

//        public PictureService(IRepository<Picture> repositoryPicture)
//        {
//            _repositoryPicture = repositoryPicture ?? throw new ArgumentNullException(nameof(repositoryPicture)); // Added null check for safety
//        }

//        public async Task<PictureDTO> RegisterPictureAsync(PictureUploadDTO pictureUploadDTO)
//        {
//            var picture = new Picture();
//            picture.MimeType = pictureUploadDTO.ContentType;
//            await _repositoryPicture.InsertAsync(picture);

//            var fileName = $"{picture.ID:0000000}_0{pictureUploadDTO.fileExtension}";

//            byte[] pictureBinary = null;
//            using (var fileStream = pictureUploadDTO.File.OpenReadStream())
//            {
//                using (var ms = new MemoryStream())
//                {
//                    fileStream.CopyTo(ms);
//                    pictureBinary = ms.ToArray();
//                }
//            }

//            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PImages", fileName);

//            await File.WriteAllBytesAsync(filePath, pictureBinary);

//            picture.VirtualPath = filePath;

//            await _repositoryPicture.UpdateAsync(picture);

//            PictureDTO pictureDTO = picture.TODTO<PictureDTO>();

//            return pictureDTO;
//        }

//        public async Task<PictureDTO> RegisterBase64PictureAsync(PictureUploadBase64DTO pictureUploadDTO)
//        {
//            var picture = new Picture();
//            picture.MimeType = pictureUploadDTO.ContentType;
//            await _repositoryPicture.InsertAsync(picture);

//            var fileName = $"{picture.ID:0000000}_0{pictureUploadDTO.fileExtension}";

//            byte[] pictureBinary = Convert.FromBase64String(pictureUploadDTO.File);

//            var filePath = Path.Combine(Directory.GetCurrentDirectory(),
//                                     "wwwroot", "PImages", fileName);

//            await File.WriteAllBytesAsync(filePath, pictureBinary);

//            picture.VirtualPath = filePath;

//            await _repositoryPicture.UpdateAsync(picture);

//            PictureDTO pictureDTO = picture.TODTO<PictureDTO>();

//            return pictureDTO;
//        }

//        public async Task<PictureDTO> SearchPictureByIdAsync(int id)
//        {
//            var picture = await _repositoryPicture.GetByIdAsync(id);

//            PictureDTO pictureDTO = picture.TODTO<PictureDTO>();

//            return pictureDTO;
//        }

//        public async Task<bool> CheckExists(int ID)
//        {
//            return (await _repositoryPicture.GetByIdAsNoTrackingAsync(ID) != null);
//        }
//    }
//}
