namespace IVY.Application.DTOs
{
    public class OutfitAddDTO
    {
        public required string Outfit__Key { get; set; }
        public required int Outfit__ProductSubColorId { get; set; }
    }
    public class OutfitGetDTO
    {
        public int Outfit__Id { get; set; }
        public required string Outfit__Key { get; set; }
        public required int Outfit__ProductSubColorId { get; set; }
    }
}