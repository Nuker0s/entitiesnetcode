using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerEntity : MonoBehaviour
{
    public float speed = 100;
    public class PlayerBaker : Baker<PlayerEntity>
    {
        
        public override void Bake(PlayerEntity authoring)
        {
            //throw new System.NotImplementedException();
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Playerdata()
            {
                speed = authoring.speed,
            });
            Debug.Log(authoring.speed);
            
        }
    }
}
