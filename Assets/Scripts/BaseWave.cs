using UnityEngine;
using UnityEngine.Jobs;

public class BaseWave : MonoBehaviour
{
    [SerializeField] protected bool m_useJobSystem;
    [SerializeField] protected bool m_isECS;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private GameObject m_ecsPrefab;
    [SerializeField] protected int m_size = 10;
    [SerializeField] protected float m_speed = 5;
    protected Vector3 m_centerPosition;
    protected GameObject[] m_cubes;
    protected Vector3[] m_positions;
    protected float[] m_distances;
    protected TransformAccessArray m_transformAccessArray;

    private void Awake()
    {
        m_transformAccessArray = new TransformAccessArray(0, -1);

        m_centerPosition = new Vector3((float)m_size / 2, 0, (float)m_size / 2);
        if (m_size % 2 == 0)
        {
            m_centerPosition += new Vector3(0.5f, 0, 0.5f);
        }

        m_cubes = new GameObject[m_size * m_size];
        m_positions = new Vector3[m_size * m_size];
        m_distances = new float[m_size * m_size];

        GameObject instance = null;
        Wave wave = null;
        for (int i = 0; i < m_size; i++)
        {
            for (int j = 0; j < m_size; j++)
            {
                if(m_isECS)
                {
                    instance = Instantiate(m_ecsPrefab, GetPosition(i, j) - m_centerPosition, Quaternion.identity);
                }
                else
                {
                    instance = Instantiate(m_prefab, GetPosition(i, j) - m_centerPosition, Quaternion.identity);
                }

                m_transformAccessArray.Add(instance.transform);

                instance.name = string.Format("Cube({0},{1})", i, j);
                instance.transform.SetParent(transform, true);

                m_cubes[i * m_size + j] = instance;
                m_positions[i * m_size + j] = instance.transform.position;
                m_distances[i * m_size + j] = GetDistance(i, j);

                if(m_isECS)
                {
                    wave = instance.GetComponent<Wave>();
                    wave.speed = m_speed;
                    wave.distance = m_distances[i * m_size + j];
                }
            }
        }
    }


    protected Vector3 GetPosition(int i, int j)
    {
        return new Vector3(i + 1, 0, j + 1);
    }


    protected float GetDistance(int i, int j)
    {
        return Vector3.Distance(m_centerPosition, GetPosition(i, j));
    }
}
