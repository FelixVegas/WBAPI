using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBAPI.API.Models;
using WBAPI.API.Services.PetNameService;

namespace WBAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PetController : Controller
    {
        private readonly ILogger<PetController> _logger;

        private readonly IPetService _petService;

        /// <summary>
        /// Constructor that injects the PetService.
        /// </summary>
        /// <param name="petService">The PetService instance.</param>
        public PetController(ILogger<PetController> logger, IPetService petService)
        {
            _petService = petService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all pets.
        /// </summary>
        /// <returns>A list of all pets.</returns>
        [HttpGet]
        public IActionResult GetAllPets()
        {
            var pets = _petService.GetAllPets();
            return Ok(pets);
        }

        /// <summary>
        /// Retrieves a specific pet by its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to retrieve.</param>
        /// <returns>The pet if found, or a 404 Not Found response.</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetPetById(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound(new { Message = "Pet not found." });
            }
            return Ok(pet);
        }

        /// <summary>
        /// Adds a new pet.
        /// </summary>
        /// <param name="pet">The pet to add.</param>
        /// <returns>The added pet with its generated ID.</returns>
        [HttpPost]
        public IActionResult AddPet([FromBody] Pet pet)
        {
            if (string.IsNullOrWhiteSpace(pet.Name))
            {
                return BadRequest(new { Message = "Pet name is required." });
            }
            var addedPet = _petService.AddPet(pet);
            return CreatedAtAction(nameof(GetPetById), new { id = addedPet.Id }, addedPet);
        }

        /// <summary>
        /// Updates an existing pet's name.
        /// </summary>
        /// <param name="id">The ID of the pet to update.</param>
        /// <param name="pet">The updated pet data.</param>
        /// <returns>The updated pet or a 404 Not Found response if the pet doesn't exist.</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdatePet(int id, [FromBody] Pet pet)
        {
            if (string.IsNullOrWhiteSpace(pet.Name))
            {
                return BadRequest(new { Message = "Pet name is required." });
            }
            var updatedPet = _petService.UpdatePet(id, pet.Name);
            if (updatedPet == null)
            {
                return NotFound(new { Message = "Pet not found." });
            }
            return Ok(updatedPet);
        }

        /// <summary>
        /// Deletes a pet by its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to delete.</param>
        /// <returns>A 204 No Content response if deleted, or a 404 Not Found response if the pet doesn't exist.</returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeletePet(int id)
        {
            var deleted = _petService.DeletePet(id);
            if (!deleted)
            {
                return NotFound(new { Message = "Pet not found." });
            }
            return NoContent();
        }
    }
}
