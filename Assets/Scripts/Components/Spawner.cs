using System.Numerics;
using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
        // public Quaternion SpawnRotation;

    public float NextSpawnTime;
    public float SpawnRate;
}
