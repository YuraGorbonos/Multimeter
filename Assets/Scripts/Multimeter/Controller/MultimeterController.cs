using UnityEngine;

namespace Multimeter
{
    public class MultimeterController : MonoBehaviour
    {
        [Header("Current Device Parameters"), SerializeField]
        private DeviceParametersSO _currentDeviceParameters;

        [SerializeField]
        private MultimeterView _view;
        [SerializeField]
        private MultimeterUI _ui;

        private MultimeterModel _model;

        private void Awake()
        {
            if (_currentDeviceParameters == null)
            {
                Debug.LogError("DeviceParametersSO not assigned to MultimeterController!");
                return;
            }

            _model = new MultimeterModel(_currentDeviceParameters);

            _view.onScrollUp += () => SwitchState(+1);
            _view.onScrollDown += () => SwitchState(-1);
        }

        private void Start()
        {
            _view.AnimateRegulatorRotation(_model.CurrentStateIndex);
            UpdateView();
        }

        public MultimeterModel GetModel()
        {
            return _model;
        }

        public void ChangeDeviceParameters(DeviceParametersSO newParams)
        {
            if (newParams == null)
            {
                return;
            }

            _currentDeviceParameters = newParams;
            _model.UpdateDeviceParameters(newParams);
            UpdateView();
        }

        private void SwitchState(int delta)
        {
            if (delta > 0)
            {
                _model.SwitchToNextState();
            }
            else
            {
                _model.SwitchToPreviousState();
            }

            _view.AnimateRegulatorRotation(_model.CurrentStateIndex);
            UpdateView();
        }

        private void UpdateView()
        {
            _view.UpdateDisplay(_model.GetCurrentValue(), _model.GetCurrentUnit());
            _ui.UpdateAllValues();
        }
    }
}