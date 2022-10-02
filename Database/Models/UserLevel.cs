
using System.ComponentModel.DataAnnotations;

namespace Database.Models {
    
    public class UserLevel {

        [Key]
        public string UserId {get; set;}
        public int Level {get; set;}
        public int TotalExperience {get; set;}
        public int LevelExperience {get; set;}
        
        public User User {get; set;}
    }
}