namespace Sklad_project_app.Models
{
    /// <summary>
    /// Роль пользователя для разграничения доступа
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Уникальный идентификатор роли
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Список пользователей с этой ролью
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
