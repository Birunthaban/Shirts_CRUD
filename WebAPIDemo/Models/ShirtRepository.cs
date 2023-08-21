namespace WebAPIDemo.Models
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
    {
        new Shirt { Id = 1, Brand = "PUMA", Color = "Blue", Gender = "Men", Size = 10, Price = 2000 },
         new Shirt { Id = 2, Brand = "NIKE", Color = "Green", Gender = "Women", Size = 7, Price = 3000 },
          new Shirt { Id = 3, Brand = "PUMA", Color = "Yellow", Gender = "Men", Size = 9, Price = 4000 },
           new Shirt { Id = 4, Brand = "NIKE", Color = "Red", Gender = "Woen", Size = 8, Price = 5000 },
     };
        public static bool ShirtExists(int id)
        {
            return shirts.Any(shirt => shirt.Id == id);
        }
        public static Shirt? getShirtById(int id)
        {
            return shirts.FirstOrDefault(shirt => shirt.Id == id);
        }
        public static List<Shirt> getAllShirts()
        {
            return shirts;
        }
        public static void createShirt(Shirt shirt)
        {
            int maxId = shirts.Max(shirt => shirt.Id);
            shirt.Id = maxId+1;
            shirts.Add(shirt);

        }
        public static Shirt? GetShirtByProperties(string? brand , 
            string?gender, string?color,int? size)
        {
            return shirts.FirstOrDefault(x =>
          !string.IsNullOrWhiteSpace(brand) &&
          !string.IsNullOrWhiteSpace(x.Brand) &&
          x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
          !string.IsNullOrWhiteSpace(gender) &&
          !string.IsNullOrWhiteSpace(x.Gender) &&
          x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
          !string.IsNullOrWhiteSpace(color) &&
          !string.IsNullOrWhiteSpace(x.Color) &&
          x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
          size.HasValue && x.Size.HasValue && size.Value == x.Size.Value);

        }
        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate =shirts.First(x=>x.Id == shirt.Id);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price = shirt.Price;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Size = shirt.Size;
        }
    }
}
