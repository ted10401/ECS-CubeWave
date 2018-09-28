using UnityEngine;

public class HybridECSWave : BaseWave
{
    private GameObject m_prefab;

    public HybridECSWave(Transform parent, int waveSize, float waveSpeed) : base(parent, waveSize, waveSpeed)
    {
        m_prefab = Resources.Load<GameObject>("HybridECSCube");

        CreateCubes();
    }

    public override void CreateCube(int x, int y)
    {
        GameObject instance = GameObject.Instantiate(m_prefab, GetPosition(x, y) - m_centerPosition, Quaternion.identity);
        instance.name = string.Format("Cube({0},{1})", x, y);
        instance.transform.SetParent(m_parent, true);

        HybridWaveComponent wave = instance.GetComponent<HybridWaveComponent>();
        wave.speed = m_waveSpeed;
        wave.distance = GetDistance(x, y);
    }

    public override void Tick()
    {

    }
}
