namespace HTMLEngine.Models
{
    public class Professor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Discipline { get; set; }
        public Group Group { get; set; }
    }
}