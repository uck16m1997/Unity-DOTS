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
        foreach (var (lt, moveSpeed, physicsVelocity)
                 in SystemAPI.Query<
                     RefRW<LocalTransform> , 
                     RefRO<MoveSpeed>,
                    RefRW<PhysicsVelocity>>()) {


            float3 targetPos = MouseWorldPosition.I.GetPosition();
            float3 moveDir = targetPos - lt.ValueRO.Position;
            moveDir = math.normalize(moveDir);

            float rotSpeed = 10f;
            lt.ValueRW.Rotation = math.slerp(lt.ValueRW.Rotation, quaternion.LookRotation(moveDir, math.up()), SystemAPI.Time.DeltaTime * rotSpeed);
            
            // lt.ValueRW.Position = lt.ValueRO.Position +  moveDir * moveSpeed.ValueRO.value * SystemAPI.Time.DeltaTime;
            physicsVelocity.ValueRW.Linear = moveDir * moveSpeed.ValueRO.value;
            physicsVelocity.ValueRW.Angular = float3.zero;
        }
    }


}
