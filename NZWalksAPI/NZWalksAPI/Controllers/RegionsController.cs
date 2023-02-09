﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task< IActionResult> GetAllRegions()
        {
            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Wellingtone",
            //        Code="WLG",
            //        Area=224466,
            //        Lat=1.5874,
            //        Long=299.88,
            //        Population=50000
            //    },
            //     new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="Auckland",
            //        Code="AUCK",
            //        Area=224466,
            //        Lat=1.5874,
            //        Long=299.88,
            //        Population=50000
            //    },
            //};

            var regions = await regionRepository.GetAllAsync();

            //Return DTO Regions
            //var regionsDTO=new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };

            //    regionsDTO.Add(regionDTO);

            //});
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDTO);

        }
    }
}
