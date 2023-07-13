using AutoMarketplace.Data;
using AutoMarketplace.Data.Entities;
using Dropbox.Api.Files;
using Dropbox.Api;
using AutoMarketplace.Extensions;
using Dropbox.Api.Sharing;
using Dropbox.Api.TeamLog;
using AutoMarketplace.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoMarketplace.Services.CarService
{
    public class CarService : ICarService
    {
        private ApplicationDbContext dbContext;

        public CarService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddModel(CarModelDto model, string userId)
        {
            var imageUrl = "/images/images.jpg";

            if (model.Image != null)
            {
                var buffer = model.Image.GetBytes().GetAwaiter().GetResult(); ;
                imageUrl = this.UploadFile("/test", model.Image.FileName, buffer).GetAwaiter().GetResult();
            }

            var newModel = new CarModel
            {
                Name = model.Name,
                NumberOfDoors = model.NumberOfDoors,
                BodyType = model.BodyType,
                CombinedFuelConsumptionPer100km = model.CombinedFuelConsumptionPer100km,
                OutOfTownFuelConsumptionPer100km = model.OutOfTownFuelConsumptionPer100km,
                InTownFuelConsumptionPer100km = model.InTownFuelConsumptionPer100km,
                FuelType = (Data.Enum.FuelType)model.FuelType,
                Engine = model.Engine,
                MakeId = model.MakeId,
                StartYearOfProduction = model.StartYearOfProduction,
                ImageUrl = imageUrl,
                TankVolume = model.TankVolume,
            };

            this.dbContext.CarModels.Add(newModel);
            this.dbContext.SaveChanges(userId);

            return true;
        }

        public bool CreateMake(string name, IFormFile file, string userId)
        {
            var buffer = file.GetBytes().GetAwaiter().GetResult(); ;

            var url = this.UploadFile("/test", file.FileName, buffer).GetAwaiter().GetResult();

            var newMake = new CarMake
            {
                Name = name,
                LogoUrl = url,
            };

            this.dbContext.CarMakes.Add(newMake);

            this.dbContext.SaveChanges(userId);
            return true;
        }

        public bool DeleteModel(int id)
        {
            var carModel = this.dbContext.CarModels.FirstOrDefault(x => x.Id == id);

            if (carModel == null)
            {
                throw new InvalidOperationException("Invalid Car Model Id!");
            }

            this.dbContext.CarModels.Remove(carModel);
            this.dbContext.SaveChanges();

            return true;
        }

        public CarMakeModel GetCarMakeById(int id)
        {
            var make =  this.dbContext.CarMakes
                .Include(x => x.Models)
                .FirstOrDefault(x => x.Id == id);

            return new CarMakeModel
            {
                Id = id,
                Name = make.Name,
                LogoUrl = make.LogoUrl,
                Models = make.Models.Select(x => new CarModelDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                }).ToList(),
            };
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
                }).OrderBy(x => x.Name).ToList();
        }

        public CarModelDto GetCarModelById(int id)
        {
            var carModel = this.dbContext.CarModels
                .Include(x => x.Make)
                .FirstOrDefault(x => x.Id == id);

            return new CarModelDto
            {
                Name = carModel.Make.Name + " " + carModel.Name,
                ImageUrl = carModel.ImageUrl,
                BodyType = carModel.BodyType,
                StartYearOfProduction = carModel.StartYearOfProduction,
                CombinedFuelConsumptionPer100km = carModel.CombinedFuelConsumptionPer100km,
                InTownFuelConsumptionPer100km = carModel.InTownFuelConsumptionPer100km,
                OutOfTownFuelConsumptionPer100km = carModel.OutOfTownFuelConsumptionPer100km,
                NumberOfDoors = carModel.NumberOfDoors,
                Engine = carModel.Engine,
                FuelString = carModel.FuelType.ToString(),
                TankVolume= carModel.TankVolume,
                Id = id,
                MakeId = carModel.MakeId,
                MaxKmForFullTankCombined = GetMaxKmForFullTank(carModel.TankVolume, carModel.CombinedFuelConsumptionPer100km),
                MaxKmForFullTankInTown = GetMaxKmForFullTank(carModel.TankVolume, carModel.InTownFuelConsumptionPer100km),
                MaxKmForFullTankOutOfTown = GetMaxKmForFullTank(carModel.TankVolume, carModel.OutOfTownFuelConsumptionPer100km)
            };
        }

        private double GetMaxKmForFullTank(double fullTankCapacity, double fuelConsumption)
        {
            return (100 / fuelConsumption) * fullTankCapacity;
        }

        private async Task<string> UploadFile(string folder, string fileName, byte[] buffer)
        {
            //Access token
            var refreshTOken = "ACCESS_TOKEN";
            
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
