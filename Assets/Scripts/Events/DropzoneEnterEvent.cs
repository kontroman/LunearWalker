using Leopotam.Ecs;

namespace Yohoho.Scripts.Events
{
    public struct DropzoneEnterEvent
    {
        public EcsEntity Owner;
        public EcsEntity Dropzone;
    }
}