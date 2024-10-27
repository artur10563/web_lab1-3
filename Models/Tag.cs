namespace Labs_WEB.Models
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public virtual ICollection<Item> Items { get; set; }
	
	}
}
