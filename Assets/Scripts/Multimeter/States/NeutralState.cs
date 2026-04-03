namespace Multimeter
{
    public class NeutralState : IMultimeterState
    {
        public float GetValue(float resistance, float power) => 0f;

        public string GetUnit() => "";
    }
}