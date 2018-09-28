using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

public class CubeWave : BaseWave
{
    [SerializeField] private Camera m_camera;
    private Vector3 m_cachePosition;
    private float m_cacheTime;
    private MovementJob m_movementJob;
    private JobHandle m_jobHandle;

    private NativeArray<float> m_distanceArray;

    private void Start()
    {
        m_camera.orthographicSize = 4 + ((float)m_size - 10) * 0.4f;

        if (m_useJobSystem)
        {
            m_distanceArray = new NativeArray<float>(m_distances, Allocator.Persistent);
        }
    }

    private void Update()
    {
        if (m_isECS)
        {
            return;
        }

        if (m_useJobSystem)
        {
            ExecutePositionJobs();
        }
        else
        {
            m_cacheTime = Time.realtimeSinceStartup;

            for (int i = 0; i < m_size; i++)
            {
                for (int j = 0; j < m_size; j++)
                {
                    m_cachePosition = m_cubes[i * m_size + j].transform.position;
                    m_cachePosition.y = Mathf.Sin(m_cacheTime * -m_speed + m_distances[i * m_size + j]);
                    m_cubes[i * m_size + j].transform.position = m_cachePosition;
                }
            }
        }
    }

    private void OnDisable()
    {
        m_transformAccessArray.Dispose();
        m_jobHandle.Complete();
        if (m_useJobSystem)
        {
            m_distanceArray.Dispose();
        }
    }

    private void ExecutePositionJobs()
    {
        m_movementJob = new MovementJob();
        m_movementJob.ditances = m_distanceArray;
        m_movementJob.time = Time.realtimeSinceStartup;
        m_movementJob.speed = m_speed;
        m_jobHandle = m_movementJob.Schedule(m_transformAccessArray);
    }

    private void LateUpdate()
    {
        m_jobHandle.Complete();
    }
}
