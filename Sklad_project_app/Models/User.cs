namespace Sklad_project_app.Models
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User
    {
        /// <summary>
        /// >Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// ID роли (внешний ключ)
        /// </summary>
        public Guid Role_id { get; set; }
        /// <summary>
        /// Логин для входа
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль для входа
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Роль пользователя (навигационное свойство)
        /// </summary>
        public virtual Role Role { get; set; }
    }
}