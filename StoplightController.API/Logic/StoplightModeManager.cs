using System;
using StoplightController.API.Interfaces;
using StoplightController.API.Models;

namespace StoplightController.API.Logic
{
    public static class StoplightModeManager
    {
        public static IStoplightStateManager StoplightStateManager { get; set; }

        public static LightStateObservable Observable { get; set; }

        public static IDisposable Subscribe(IObserver<LightState> observable)
        {
            return Observable.Subscribe(observable);
        }

        public static void Initialize(IStoplightStateManager stoplightStateManager)
        {
            StoplightStateManager = stoplightStateManager;
        }

        public static IMode CurrentMode { get; set; }

        public static void SetMode(IMode mode)
        {
            if (CurrentMode != null)
            {
                EndCurrentMode();
            }

            CurrentMode = mode;
            CurrentMode.Start(StoplightStateManager);
        }

        public static void EndCurrentMode()
        {
            CurrentMode.End();
            CurrentMode = null;
            StoplightStateManager.SetState(new LightState
            {
                GreenIsOn = false,
                RedIsOn = false,
                YellowIsOn = false
            });
        }
    }
}