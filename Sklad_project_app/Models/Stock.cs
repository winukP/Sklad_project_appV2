namespace Sklad_project_app.Models
{
    /// <summary>
    /// Остатки товара на складе и цена закупки
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Уникальный идентификатор записи остатка
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// >ID товара (внешний ключ)
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Цена закупки товара 
        /// </summary>
        public decimal PurchasePrice { get; set; }
        /// <summary>
        /// Текущий остаток товара на складе
        /// </summary>
        public int Rest { get; set; }
        /// <summary>
        /// Товар (навигационное свойство)
        /// </summary>
        public virtual Product Product { get; set; }
    }
}