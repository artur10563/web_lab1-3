namespace Labs_WEB.Models
{
	public class Purchase
	{
		public int Id { get; set; }
		public string PurchasedBy { get; set; }
		public DateTime PurchasedAt { get; set; }
		public int ItemId { get; set; }
		public virtual Item Item { get; set; }
	}
}
