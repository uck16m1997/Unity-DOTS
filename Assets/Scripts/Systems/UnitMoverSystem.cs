using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

partial struct UnitMoverSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (lt, unitMover, physicsVelocity)
                 in SystemAPI.Query<
                     RefRW<LocalTransform> , 
                     RefRO<UnitMover>,
                    RefRW<PhysicsVelocity>>()) {


            float3 moveDir = unitMover.ValueRO.targetPosition - lt.ValueRO.Position;
            moveDir = math.normalize(moveDir);

            float rotSpeed = unitMover.ValueRO.rotationSpeed;
            lt.ValueRW.Rotation = math.slerp(lt.ValueRW.Rotation, 
                quaternion.LookRotation(moveDir, math.up()), SystemAPI.Time.DeltaTime * rotSpeed);
            
            // lt.ValueRW.Position = lt.ValueRO.Position +  moveDir * moveSpeed.ValueRO.value * SystemAPI.Time.DeltaTime;
            physicsVelocity.ValueRW.Linear = moveDir * unitMover.ValueRO.moveSpeed;
            physicsVelocity.ValueRW.Angular = float3.zero;
        }
    }


}
