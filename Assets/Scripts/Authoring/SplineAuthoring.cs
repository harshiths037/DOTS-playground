using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

class SplineAuthoring : MonoBehaviour
{
    ComputeShader m_ComputeShader;
    int m_Segments;
}

class SplineBaker : Baker<SplineAuthoring>
{
    public override void Bake(SplineAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new SplineAddKnots
        {
          //  m_Segments = authoring.m_
        });
    }
}
