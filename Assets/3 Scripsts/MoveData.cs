using UnityEngine;

public class MoveData
{
    public Vector3 direction;
    public float time;

    public MoveData(Vector2 dir, float time)
    {
        direction = dir;
        this.time = time;
    }

    public override string ToString()
    {
        return $"direction: {direction}, time: {time}";
    }
}
