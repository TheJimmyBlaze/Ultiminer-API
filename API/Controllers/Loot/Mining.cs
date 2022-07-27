
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Loot;

namespace Controller.Loot {

    [ApiController]
    public class LootMinerController : ControllerBase {

        private readonly ILogger logger;

        private readonly LootTableIndex index;

        public LootMinerController(ILogger<LootMinerController> logger, 
            LootTableIndex index){

            this.logger = logger;

            this.index = index;
        }

        [HttpGet("Mine")]
        public IResult Mine() {
            
            return Results.Ok(index.GenerateLoot("Node.Stone"));
        }
    }
}