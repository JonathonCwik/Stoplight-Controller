using System;
using StoplightController.API.Models;
using Unosquare.RaspberryIO;
using System.Collections.Generic;

namespace StoplightController.API.Logic
{
    public class GpioStoplightStateManager : BaseStoplightStateManager
    {
        private Dictionary<LightType, int> _gpioLightMap { get; set; }
        private LightState _lightState { get; set; } = new LightState();

        public GpioStoplightStateManager(LightStateObservable observable, Dictionary<LightType, int> gpioLightMap) : base(observable)
        {
            _gpioLightMap = gpioLightMap;

            foreach (var pinNum in _gpioLightMap.Values)
            {
                var pin = Pi.Gpio[pinNum];
                pin.PinMode = Unosquare.RaspberryIO.Abstractions.GpioPinDriveMode.Output;
            }
        }

        public override void UpdateState(LightState state)
        {
            if (state.RedIsOn != _lightState.RedIsOn)
            {
                Pi.Gpio[_gpioLightMap[LightType.Red]].Write(!state.RedIsOn);
                _lightState.RedIsOn = state.RedIsOn;
            }

            if (state.YellowIsOn != _lightState.YellowIsOn)
            {
                Pi.Gpio[_gpioLightMap[LightType.Yellow]].Write(!state.YellowIsOn);
                _lightState.YellowIsOn = state.YellowIsOn;
            }

            if (state.GreenIsOn != _lightState.GreenIsOn)
            {
                Pi.Gpio[_gpioLightMap[LightType.Green]].Write(!state.GreenIsOn);
                _lightState.GreenIsOn = state.GreenIsOn;
            }
        }
    }

    public enum LightType
    {
        Red,
        Yellow,
        Green
    }
}