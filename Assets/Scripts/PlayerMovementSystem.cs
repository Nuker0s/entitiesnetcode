using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Burst;
using Unity.Physics;

[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct PlayerMovementSystem : ISystem
{

    /*[BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var speed = SystemAPI.Time.DeltaTime * 4;
        foreach (var (input, trans) in SystemAPI.Query<RefRO<PlayerInput>, RefRW<LocalTransform>>().WithAll<Simulate>())
        {
            trans.ValueRW.Position = new float3(0, 3, 0);
        }
    }*/
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var speed = SystemAPI.Time.DeltaTime * 130;//calculate time dialation
        foreach (var (input, transform, vel) in SystemAPI.Query<RefRW<PlayerInput>, RefRW<LocalTransform>,RefRW<PhysicsVelocity>>().WithAll<Simulate>())
        {
            var moveInput = new float2(input.ValueRO.Horizontal, input.ValueRO.Vertical); //get raw movement dir
            moveInput = math.normalizesafe(moveInput) * speed;//calculate movement dir

            vel.ValueRW.Linear = new float3(moveInput.x, vel.ValueRO.Linear.y, moveInput.y); // apply movement dir

            if (input.ValueRO.jump)//jumping
            {
                vel.ValueRW.Linear = new float3(0, 5, 0);
                input.ValueRW.jump = false;
            }
            
            transform.ValueRW.Rotation = quaternion.identity;// make player stand upright
        }
    }
}
