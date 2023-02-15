using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [Authorize(Roles ="reader")]
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region==null)
            {
                return NotFound();
            }
            var regionDTO=mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //validate the request
           //if(!ValidateAddRegionAsync(addRegionRequest))
           // {
           //     return BadRequest(ModelState);
           // }

            //Request (DTO) to doman model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            // pass details to repository
            region= await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var region = await regionRepository.DeleteAsync(id);

            //if null NotFound
            if(region == null)
            {
                return NotFound();
            }
            //convert response to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //return OK response
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id ,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //validate the incoming Request
            if(!ValidateUpdateRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }


            //convert DTO to domain Model
            var region = new Models.Domain.Region()
            {
              
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            //update region using repository
            region = await regionRepository.UpdateAsync(id,region);

            //if null than not found
            if(region==null)
            {
                return NotFound();
            }
            //convert domain back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return ok response
            return Ok(regionDTO);

        }


        #region Private methods
        private bool ValidateAddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest==null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                   $"Add Region Data is required.");
                return false;
            }


            if(string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} Cannot be null or empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} Cannot be null or empty or white space.");
            }
            if(addRegionRequest.Area<=0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                   $"{nameof(addRegionRequest.Area)} Cannot be less than or equal to Zero.");
            }
            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat),
                   $"{nameof(addRegionRequest.Lat)} Cannot be less than or equal to Zero.");
            }
            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long),
                   $"{nameof(addRegionRequest.Long)} Cannot be less than or equal to Zero.");
            }
            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                   $"{nameof(addRegionRequest.Population)} Cannot be less than  Zero.");
            }
            if(ModelState.ErrorCount>0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateRegionAsync(Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                   $"Add Region Data is required.");
                return false;
            }


            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} Cannot be null or empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} Cannot be null or empty or white space.");
            }
            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                   $"{nameof(updateRegionRequest.Area)} Cannot be less than or equal to Zero.");
            }
            //if (updateRegionRequest.Lat <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateRegionRequest.Lat),
            //       $"{nameof(updateRegionRequest.Lat)} Cannot be less than or equal to Zero.");
            //}
            //if (updateRegionRequest.Long <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateRegionRequest.Long),
            //       $"{nameof(updateRegionRequest.Long)} Cannot be less than or equal to Zero.");
            //}
            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                   $"{nameof(updateRegionRequest.Population)} Cannot be less than  Zero.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }



        #endregion

    }
}
