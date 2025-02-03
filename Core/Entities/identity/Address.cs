namespace Core.Entities.identity
{
	public class Address
	{
		public Address()
		{
		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zipcode { get; set; }
		public string AppUserId { get; set; }
		public ApplicationUser AppUser { get; set; }




	}
}