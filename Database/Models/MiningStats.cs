
namespace Database.Models {

    public class MiningStats {

        public string UserId {get; set;}

        public DateTime LastMine {get; set;}
        public DateTime NextMine {get; set;}

        public int TotalMineActions {get; set;}

        public User User {get; set;}
    }
}