namespace Sklad_project_app.Models
{
    /// <summary>
    /// Товар - основа системы
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///>Артикул товара (уникальный номер, 6 цифр)
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ID категории товара (внешний ключ)
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// единицы измерения (внешний ключ)
        /// </summary>
        public Guid UnitId { get; set; }
        /// <summary>
        /// Категория товара (навигационное свойство)
        /// </summary>
        public virtual Category Category { get; set; }
        /// <summary>
        /// Единица измерения (навигационное свойство)
        /// </summary>
        public virtual Unit Unit { get; set; }
        /// <summary>
        /// Остаток и цена товара (навигационное свойство)
        /// </summary>
        public virtual Stock Stock { get; set; }
    }
}