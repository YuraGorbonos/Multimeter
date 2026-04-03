using UnityEngine;

namespace Multimeter
{
    [CreateAssetMenu(fileName = "DeviceParameters", menuName = "Multimeter/Device Parameters")]
    public class DeviceParametersSO : ScriptableObject
    {
        public float resistance = 1000f;
        public float power = 400f;
    }
}