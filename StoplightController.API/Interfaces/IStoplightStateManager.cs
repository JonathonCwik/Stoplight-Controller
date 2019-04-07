using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoplightController.API.Models;

namespace StoplightController.API.Interfaces
{
    /// <summary>
    /// Interface to Get and Set State of Lights
    /// </summary>
    public interface IStoplightStateManager
    {
        LightState GetState();

        void SetState(LightState state);
    }
}
