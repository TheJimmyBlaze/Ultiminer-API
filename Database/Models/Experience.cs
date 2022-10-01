
using System.ComponentModel.DataAnnotations;

namespace Database.Models {
    
    public class Experience {

        [Key]
        public string UserId {get; set;}
        public int TotalExperience {get; set;}
        
        public User User {get; set;}
    }
}