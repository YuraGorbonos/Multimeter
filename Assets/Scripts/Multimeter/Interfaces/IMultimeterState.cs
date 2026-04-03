namespace Multimeter
{
    public interface IMultimeterState
    {
        float GetValue(float resistance, float power);
        string GetUnit();
    }
}