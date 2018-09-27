using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public struct PositionLayerJob : IJobParallelFor
{
    public NativeArray<Vector3> positions;
    public float[] distances;
    public float speed;
    public float time;

    public void Execute(int index)
    {
        var position = positions[index];
        position.y = Mathf.Sin(time * -speed + distances[index]);

        positions[index] = position;
    }
}
