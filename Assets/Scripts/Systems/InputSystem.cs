using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Core;

namespace Yohoho.Scripts.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsFilter<InputComponent, MoveComponent> _filter;
        private UIData _uiData;

        public void Run()
        {
            foreach (var input in _filter)
            {
                ref var inputComponent = ref _filter.Get1(input);
                inputComponent.Input = new Vector3(_uiData.Joystick.Horizontal, 0, _uiData.Joystick.Vertical);

                ref var moveComponent = ref _filter.Get2(input);
                moveComponent.Direction = (inputComponent.Input.normalized);
            }
        }
    }
}
