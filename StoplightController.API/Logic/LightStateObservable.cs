using System;
using System.Collections.Generic;
using StoplightController.API.Models;

namespace StoplightController.API.Logic
{
    public class LightStateObservable : IObservable<LightState>
    {
        private readonly List<IObserver<LightState>> _observers = new List<IObserver<LightState>>();

        public IDisposable Subscribe(IObserver<LightState> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<LightState>(_observers, observer);
        }

        public void LightStateChanged(LightState lightState)
        {
            _observers.ForEach(o =>
            {
                o.OnNext(lightState);
            });
        }
    }
}