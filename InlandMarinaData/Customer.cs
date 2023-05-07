using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
	[Table("Customers")]
	public class Customer
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Please enter a username.")]
		[StringLength(30)]
		public string Username { get; set; }

		[Required(ErrorMessage = "Please enter a password.")]
		[StringLength(30)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please enter first name.")]
		[StringLength(30)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter last name.")]
		[StringLength(30)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter a phone number.")]
		[RegularExpression("[0-9]{3}[-][0-9]{3}[-][0-9]{4}", ErrorMessage ="Must be a valid phone number")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Please enter a city.")]
		[StringLength(30)]
		public string City { get; set; }

		// navigation property
		public virtual ICollection<Lease> Leases { get; set; }
	}
}
