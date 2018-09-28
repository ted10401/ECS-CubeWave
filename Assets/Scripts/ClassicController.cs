using UnityEngine;

public class ClassicController : BaseController
{
    private GameObject m_prefab;
    private GameObject[] m_cubes;
    private float[] m_distances;
    private float m_cacheTime;
    private Vector3 m_cachePosition;

    public ClassicController(Transform parent, int waveSize, float waveSpeed) : base(parent, waveSize, waveSpeed)
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

        m_cubes[x * m_waveSize + y] = instance;
        m_distances[x * m_waveSize + y] = GetDistance(x, y);
    }

    public override void Tick()
    {
        m_cacheTime = Time.realtimeSinceStartup;

        for (int x = 0; x < m_waveSize; x++)
        {
            for (int y = 0; y < m_waveSize; y++)
            {
                m_cachePosition = m_cubes[x * m_waveSize + y].transform.position;
                m_cachePosition.y = Mathf.Sin(m_cacheTime * -m_waveSpeed + m_distances[x * m_waveSize + y]);
                m_cubes[x * m_waveSize + y].transform.position = m_cachePosition;
            }
        }
    }
}
