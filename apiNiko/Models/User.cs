namespace apiNiko.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set;}

        public List<TodoTask> Tasks { get; set; } = new();
   }
}
