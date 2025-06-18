using workstation_back_end.Shared.Domain.Model.Entities;

namespace workstation_back_end.Experience.Domain.Models.Entities
{
    public class User : BaseEntity
    {
        public User(string firstName, string lastName, string number, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            Email = email;
            Password = password;
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}