using System;
using Unity.Entities;

[Serializable]
public struct PureWave : IComponentData
{
    public float speed;
    public float distance;
}

public class PureWaveComponent : ComponentDataWrapper<PureWave> { }