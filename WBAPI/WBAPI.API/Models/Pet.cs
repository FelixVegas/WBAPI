namespace WBAPI.API.Models
{
    /// <summary>
    /// Represents a pet with an ID and a name.
    /// </summary>
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
