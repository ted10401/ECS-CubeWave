using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

public class ClassicJobSystemController : BaseController
{
    private GameObject m_prefab;
    private GameObject[] m_cubes;
    private float[] m_distances;
    private float m_cacheTime;
    private Vector3 m_cachePosition;

    protected TransformAccessArray m_transformAccessArray = new TransformAccessArray(0, -1);
    private NativeArray<float> m_distanceArray;
    private WaveJob m_job;
    private JobHandle m_jobHandle;

    public ClassicJobSystemController(Transform parent, int waveSize, float waveSpeed) : base(parent, waveSize, waveSpeed)
    {
        m_cubes = new GameObject[m_waveSize * m_waveSize];
        m_distances = new float[m_waveSize * m_waveSize];
        m_prefab = Resources.Load<GameObject>("Cube");

        CreateCubes();
    }

    public override void CreateCube(int x, int y)
    {
        GameObject instance = GameObject.Instantiate(m_prefab, GetPosition(x, y) - m_centerPosition, Quaternion.identity);
        instance.name = string.Format("Cube({0},{1})", x, y);
        instance.transform.SetParent(m_parent, true);

        m_transformAccessArray.Add(instance.transform);
        m_cubes[x * m_waveSize + y] = instance;
        m_distances[x * m_waveSize + y] = GetDistance(x, y);
    }

    public override void OnCreateCubeComplete()
    {
        m_distanceArray = new NativeArray<float>(m_distances, Allocator.Persistent);
    }

    public override void Tick()
    {
        m_jobHandle.Complete();

        m_job = new WaveJob();
        m_job.ditances = m_distanceArray;
        m_job.time = Time.realtimeSinceStartup;
        m_job.speed = m_waveSpeed;
        m_jobHandle = m_job.Schedule(m_transformAccessArray);
    }

    public override void Destroy()
    {
        m_distanceArray.Dispose();
        m_transformAccessArray.Dispose();
        m_jobHandle.Complete();
    }
}
