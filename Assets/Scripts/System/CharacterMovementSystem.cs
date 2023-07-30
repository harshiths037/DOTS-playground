using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Unity.Physics.Aspects;

[BurstCompile]
public partial struct CharacterMovementSystem : ISystem
{
    private float3 moveDirection;
    public float move_speed;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        foreach (var characterMovement in SystemAPI.Query<CharacterMovement>())
        {
            move_speed = characterMovement.MOVE_SPEED;
        }
    }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float moveX = 0;
        float moveY = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //works fine
            // foreach (var (transform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CharacterMovement>>())
            // {
            //     Debug.Log(speed);
            //     var dir = float3.zero;

            //     transform.ValueRW.Position += new float3(1, 0, 0) * dt * 5.0f;
            // }
            foreach (var playerTransform in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                        .WithAll<CharacterMovement>())
            {
                //  playerTransform.ValueRW.Position += new float3(0, 0, 1) * dt * 5.0f;

                moveY = 1;
            }

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            foreach (var playerTransform in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                        .WithAll<CharacterMovement>())
            {
                //  playerTransform.ValueRW.Position += new float3(0, 0, -1) * dt * 5.0f;
                moveY = -1;

            }

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            foreach (var playerTransform in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                        .WithAll<CharacterMovement>())
            {
                //playerTransform.ValueRW.Position += new float3(-1, 0, 0) * dt * 5.0f;
                moveX = -1;

            }

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            foreach (var playerTransform in
                    SystemAPI.Query<RefRW<LocalTransform>>()
                        .WithAll<CharacterMovement>())
            {
                // playerTransform.ValueRW.Position += new float3(1, 0, 0) * dt * 5.0f;
                moveX = 1;
            }

        }

        moveDirection = math.normalizesafe(new float3(moveX, 0, moveY));

        new CharacterMovementJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            force = moveDirection,
        }.Schedule();
    }

    [BurstCompile]
    public partial struct CharacterMovementJob : IJobEntity
    {
        public float DeltaTime;
        public float3 force;

        public void Execute(in CharacterMovement characterMovement, RigidBodyAspect rigidBodyAspect)
        {
            // ApplyLinearImpulseLocalSpace(force);
            rigidBodyAspect.LinearVelocity = force * characterMovement.MOVE_SPEED;
        }
    }


}
