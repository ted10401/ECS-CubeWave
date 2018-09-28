using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class PureECS : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private Mesh m_cubeMesh;
    [SerializeField] private Material m_cubeMaterial;
    [SerializeField] private int m_size = 10;
    [SerializeField] private float m_speed = 5;

    private EntityManager m_entityManager;
    private float3 m_centerPosition;

    private void Awake()
    {
        m_camera.orthographicSize = 4 + ((float)m_size - 10) * 0.4f;

        m_entityManager = World.Active.GetOrCreateManager<EntityManager>();

        m_centerPosition = new float3((float)m_size / 2, 0, (float)m_size / 2);
        if (m_size % 2 == 0)
        {
            m_centerPosition += new float3(0.5f, 0, 0.5f);
        }

        CreateCube();
    }


    private void CreateCube()
    {
        for (int i = 0; i < m_size; i++)
        {
            for (int j = 0; j < m_size; j++)
            {
                var entity = m_entityManager.CreateEntity(
                    ComponentType.Create<Position>(),
                    ComponentType.Create<WaveData>(),
                    ComponentType.Create<MeshInstanceRenderer>()
                );

                m_entityManager.SetComponentData(entity, new Position
                {
                    Value = GetPosition(i, j) - m_centerPosition
                });

                m_entityManager.SetComponentData(entity, new WaveData
                {
                    speed = m_speed,
                    distance = GetDistance(i, j)
                });

                m_entityManager.SetSharedComponentData(entity, new MeshInstanceRenderer
                {
                    mesh = m_cubeMesh,
                    material = m_cubeMaterial
                });
            }
        }
    }


    protected float3 GetPosition(int i, int j)
    {
        return new float3(i + 1, 0, j + 1);
    }


    protected Vector3 GetVec3Position(int i, int j)
    {
        return new Vector3(i + 1, 0, j + 1);
    }


    protected float GetDistance(int i, int j)
    {
        return Vector3.Distance(m_centerPosition, GetVec3Position(i, j));
    }
}
