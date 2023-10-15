using Leopotam.Ecs;

namespace Yohoho.Scripts.Events
{
    public struct DropzoneExitEvent
    {
        public EcsEntity Owner;
        public EcsEntity Dropzone;
    }
}