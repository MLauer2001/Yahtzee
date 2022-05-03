using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.BL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }


        // Insert
        public User(string username, string firstName, string lastName, string password)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        // Empty
        public User()
        {

        }

        // Login
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
