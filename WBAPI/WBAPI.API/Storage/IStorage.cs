using WBAPI.API.Models;

namespace WBAPI.API.Storage
{
    public interface IStorage
    {
        void SavePets(List<Pet> pets);
        List<Pet> LoadPets();
    }
}
