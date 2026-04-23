namespace Sklad_project_app.Models
{
    /// <summary>
    /// Категория товара
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список товаров в этой категории
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}