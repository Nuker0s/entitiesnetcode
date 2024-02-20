using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

public struct PlayerInput : IInputComponentData
{
    //declare variables
    public int Horizontal;
    public int Vertical;
    public bool jump;
}

//authoriting baker
[DisallowMultipleComponent]
public class PlayerInputAuthoring : MonoBehaviour
{
    class Baking : Baker<PlayerInputAuthoring>
    {
        public override void Bake(PlayerInputAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerInput>(entity);
        }
    }
}

[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial struct SampleCubeInput : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        //get inputs
        bool left = UnityEngine.Input.GetKey("left");
        bool right = UnityEngine.Input.GetKey("right");
        bool down = UnityEngine.Input.GetKey("down");
        bool up = UnityEngine.Input.GetKey("up");
        bool jump = UnityEngine.Input.GetKeyDown(KeyCode.Space);

        foreach (var playerInput in SystemAPI.Query<RefRW<PlayerInput>>().WithAll<GhostOwnerIsLocal>())
        {
            
            playerInput.ValueRW = default;
            
            //set inputs
            if (left)
                playerInput.ValueRW.Horizontal -= 1;
            if (right)
                playerInput.ValueRW.Horizontal += 1;
            if (down)
                playerInput.ValueRW.Vertical -= 1;
            if (up)
                playerInput.ValueRW.Vertical += 1;
            if (jump)
            {
                playerInput.ValueRW.jump = true;
            }

        }
    }
}
