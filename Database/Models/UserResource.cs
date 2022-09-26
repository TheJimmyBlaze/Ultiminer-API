
namespace Database.Models {

    public class UserResource {
        
        public string UserId {get; set;}
        public string ResourceId {get; set;}
        public int Count {get; set;}

        public User User {get; set;}
        public Resource Resource {get; set;}
    }
}