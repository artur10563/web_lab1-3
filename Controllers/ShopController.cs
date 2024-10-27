using Labs_WEB.DTO;
using Labs_WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labs_WEB.Controllers
{
	public class ShopController : Controller
	{
		private readonly AppDbContext _context;

		public ShopController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult Purchase(int itemId)
		{
			var item = _context.Items.Where(x => x.Id == itemId).Select(x => new ItemDTO
			{
				Id = x.Id,
				Name = x.Name,
				Price = x.Price,
				Quantity = x.Quantity,
			}).FirstOrDefault();
			if (item == null) return NotFound(itemId);

			ViewBag.Item = item;

			return View();
		}

		[HttpPost]
		public ActionResult Purchase(PurchaseDTO purchase)
		{


			var item = _context.Items.FirstOrDefault(x => x.Id == purchase.ItemId);
			if (item == null) return NotFound(purchase.ItemId);
			if (item.Quantity <= 0) return BadRequest("Not in stock");

			purchase.PurchasedAt = DateTime.UtcNow;
			item.Quantity -= 1;


			_context.Items.Update(item);
			_context.Purchases.Add(new Purchase()
			{
				ItemId = purchase.ItemId,
				PurchasedAt = DateTime.Now,
				PurchasedBy = purchase.PurchasedBy,
			});

			_context.SaveChanges();
			return RedirectToAction(nameof(GetPurchases));
		}

		public ActionResult GetItems()
		{
			var itemDTOs = _context.Items
				.Select(item => new ItemDTO
				{
					Id = item.Id,
					Name = item.Name,
					Price = item.Price,
					Quantity = item.Quantity,
					Tags = item.Tags.Select(tag => new TagDTO
					{
						Id = tag.Id,
						Name = tag.Name
					}).ToList()
				}).ToList();

			
			return View(itemDTOs);
		}

		public ActionResult GetPurchases()
		{
			var items = (from pur in _context.Purchases
						 join item in _context.Items
							on pur.ItemId equals item.Id
						 select new PurchaseDTO()
						 {
							 Id = pur.Id,
							 ItemName = item.Name,
							 PurchasedAt = pur.PurchasedAt,
							 PurchasedBy = pur.PurchasedBy
						 }).ToList();

			return View(items);
		}

		#region lab2

		public ActionResult DeleteItem(int itemId)
		{
			var item = _context.Items.FirstOrDefault(x => x.Id == itemId);

			if (item == null) return NotFound();

			_context.Items.Remove(item);
			_context.SaveChanges();

			return RedirectToAction(nameof(GetItems));
		}

		[HttpGet]
		public ActionResult EditItem(int itemId)
		{
			var item = _context.Items.Select(x => new ItemDTO()
			{
				Id = x.Id,
				Name = x.Name,
				Price = x.Price,
				Quantity = x.Quantity
			}).FirstOrDefault(x => x.Id == itemId);

			if (item == null) return NotFound();

			return View(item);
		}

		[HttpPost]
		public ActionResult EditItem(ItemDTO editedItem)
		{
			var item = _context.Items.FirstOrDefault(x => x.Id == editedItem.Id);
			if (item == null) return NotFound();

			item.Quantity = editedItem.Quantity;
			item.Price = editedItem.Price;
			item.Name = editedItem.Name;

			_context.Update(item);
			_context.SaveChanges();

			return RedirectToAction(nameof(GetItems));
		}

		[HttpGet]
		public ActionResult CreateItem()
		{
			var availableTags = _context.Tags.Select(t => new TagDTO
			{
				Id = t.Id,
				Name = t.Name
			}).ToList();

			ViewBag.AvailableTags = availableTags;
			return View();
		}

		[HttpPost]
		public ActionResult CreateItem(ItemDTO newItem, int[] SelectedTagIds)
		{
			var item = new Item()
			{
				Name = newItem.Name,
				Price = newItem.Price,
				Quantity = newItem.Quantity,
				Tags = new List<Tag>()
			};

            if (SelectedTagIds != null)
            {
                foreach (var tagId in SelectedTagIds)
                {
                    var tag = _context.Tags.Find(tagId); 
                    if (tag != null)
                    {
                        item.Tags.Add(tag); 
                    }
                }
            }

			_context.Add(item);
            _context.SaveChanges();

			return RedirectToAction(nameof(GetItems));
		}

		#endregion

	}
}
