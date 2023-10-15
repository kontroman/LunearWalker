using UnityEngine;
using Leopotam.Ecs;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Events;

namespace Yohoho.Scripts.Monbeh
{
    public class DropzoneOnTrigger : MonoBehaviour
    {
        public EcsWorld EcsWorld;
        public EcsEntity OwnerEntity;

        private void OnTriggerExit(Collider other)
        {
            var newEvent = EcsWorld.NewEntity();
            ref var exitEvent = ref newEvent.Get<DropzoneExitEvent>();
            exitEvent.Owner = OwnerEntity;
            exitEvent.Dropzone = other.transform.parent.GetComponent<Dropzone>().Entity;

            exitEvent.Owner.Get<StackComponent>().DropzoneEntity = EcsEntity.Null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Dropzone")) return;

            var newEvent = EcsWorld.NewEntity();
            ref var enterEvent = ref newEvent.Get<DropzoneEnterEvent>();
            enterEvent.Owner = OwnerEntity;
            enterEvent.Dropzone = other.transform.parent.GetComponent<Dropzone>().Entity;

            enterEvent.Owner.Get<StackComponent>().DropzoneEntity = enterEvent.Dropzone;
        }
    }
}