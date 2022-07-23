
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class User {

        [Key]
        public string DiscordId {get; set;}
    }
}