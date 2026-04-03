namespace Multimeter
{
    public class OffState : IMultimeterState
    {
        public float GetValue(float resistance, float power) => 0f;
        public string GetUnit() => null;
    }
}