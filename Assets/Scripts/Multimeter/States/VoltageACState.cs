namespace Multimeter
{
    public class VoltageACState : IMultimeterState
    {
        public float GetValue(float resistance, float power) => 0.01f;
        public string GetUnit() => "V~";
    }
}