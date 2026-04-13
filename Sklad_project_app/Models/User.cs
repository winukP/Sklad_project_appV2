namespace Sklad_project_app.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Guid Role_id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Role Role { get; set; }
    }
}