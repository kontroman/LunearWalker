using UnityEngine;
using Leopotam.Ecs;
using Yohoho.Scripts.Events;

namespace Yohoho.Scripts.Monbeh
{
    public class ItemOnTrigger : MonoBehaviour
    {
        public EcsWorld EcsWorld;
        public EcsEntity TriggerEntity;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("PickUp")) return;

            var newEvent = EcsWorld.NewEntity();
            ref var pickupEvent = ref newEvent.Get<PickupEvent>();
            pickupEvent.Entity = TriggerEntity;
            pickupEvent.Transform = other.transform;
        }
    }
}