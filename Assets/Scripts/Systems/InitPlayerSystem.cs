using TMPro;
using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Core;
using Yohoho.Scripts.Monbeh;

namespace Yohoho.Scripts.Systems
{
    public class InitPlayerSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        private UIData _uiData;
        private SceneData _sceneData;

        public void Init()
        {
            EcsEntity playerEntity = _ecsWorld.NewEntity();
            ref var moveComponent = ref playerEntity.Get<MoveComponent>();
            ref var inputComponent = ref playerEntity.Get<InputComponent>();
            ref var animatorComponent = ref playerEntity.Get<AnimatorComponent>();

            moveComponent.Transform = _sceneData.Player;
            moveComponent.Speed = 7f;
            moveComponent.RotationSpeed = 5f;
            moveComponent.CharacterController = _sceneData.Player.GetComponent<CharacterController>();

            animatorComponent.Animator = _sceneData.Player.GetComponent<Animator>();

            EcsEntity stackEntity = _ecsWorld.NewEntity();
            ref var stackComponent = ref stackEntity.Get<StackComponent>();

            DropzoneOnTrigger dropzoneTrigger = _sceneData.Player.GetComponentInChildren<DropzoneOnTrigger>();
            dropzoneTrigger.EcsWorld = _ecsWorld;
            dropzoneTrigger.OwnerEntity = stackEntity;

            ItemOnTrigger itemTrigger = _sceneData.Player.GetComponentInChildren<ItemOnTrigger>();
            itemTrigger.EcsWorld = _ecsWorld;
            itemTrigger.TriggerEntity = stackEntity;

            stackComponent.Capacity = 42;
            stackComponent.Transform = itemTrigger.transform;
            stackComponent.ItemsStack = new Stack<Transform>();
            stackComponent.AttachmentTransform = stackComponent.Transform.GetChild(0);

            AddStackCounter(stackEntity);
        }

        private void AddStackCounter(EcsEntity inputStackEntity)
        {
            //// Stack counter
            //CounterView stackCounter = Object.Instantiate(_uiData.CounterPrefab, _uiData.CounterHolder).GetComponent<CounterView>();

            //// Entitize
            //EcsEntity stackCounterEntity = _ecsWorld.NewEntity();
            //ref var stackCounterComponent = ref stackCounterEntity.Get<CounterComponent>();
            //stackCounterComponent.Text = stackCounter.GetComponentInChildren<TMP_Text>();
            //stackCounterComponent.Text.text = string.Format(_uiData.CounterFormat,
            //    inputStackEntity.Get<StackComponent>().ItemsStack.Count,
            //    inputStackEntity.Get<StackComponent>().Capacity);

            //stackCounterComponent.Transform = stackCounter.transform;
            //stackCounterComponent.Attachment = inputStackEntity.Get<StackComponent>().Transform;
            //stackCounterComponent.StackEntity = inputStackEntity;

            //// Attaching
            //stackCounter.Entity = stackCounterEntity;
            //inputStackEntity.Get<StackComponent>().CounterEntity = stackCounterEntity;
        }
    }
}