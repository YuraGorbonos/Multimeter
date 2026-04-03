using TMPro;
using UnityEngine;

namespace Multimeter
{
    public class MultimeterUI : MonoBehaviour
    {
        [Header("UI Text References"), SerializeField]
        private TMP_Text _voltageText;

        [SerializeField]
        private TMP_Text _currentText;

        [SerializeField]
        private TMP_Text _resistanceText;

        [SerializeField]
        private TMP_Text _acVoltageText;

        [Header("References"), SerializeField]
        private MultimeterController _controller;

        private MultimeterModel _model;

        private void Start()
        {
            if (_controller == null)
            {
                Debug.Log("Ссылки нет");
                return;
            }

            _model = _controller.GetModel();

            if (_model == null)
            {
                return;
            }

            UpdateAllValues();
        }

        public void UpdateAllValues()
        {
            SetAllValues(_model.GetVoltage(), _model.GetCurrent(), _model.GetResistance(), _model.GetACVoltage());
        }

        private void SetAllValues(float v, float a, float r, float vac)
        {
            if (_voltageText != null)
            {
                _voltageText.text = $"{v:F2} V";
            }

            if (_currentText != null)
            {
                _currentText.text = $"{a:F2} A";
            }

            if (_resistanceText != null)
            {
                _resistanceText.text = $"{r:F2} Ω";
            }

            if (_acVoltageText != null)
            {
                _acVoltageText.text = $"{vac:F2} V~";
            }
        }
    }
}