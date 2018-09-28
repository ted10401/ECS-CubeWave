using UnityEngine;
using UnityEngine.Jobs;
using Unity.Collections;

public struct MovementJob : IJobParallelForTransform
{
    public NativeArray<float> ditances;
    public float speed;
    public float time;
    private Vector3 m_cachePosition;

    public void Execute(int index, TransformAccess transform)
    {
        m_cachePosition = transform.position;
        m_cachePosition.y = Mathf.Sin(time * -speed + ditances[index]);
        transform.position = m_cachePosition;
    }
}
