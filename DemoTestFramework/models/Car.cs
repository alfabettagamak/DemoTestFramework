namespace DemoTestFramework.models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        
        public int PersonId { get; set; }
        
        public Person Person { get; set; }
    }
}