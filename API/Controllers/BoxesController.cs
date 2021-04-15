using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductDb.DataClasses;
using Services;
using Services.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoxesController : ControllerBase
    {
        private readonly ILogger<BoxesController> _logger;
        private readonly BoxService _boxService;

        public BoxesController(ILogger<BoxesController> logger, BoxService boxservice)
        {
            _logger = logger;
            _boxService = boxservice;
        }

        [HttpGet("Boxes")]
        public async Task<ActionResult<IReadOnlyList<BoxToReturnDto>>> GetBoxes([FromQuery] BoxProductParams productParams)
        {
            var boxes = await _boxService.GetBoxesWithParams(productParams);

            if (boxes is null)
                return BadRequest();

            return Ok(boxes);
        }

        [HttpPost("Boxes/Create")]
        public async Task<ActionResult> CreateBox(BoxToCreateDto boxToCreate)
        {
            var isSuccess = false;
            if (ModelState.IsValid)
                isSuccess = await _boxService.AddBox(boxToCreate);

            if (!isSuccess)
                return BadRequest();
            
            return NoContent();
        }

        [HttpPut("Boxes/Update")]
        public async Task<ActionResult> UpdateBox([FromBody]BoxToUpdateDto boxToUpdate)
        {
            var isSuccess = false;
            if (ModelState.IsValid)
                isSuccess = await _boxService.EditBox(boxToUpdate);

            if (!isSuccess)
                return BadRequest();
            
            return NoContent();
        }


        [HttpDelete("Boxes/Delete/{id}")]
        public async Task<ActionResult> DeleteBox(int id)
        {
            var isSuccess = await _boxService.RemoveBox(id);

            if (!isSuccess)
                return NotFound();
            
            return NoContent();
        }


    }
}