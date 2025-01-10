using WBAPI.API.Models;

namespace WBAPI.API.Services.PetNameService
{
    public interface IPetService
    {
        Pet AddPet(Pet pet);

        List<Pet> GetAllPets();

        Pet GetPetById(int id);

        Pet UpdatePet(int id, string newName);

        bool DeletePet(int id);
    }
}
