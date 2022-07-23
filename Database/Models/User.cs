
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class User {

        [Key]
        public string UserId {get; set;}

        public List<UserResource> Resources {get; set;}
    }
}