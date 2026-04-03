using System;
using UnityEngine;

namespace Multimeter
{
    public class MultimeterModel
    {
        public DeviceParametersSO DeviceParams { get; private set; }

        private float GetValueIfCurrentStateIs<T>() where T : IMultimeterState
        {
            return _currentState is T ? GetCurrentValue() : 0f;
        }

        public float GetVoltage() => GetValueIfCurrentStateIs<VoltageDCState>();
        public float GetCurrent() => GetValueIfCurrentStateIs<CurrentState>();
        public float GetResistance() => GetValueIfCurrentStateIs<ResistanceState>();
        public float GetACVoltage() => GetValueIfCurrentStateIs<VoltageACState>();
        public int CurrentStateIndex => _currentIndex;

        private IMultimeterState _currentState;
        private readonly IMultimeterState[] _states;
        private int _currentIndex;

        public MultimeterModel(DeviceParametersSO deviceParams)
        {
            DeviceParams = deviceParams;

            _states = new IMultimeterState[]
                      {
                          new NeutralState(),
                          new ResistanceState(),
                          new CurrentState(),
                          new VoltageDCState(),
                          new VoltageACState()
                      };

            _currentIndex = 0;
            _currentState = _states[_currentIndex];
        }

        public void SwitchToNextState()
        {
            _currentIndex = (_currentIndex + 1) % _states.Length;
            _currentState = _states[_currentIndex];
        }

        public void SwitchToPreviousState()
        {
            _currentIndex = (_currentIndex - 1 + _states.Length) % _states.Length;
            _currentState = _states[_currentIndex];
        }

        public void UpdateDeviceParameters(DeviceParametersSO newParams)
        {
            if (newParams == null) return;
            DeviceParams = newParams;
        }

        public float GetCurrentValue()
        {
            return _currentState.GetValue(DeviceParams.resistance, DeviceParams.power);
        }

        public string GetCurrentUnit()
        {
            return _currentState.GetUnit();
        }
    }
}