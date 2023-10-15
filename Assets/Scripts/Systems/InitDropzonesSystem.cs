using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Core;
using Yohoho.Scripts.Monbeh;

namespace Yohoho.Scripts.Systems
{
    public class InitDropzonesSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;

        public void Init()
        {
            foreach (Dropzone dropzone in GameObject.FindObjectsOfType<Dropzone>())
            {
                EcsEntity dropzoneEntity = _ecsWorld.NewEntity();
                ref var dropzoneComponent = ref dropzoneEntity.Get<DropzoneComponent>();

                dropzone.Entity = dropzoneEntity;

                dropzoneComponent.Transform = dropzone.transform;
                dropzoneComponent.DroppointTransform = dropzoneComponent.Transform.Find("DropzonePoint");
            }
        }
    }
}