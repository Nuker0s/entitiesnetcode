using Unity.Entities;
using UnityEngine;

public struct Player : IComponentData //playerstruct
{

}

//authoriting baker
[DisallowMultipleComponent]
public class PlayerAuthoring : MonoBehaviour
{
    class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            //Cube component = default(Cube);
            AddComponent(entity, default(Player));
           
            
        }
    }
}