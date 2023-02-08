namespace MyPet.Domain.Entidades
{
    public class Nominatim
    {
        public int place_id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }
        public object address { get; set; }
        public List<string> boundingbox { get; set; }

    }
}
