using UnityEngine;
using Unity.Entities;

public class HybridWaveComponent : MonoBehaviour
{
    public float speed;
    public float distance;
}

public class HybridWaveSystem : ComponentSystem
{
    struct Components
    {
        public Transform transform;
        public HybridWaveComponent wave;
    }

    private ComponentGroupArray<Components> m_cacheComponents;
    private Vector3 m_cachePosition;
    private float m_cacheTime;

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        m_cacheComponents = GetEntities<Components>();
    }

    protected override void OnUpdate()
    {
        m_cacheTime = Time.realtimeSinceStartup;

        foreach (var e in m_cacheComponents)
        {
            m_cachePosition = e.transform.position;
            m_cachePosition.y = Mathf.Sin(m_cacheTime * -e.wave.speed + e.wave.distance);
            e.transform.position = m_cachePosition;
        }
    }
}