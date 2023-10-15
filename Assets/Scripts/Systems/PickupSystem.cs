using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Events;

namespace Yohoho.Scripts.Systems
{
    public class PickupSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<PickupEvent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var pickupEvent = ref _filter.Get1(i);
                ref var stackComponent = ref pickupEvent.Entity.Get<StackComponent>();

                if (stackComponent.ItemsStack.Count < stackComponent.Capacity)
                {
                    Vector3 newPosition = new Vector3(0f, (0.25f * stackComponent.ItemsStack.Count), 0f);
                    Transform itemTransform = pickupEvent.Transform;

                    AudioClip clip = itemTransform.gameObject.GetComponent<AudioSource>().clip;
                    itemTransform.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);

                    itemTransform.SetParent(stackComponent.AttachmentTransform);
                    itemTransform.localPosition = newPosition;
                    itemTransform.localRotation = Quaternion.Euler(0, 90, 0);
                    itemTransform.GetComponent<Collider>().enabled = false;

                    stackComponent.ItemsStack.Push(pickupEvent.Transform);

                    var newEvent = _ecsWorld.NewEntity();
                    ref var counterEvent = ref newEvent.Get<CounterEvent>();
                    counterEvent.Amount = stackComponent.ItemsStack.Count;
                    counterEvent.StackEntity = pickupEvent.Entity;
                    counterEvent.CounterEntity = pickupEvent.Entity.Get<StackComponent>().CounterEntity;
                }

                ref var pickupEntity = ref _filter.GetEntity(i);
                    pickupEntity.Destroy();
            }
        }
    }
}