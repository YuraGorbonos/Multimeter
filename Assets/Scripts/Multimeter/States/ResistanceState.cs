namespace Multimeter
{
    public class ResistanceState : IMultimeterState
    {
        public float GetValue(float resistance, float power) => resistance;
        public string GetUnit() => "Ω";
    }
}