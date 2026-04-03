using UnityEngine;

namespace Multimeter
{
    public class CurrentState : IMultimeterState
    {
        public float GetValue(float resistance, float power)
        {
            if (resistance <= 0) return 0;
            return Mathf.Sqrt(power / resistance);
        }
        public string GetUnit() => "A";
    }
}