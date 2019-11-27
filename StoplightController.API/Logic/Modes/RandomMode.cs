using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StoplightController.API.Interfaces;
using StoplightController.API.Models;

namespace StoplightController.API.Logic.Modes
{
    public class RandomMode : IMode
    {
        private Timer _timer;

        private IStoplightStateManager _stoplightStateManager;

        public void Start(IStoplightStateManager stoplightStateManager)
        {
            _stoplightStateManager = stoplightStateManager;
            _timer = new Timer(Change, stoplightStateManager, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));
        }

        private static Random _random = new Random();

        private void Change(object state)
        {
            var lightstate = new LightState()
            {
                RedIsOn = _random.Next(2) == 0,
                YellowIsOn = _random.Next(2) == 0,
                GreenIsOn = _random.Next(2) == 0
            };

            StoplightModeManager.StoplightStateManager.SetState(lightstate);
        }

        public void End()
        {
            _timer.Dispose();
            _stoplightStateManager.SetState(new LightState()
            {
                GreenIsOn = false,
                YellowIsOn = false,
                RedIsOn = false
            });
        }
    }
}
