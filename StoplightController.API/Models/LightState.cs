using StoplightController.API.Interfaces;

namespace StoplightController.API.Models
{
    /// <summary>
    /// Model of Light State expandable via interfaces
    /// </summary>
    public class LightState : IRedState, IYellowState, IGreenState
    {
        public bool RedIsOn { get; set; }
        public bool YellowIsOn { get; set; }
        public bool GreenIsOn { get; set; }
    }
}