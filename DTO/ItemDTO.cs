namespace Labs_WEB.DTO
{
    public class ItemDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

		public List<TagDTO> Tags { get; set; }
		
	}
}
