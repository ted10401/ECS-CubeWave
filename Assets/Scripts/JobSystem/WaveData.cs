using System;
using Unity.Entities;

[Serializable]
public struct WaveData : IComponentData
{
    public float speed;
    public float distance;
}

public class WaveDataComponent : ComponentDataWrapper<WaveData> { }