using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class CharacterMovementAuthoring : MonoBehaviour
{
  public float speed = 10;
}

class CharacterMovementBaker : Baker<CharacterMovementAuthoring>
{
    public override void Bake(CharacterMovementAuthoring authoring)
    {
         var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new CharacterMovement
        {
           MOVE_SPEED = authoring.speed
        });
       
    }
}
