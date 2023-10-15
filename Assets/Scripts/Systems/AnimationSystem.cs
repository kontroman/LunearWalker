using Leopotam.Ecs;
using Yohoho.Scripts.Components;

namespace Yohoho.Scripts.Systems
{
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent, AnimatorComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var animatorComponent = ref _filter.Get2(i);

                animatorComponent.Animator.SetBool("IsMoving", moveComponent.Direction.sqrMagnitude > 0 ? true : false);
            }
        }
    }
}