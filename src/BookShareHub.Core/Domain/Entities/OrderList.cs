using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShareHub.Core.Domain.Entities;

public class OrderList
{
	public int OrderId { get; set; }
	public int BookId { get; set; }
	public int UserID { get; set; }
}
