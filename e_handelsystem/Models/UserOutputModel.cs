namespace e_handelsystem.Models
{
    public class UserOutputModel
    {
        public UserOutputModel(int id, string firstName, string lastName, string email, string password, AddressModel address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Address = address;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AddressModel Address { get; set; }
    }
}
