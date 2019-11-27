using Microsoft.EntityFrameworkCore;
using StoplightController.API.Interfaces;
using StoplightController.API.Models;

namespace StoplightController.API.Logic
{
    public abstract class BaseStoplightStateManager : IStoplightStateManager
    {
        private readonly LightStateObservable _observable;

        internal LightState state = new LightState();

        public BaseStoplightStateManager(LightStateObservable observable)
        {
            _observable = observable;
        }

        public LightState GetState()
        {
            return state;
        }

        public void SetState(LightState newState)
        {
            state = newState;

            _observable.LightStateChanged(newState);

            UpdateState(newState);
        }

        public abstract void UpdateState(LightState state);
    }
}