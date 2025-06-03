namespace FruityViceApi.Application.Models
{
    public class Fruit
    {
        public int id { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string genus { get; set; }
        public string order { get; set; }
        public Nutrition nutritions { get; set; } = new Nutrition();
    }
}
