using UnityEngine;
using Leopotam.Ecs;

namespace Yohoho.Scripts.Events
{
    public struct PickupEvent
    {
        public EcsEntity Entity;
        public Transform Transform;
    }
}