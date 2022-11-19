
using System.ComponentModel.DataAnnotations;

namespace Database.Models {

    public class UserNode {

        [Key]
        public string UserId {get; set;}
        public string SelectedNodeId {get; set;}

        public User User {get; set;}
        public Node Node {get; set;}
    }
}