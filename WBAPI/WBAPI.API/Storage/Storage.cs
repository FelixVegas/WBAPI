using Microsoft.Extensions.Options;
using System.Text.Json;
using WBAPI.API.Infrastructure.OptionsSettings;
using WBAPI.API.Models;

namespace WBAPI.API.Storage
{
    public class Storage : IStorage
    {
        // Pet Storage
        private readonly string _filePath;

        public Storage(IOptions<StorageSettings> storageSettings)
        {
            _filePath = storageSettings.Value.PetsFileRoute;
        }

        /// <summary>
        /// Loads data from a JSON file.
        /// </summary>
        public List<Pet> LoadPets()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Pet>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Pet>>(jsonData) ?? new List<Pet>();
        }

        /// <summary>
        /// Saves the current data to a JSON file.
        /// </summary>
        public void SavePets(List<Pet> pets)
        {
            var jsonData = JsonSerializer.Serialize(pets, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
