using Microsoft.AspNetCore.Mvc;
using StoplightController.API.Interfaces;
using StoplightController.API.Logic;
using StoplightController.API.Logic.Modes;

namespace StoplightController.API.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        public ApiController()
        {

        }

        [HttpGet("/current-state")]
        public IActionResult CurrentState()
        {
            return Ok(StoplightModeManager.StoplightStateManager.GetState());
        }

        [HttpPost("manual")]
        public IActionResult StartManualMode()
        {
            var mode = new ManualMode();

            StoplightModeManager.SetMode(mode);

            return Ok();
        }

        [HttpPost("random")]
        public IActionResult StartRandomMode()
        {
            var mode = new RandomMode();

            StoplightModeManager.SetMode(mode);

            return Ok();
        }

        [HttpPost("manual/toggle-red")]
        public IActionResult ManualToggleRed()
        {
            var state = StoplightModeManager.StoplightStateManager.GetState();
            state.RedIsOn = !state.RedIsOn;
            StoplightModeManager.StoplightStateManager.SetState(state);

            return Ok(state);
        }

        [HttpPost("manual/toggle-yellow")]
        public IActionResult ManualToggleYellow()
        {
            var state = StoplightModeManager.StoplightStateManager.GetState();
            state.YellowIsOn = !state.YellowIsOn;
            StoplightModeManager.StoplightStateManager.SetState(state);

            return Ok(state);
        }

        [HttpPost("manual/toggle-green")]
        public IActionResult ManualToggleGreen()
        {
            var state = StoplightModeManager.StoplightStateManager.GetState();
            state.GreenIsOn = !state.GreenIsOn;
            StoplightModeManager.StoplightStateManager.SetState(state);

            return Ok(state);
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            StoplightModeManager.EndCurrentMode();

            return Ok();
        }

    }
}