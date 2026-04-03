using UnityEngine;

namespace Multimeter
{
    public class VoltageDCState : IMultimeterState
    {
        public float GetValue(float resistance, float power)
        {
            return Mathf.Sqrt(power * resistance);
        }
        public string GetUnit() => "V";
    }
}