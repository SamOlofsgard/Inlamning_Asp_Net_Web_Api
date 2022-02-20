namespace e_handelsystem.Models
{
    public class AddressModel
    {
        public AddressModel(string addressLine, string postalCode, string city)
        {
            AddressLine = addressLine;
            PostalCode = postalCode;
            City = city;
        }

        public string AddressLine { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
