namespace Sklad_project_app.Models
{
    /// <summary>
    ///  Детали отгрузки
    /// </summary>
    public class ShipmentItem
    {
        /// <summary>
        /// Уникальный идентификатор записи
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ID отгрузки (внешний ключ)
        /// </summary>
        public Guid ShipmentId { get; set; }
        /// <summary>
        /// ID товара (внешний ключ)
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Количество отгруженного товара
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Общая сумма по позиции
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Шапка отгрузки (навигационное свойство)
        /// </summary>
        public virtual Shipment Shipment { get; set; }
        /// <summary>
        /// Товар (навигационное свойство)
        /// </summary>
        public virtual Product Product { get; set; }
    }
}