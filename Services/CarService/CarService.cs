﻿using AutoMarketplace.Data;
using AutoMarketplace.Data.Entities;
using Dropbox.Api.Files;
using Dropbox.Api;
using AutoMarketplace.Extensions;
using Dropbox.Api.Sharing;
using Dropbox.Api.TeamLog;
using AutoMarketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMarketplace.Services.CarService
{
    public class CarService : ICarService
    {
        private ApplicationDbContext dbContext;

        public CarService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CreateMake(string name, IFormFile file)
        {
            var buffer = file.GetBytes().GetAwaiter().GetResult(); ;

            var url = this.UploadFile("/test", file.FileName, buffer).GetAwaiter().GetResult();

            var newMake = new CarMake
            {
                Name = name,
                LogoUrl = url,
            };

            this.dbContext.CarMakes.Add(newMake);

            this.dbContext.SaveChanges();
            return true;
        }

        public List<CarMakeModel> GetCarMakeList()
        {
            return this.dbContext.CarMakes
                .Include(x => x.Models)
                .Select(c => new CarMakeModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    LogoUrl = c.LogoUrl,
                    ModelsCount = c.Models.Count(),
                }).OrderBy(x => x.Name).ToList();
        }

        private async Task<string> UploadFile(string folder, string fileName, byte[] buffer)
        {
            //Access token
            var refreshTOken = "sl.BiDhhQqvF9e2Mlz4eZLa2HeS_15-FCxsvHkpdIhzsmbYfXsP3AVlc7Z02PyOvb80wrQtqPVHKlmy2IsvRFhJN5hvRO4xgXNw-LOSDKgy7N5LehNMOtPVbDtMdWyzWrHQFfmJN1o-yI3F";
            
            var dropBoxClient = new DropboxClient(refreshTOken);
            FileMetadata uploadResult = await dropBoxClient.Files.UploadAsync(
              folder + "/" + fileName,
              WriteMode.Overwrite.Instance,
              body: new MemoryStream(buffer));

            var filePath = uploadResult.PathDisplay;

            var visability = new RequestedVisibility();
            var sharedLinkSettings = new SharedLinkSettings(requestedVisibility: visability.AsPublic);
            var sharedLink = await dropBoxClient.Sharing.CreateSharedLinkWithSettingsAsync(filePath, sharedLinkSettings);

            var imageUrl = sharedLink.Url.Replace("dl=0", "raw=1");

            return imageUrl;
        }
    }
}
