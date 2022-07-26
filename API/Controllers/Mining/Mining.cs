
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Loot;

namespace Controller.Mining {

    [ApiController]
    public class MiningController : ControllerBase {

        private readonly LootTableIndex index;

        public MiningController(LootTableIndex index) => this.index = index;

        [HttpGet("Mine")]
        [AllowAnonymous]
        public IResult Mine() {
            
            return Results.Ok(index.GenerateLoot("Node.Stone"));
        }
    }
}