using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Multimeter
{
    public class MultimeterView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
    {
        public Action onScrollUp;
        public Action onScrollDown;

        [SerializeField]
        private TMP_Text _displayText;

        [SerializeField]
        private bool _animatedDisplay = true;

        [SerializeField]
        private Transform _regulatorTransform;

        [SerializeField]
        private Vector3 _rotationAxis = Vector3.up;

        [SerializeField]
        private float[] _stateAngles;

        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private Material _defaultMaterial;

        [SerializeField]
        private Material _highlightedMaterial;

        private bool _isPointerOver;

        private void Awake()
        {
            if (_renderer != null && _defaultMaterial != null)
            {
                _renderer.material = _defaultMaterial;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;

            if (_renderer != null && _highlightedMaterial != null)
            {
                _renderer.material = _highlightedMaterial;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;

            if (_renderer != null && _defaultMaterial != null)
            {
                _renderer.material = _defaultMaterial;
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (!_isPointerOver)
            {
                return;
            }

            if (eventData.scrollDelta.y > 0)
            {
                onScrollUp?.Invoke();
            }
            else if (eventData.scrollDelta.y < 0)
            {
                onScrollDown?.Invoke();
            }
        }

        public void AnimateRegulatorRotation(int targetStateIndex)
        {
            if (_regulatorTransform == null || _stateAngles == null || _stateAngles.Length == 0)
            {
                return;
            }

            if (targetStateIndex < 0 || targetStateIndex >= _stateAngles.Length)
            {
                return;
            }

            _regulatorTransform.localRotation = Quaternion.AngleAxis(_stateAngles[targetStateIndex], _rotationAxis);
        }

        public IEnumerator AnimateNumberChange(float fromValue, float toValue, string unit, float duration = 0.3f)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                UpdateDisplayImmediate(Mathf.Lerp(fromValue, toValue, elapsed / duration), unit);
                yield return null;
            }

            UpdateDisplayImmediate(toValue, unit);
        }

        public void UpdateDisplay(float value, string unit)
        {
            if (unit == null)
            {
                if (_displayText != null)
                {
                    _displayText.text = "";
                }

                return;
            }

            if (!_animatedDisplay || _displayText == null)
            {
                UpdateDisplayImmediate(value, unit);
                return;
            }

            float currentValue = value;
            string currentText = _displayText.text;

            if (!string.IsNullOrEmpty(currentText))
            {
                string[] parts = currentText.Split(' ');

                if (parts.Length > 0 && float.TryParse(parts[0], out float parsed))
                {
                    currentValue = parsed;
                }
            }

            StartCoroutine(AnimateNumberChange(currentValue, value, unit));
        }

        private void UpdateDisplayImmediate(float value, string unit)
        {
            if (_displayText != null)
            {
                _displayText.text = $"{value:F2} {unit}".Trim();
            }
        }
    }
}