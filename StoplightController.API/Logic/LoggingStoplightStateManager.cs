using System;
using StoplightController.API.Models;

namespace StoplightController.API.Logic
{
    public class LoggingStoplightStateManager : BaseStoplightStateManager
    {

        public LoggingStoplightStateManager(LightStateObservable observable) : base(observable)
        {
        }

        public override void UpdateState(LightState state)
        {
            Console.WriteLine($"NEW STATE: GREEN({state.GreenIsOn}), YELLOW({state.YellowIsOn}), RED({state.RedIsOn}) ");
        }
    }
}