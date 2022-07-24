
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DropTables;

namespace Controller.Mining {

    [ApiController]
    public class MiningController : ControllerBase {

        private readonly DropTableIndex index;

        public MiningController(DropTableIndex index) => this.index = index;

        [HttpGet("Mine")]
        [AllowAnonymous]
        public IResult Mine() {
            
            return Results.Ok();
        }
    }
}