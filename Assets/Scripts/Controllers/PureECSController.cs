using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class PureECSController : BaseController
{
    private EntityManager m_entityManager;
    private Mesh m_mesh;
    private Material m_material;

    public PureECSController(Transform parent, int waveSize, float waveSpeed) : base(parent, waveSize, waveSpeed)
    {
        m_entityManager = World.Active.GetOrCreateManager<EntityManager>();
        GameObject prefab = Resources.Load<GameObject>("Cube");
        m_mesh = prefab.GetComponent<MeshFilter>().sharedMesh;
        m_material = prefab.GetComponent<MeshRenderer>().sharedMaterial;

        CreateCubes();
    }

    public override void CreateCube(int x, int y)
    {
        var entity = m_entityManager.CreateEntity(
                    ComponentType.Create<Position>(),
                    ComponentType.Create<PureWave>(),
                    ComponentType.Create<MeshInstanceRenderer>()
                );

        m_entityManager.SetComponentData(entity, new Position
        {
            Value = GetPosition(x, y) - m_centerPosition
        });

        m_entityManager.SetComponentData(entity, new PureWave
        {
            speed = m_waveSpeed,
            distance = GetDistance(x, y)
        });

        m_entityManager.SetSharedComponentData(entity, new MeshInstanceRenderer
        {
            mesh = m_mesh,
            material = m_material
        });
    }

    public override void Tick()
    {

    }
}
