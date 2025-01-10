using WBAPI.API.Models;
using WBAPI.API.Storage;

namespace WBAPI.API.Services.PetNameService
{
    public class PetService : IPetService
    {
        private readonly ILogger<PetService> _logger;
        private readonly IStorage _storage;

        private List<Pet> _pets; 

        public PetService(ILogger<PetService> logger, IStorage storage)
        {
            _logger = logger;
            _storage = storage;
            _pets = _storage.LoadPets();
        }

        /// <summary>
        /// Adds a new pet to the list.
        /// </summary>
        /// <param name="pet">The pet object to be added.</param>
        /// <returns>The added pet with its generated ID.</returns>
        public Pet AddPet(Pet pet)
        {
            pet.Id = _pets.Count > 0 ? _pets.Max(p => p.Id) + 1 : 1;
            _pets.Add(pet);
            _storage.SavePets(_pets);
            return pet; 
        }

        /// <summary>
        /// Retrieves all pets from the list.
        /// </summary>
        /// <returns>A list of all pets.</returns>
        public List<Pet> GetAllPets()
        {
            _pets = _storage.LoadPets();
            return _pets;
        }

        /// <summary>
        /// Retrieves a pet by its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to retrieve.</param>
        /// <returns>The pet with the specified ID, or null if not found.</returns>
        public Pet GetPetById(int id)
        {
            _pets = _storage.LoadPets();
            return _pets.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Updates an existing pet's name by its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to update.</param>
        /// <param name="newName">The new name for the pet.</param>
        /// <returns>The updated pet, or null if not found.</returns>
        public Pet UpdatePet(int id, string newName)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet != null)
            {
                pet.Name = newName;
                _storage.SavePets(_pets);
                return pet;
            }
            return null;
        }

        /// <summary>
        /// Deletes a pet by its ID.
        /// </summary>
        /// <param name="id">The ID of the pet to delete.</param>
        /// <returns>True if the pet was deleted, false if not found.</returns>
        public bool DeletePet(int id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet != null)
            {
                _pets.Remove(pet);
                _storage.SavePets(_pets);
                return true;
            }
            return false;
        }
    }
}
