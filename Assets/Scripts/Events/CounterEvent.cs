using Leopotam.Ecs;

namespace Yohoho.Scripts.Events
{
    public struct CounterEvent
    {
        public int Amount;
        public EcsEntity StackEntity;
        public EcsEntity CounterEntity;
    }
}
