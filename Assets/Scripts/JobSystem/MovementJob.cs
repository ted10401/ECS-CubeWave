using UnityEngine;
using UnityEngine.Jobs;

public struct MovementJob : IJobParallelForTransform
{
    public Vector3 center;
    public float speed;
    public float distance;
    public float time;

    public void Execute(int index, TransformAccess transform)
    {
        var position = transform.position;
        position.y = Mathf.Sin(time * -speed + distance);
        transform.position = position;
    }
}
