using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using NZWalks.API.Repository.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _context;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this._context = regionRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from context
            var regionsDomain = await _context.GetAllAsync();

            // Pass it to DTO
            /*var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(
                    new RegionDto()
                    {
                        Id = region.Id,
                        Name = region.Name,
                        Code = region.Code,
                        RegionImageUrl = region.RegionImageUrl,
                    });
            }*/

            // AutoMapper
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            // return DTOs
            return Ok(regionsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _context.GetByIdAsync(id);

            if (regionDomain == null) { return  NotFound(); }

            /*var regionDto = new RegionDto() 
            { 
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };*/

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Convert to Model domain
            var regionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // add and save to database
            regionDomain = await _context.CreateAsync(regionDomain);

            // Map domain back to Dto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Model Domain
            var regionDomain = _mapper.Map<Region>(addRegionRequestDto);

            // update region with dependency inyection interface (repository pattern)
            regionDomain = await _context.UpdateAsync(id, regionDomain);

            if (regionDomain == null) { return NotFound(); }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // check if region exist
            var regionDomain = await _context.RemoveAsync(id);

            if (regionDomain == null) { return NotFound(); }

            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
    }
}
