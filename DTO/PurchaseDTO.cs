namespace Labs_WEB.DTO
{
	public class PurchaseDTO
	{
		public int Id { get; set; }
		public string PurchasedBy { get; set; }
		public DateTime PurchasedAt { get; set; }
		public string ItemName { get; set; }

		public int ItemId { get; set; }
	}
}
