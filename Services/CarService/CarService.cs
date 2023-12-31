﻿using AutoMarketplace.Data;
using AutoMarketplace.Data.Entities;
using Dropbox.Api.Files;
using Dropbox.Api;
using AutoMarketplace.Extensions;
using Dropbox.Api.Sharing;
using Dropbox.Api.TeamLog;
using AutoMarketplace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Policy;
using static Dropbox.Api.Files.ListRevisionsMode;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Runtime.ConstrainedExecution;
using AutoMarketplace.Modules;

namespace AutoMarketplace.Services.CarService
{
    public class CarService : ICarService
    {
        private ApplicationDbContext dbContext;
        private readonly ILogger<CarService> _logger;
        public CarService(ApplicationDbContext dbContext, ILogger<CarService> logger)
        {
            this.dbContext = dbContext;
            this._logger = logger;
        }

        public bool AddModel(CarModelDto model, string userId)
        {
            var imageUrl = "/images/images.jpg";

            if (model.Image != null)
            {
                try
                {
                    var buffer = model.Image.GetBytes().GetAwaiter().GetResult(); ;
                    imageUrl = this.UploadFile("/test", model.Image.FileName, buffer).GetAwaiter().GetResult();
                }
                catch (Exception)
                {
                    this._logger.LogError("Error occured while uploading model image");
                }
               
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
            var url = "/images/images.jpg";

            try
            {
                var buffer = file.GetBytes().GetAwaiter().GetResult(); ;
                url = this.UploadFile("/test", file.FileName, buffer).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                this._logger.LogError("Error occured while uploading make image");
            }
             

            var newMake = new CarMake
            {
                Name = name,
                LogoUrl = url,
            };

            this.dbContext.CarMakes.Add(newMake);

            this.dbContext.SaveChanges(userId);
            return true;
        }

        public bool DeleteMake(int id)
        {
            var carMake = this.dbContext.CarMakes.Include(x => x.Models).FirstOrDefault(x => x.Id == id);

            if (carMake == null)
            {
                throw new InvalidOperationException("Invalid Car Make Id!");
            }

            carMake.Models.Clear();

            this.dbContext.CarMakes.Remove(carMake);
            this.dbContext.SaveChanges();

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

        public CarMakeModel GetCarMakeById(int id, int pageNumber = 1)
        {
            var make =  this.dbContext.CarMakes
                .Include(x => x.Models)
                .FirstOrDefault(x => x.Id == id);

            var mappedModels = make.Models.Select(x => new CarModelDto
            {
                Name = x.Name,
                Id = x.Id,
                ImageUrl = x.ImageUrl,
            }).ToList();

            var models = new PaginatedList<CarModelDto>().Create(mappedModels, pageNumber, 3);

            return new CarMakeModel
            {
                Id = id,
                Name = make.Name,
                LogoUrl = make.LogoUrl,
                Models = models,
            };
        }

        public List<CarMakeModel> GetCarMakeList()
        {
            return this.dbContext.CarMakes
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
            var refreshTOken = "Access_token";
            
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

        public bool EditMake(CarMakeModel model, string userId)
        {
            var carMake = this.dbContext.CarMakes.Include(x => x.Models).FirstOrDefault(x => x.Id == model.Id);

            if (carMake == null)
            {
                throw new InvalidOperationException("Invalid Car Make Id!");
            }

            carMake.Name = model.Name;
            try
            {
                if (model.File != null)
                {
                    var buffer = model.File.GetBytes().GetAwaiter().GetResult(); ;
                    var url = this.UploadFile("/test", model.File.FileName, buffer).GetAwaiter().GetResult();
                    carMake.LogoUrl = url;
                }
              
            }
            catch (Exception)
            {
                this._logger.LogError("Error occured while uploading make image");
            }

            this.dbContext.SaveChanges(userId);

            return true;
        }

        public bool EditModel(CarModelDto model, string userId)
        {
            var carModel = this.dbContext.CarModels.FirstOrDefault(x => x.Id == model.Id);

            if (carModel == null)
            {
                throw new InvalidOperationException("Invalid Car Model Id!");
            }

            carModel.Name = model.Name;
            carModel.NumberOfDoors = model.NumberOfDoors;
            carModel.BodyType = model.BodyType;
            carModel.FuelType = (Data.Enum.FuelType)model.FuelType;
            carModel.InTownFuelConsumptionPer100km = model.InTownFuelConsumptionPer100km;
            carModel.OutOfTownFuelConsumptionPer100km = model.OutOfTownFuelConsumptionPer100km;
            carModel.CombinedFuelConsumptionPer100km = model.CombinedFuelConsumptionPer100km;
            carModel.TankVolume = model.TankVolume;
            carModel.StartYearOfProduction = model.StartYearOfProduction;
            carModel.Engine = model.Engine;

            try
            {
                if (model.Image != null)
                {
                    var buffer = model.Image.GetBytes().GetAwaiter().GetResult(); ;
                    var url = this.UploadFile("/test", model.Image.FileName, buffer).GetAwaiter().GetResult();
                    carModel.ImageUrl = url;
                }

            }
            catch (Exception)
            {
                this._logger.LogError("Error occured while uploading model image");
            }

            this.dbContext.SaveChanges(userId);

            return true;
        }

        public List<CarMakeModel> GetSerachedCarMakes(string searchWord)
        {
            return this.dbContext.CarMakes
              .Where(c => c.Name.StartsWith(searchWord) || c.Name.Contains(searchWord))
              .Select(c => new CarMakeModel
              {
                  Id = c.Id,
                  Name = c.Name,
                  LogoUrl = c.LogoUrl,
              }).OrderBy(x => x.Name).ToList();
        }
    }
}
