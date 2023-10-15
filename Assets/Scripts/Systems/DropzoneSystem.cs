using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Events;
using Yohoho.Scripts.Monbeh;

namespace Yohoho.Scripts.Systems
{
    public class DropzoneSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<StackComponent> _filter;
        private EcsFilter<DropzoneExitEvent> _filterExit;
        private EcsFilter<DropzoneEnterEvent> _filterEnter;

        private float nextStack;

        public void Run()
        {
            ProcessStacks();
            ProcessExits();
            ProcessEnters();
        }

        private void ProcessExits()
        {
            foreach (var i in _filterExit)
            {
                ref var exitEventEntity = ref _filterExit.GetEntity(i);
                exitEventEntity.Destroy();
            }
        }

        private void ProcessEnters()
        {
            foreach (var i in _filterEnter)
            {
                ref var enterEventEntity = ref _filterEnter.GetEntity(i);
                enterEventEntity.Destroy();
            }
        }

        private void ProcessStacks()
        {
            if (nextStack > Time.time) return;

            foreach (var i in _filter)
            {
                ref var stackComponent = ref _filter.Get1(i);
                if ((stackComponent.ItemsStack.Count > 0) && (stackComponent.DropzoneEntity != EcsEntity.Null))
                {
                    Transform droppointTransform = stackComponent.DropzoneEntity.Get<DropzoneComponent>().DroppointTransform;

                    Transform itemTransform = stackComponent.ItemsStack.Pop();
                    itemTransform.GetComponent<BoxCollider>().enabled = false;

                    var newEvent = _ecsWorld.NewEntity();
                    ref var counterEvent = ref newEvent.Get<CounterEvent>();
                    counterEvent.Amount = stackComponent.ItemsStack.Count;
                    counterEvent.StackEntity = _filter.GetEntity(i);
                    counterEvent.CounterEntity = _filter.GetEntity(i).Get<StackComponent>().CounterEntity;

                    itemTransform.SetParent(droppointTransform);

                    itemTransform.GetComponent<Item>().Entity.Destroy();
                    Object.Destroy(itemTransform.gameObject);
                }
            }

            nextStack = (Time.time + 0.15f);
        }
    }
}