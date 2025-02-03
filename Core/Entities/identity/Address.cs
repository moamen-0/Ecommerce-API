namespace Core.Entities.identity
{
	public class Address 
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zipcode { get; set; }
		public string AppUserId { get; set; }
		public required ApplicationUser AppUser { get; set; }
		public Address()
		{
		}
		public Address(string firstName, string lastName, string street, string city, string state, string zipcode)
		{
			FirstName = firstName;
			LastName = lastName;
			Street = street;
			City = city;
			State = state;
			Zipcode = zipcode;
		}
	}
	
	}