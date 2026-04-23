namespace Sklad_project_app.Models
{
    /// <summary>
    /// Шапка отгрузки
    /// </summary>
    public class Shipment
    {
        /// <summary>
        /// Уникальный идентификатор отгрузки
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ID клиента (внешний ключ)
        /// </summary>
        public Guid ClientId { get; set; }
        /// <summary>
        /// ID пользователя, создавшего отгрузку
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Дата отгрузки
        /// </summary>
        public DateTime? ShipmentDate { get; set; }
        /// <summary>
        /// Клиент (навигационное свойство)
        /// </summary>
        public virtual Client Client { get; set; }
        /// <summary>
        /// Пользователь (навигационное свойство)
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// Список товаров в отгрузке
        /// </summary>
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}