using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Samples.Common
{
    public class PureWaveSystem : JobComponentSystem
    {
        [BurstCompile]
        struct WaveJob : IJobParallelFor
        {
            public ComponentDataArray<Position> positions;
            [ReadOnly] public ComponentDataArray<PureWave> waveDatas;
            public float time;

            public void Execute(int i)
            {
                positions[i] = new Position
                {
                    Value = new float3(positions[i].Value.x, Mathf.Sin(time * -waveDatas[i].speed + waveDatas[i].distance), positions[i].Value.z)
                };
            }
        }

        ComponentGroup m_componentGroup;

        protected override void OnCreateManager()
        {
            m_componentGroup = GetComponentGroup(
                ComponentType.ReadOnly(typeof(PureWave)),
                typeof(Position));
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new WaveJob
            {
                positions = m_componentGroup.GetComponentDataArray<Position>(),
                waveDatas = m_componentGroup.GetComponentDataArray<PureWave>(),
                time = Time.realtimeSinceStartup
            };

            var jobHandle = job.Schedule(m_componentGroup.CalculateLength(), 64, inputDeps);
            return jobHandle;
        }
    }
}