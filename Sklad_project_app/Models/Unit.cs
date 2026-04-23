namespace Sklad_project_app.Models
{
    /// <summary>
    /// Единица измерения товара
    /// </summary>
    public class Unit
    {
        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// >Название единицы измерения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// >Список товаров с этой единицей измерения
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}