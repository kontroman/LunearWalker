using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Core;

namespace Yohoho.Scripts.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private EcsFilter<MoveComponent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);

                Vector3 direction = Converter(moveComponent.Direction * (moveComponent.Speed * Time.deltaTime));
                moveComponent.CharacterController.Move(direction);

                Vector3 newDirection = Vector3.RotateTowards(moveComponent.Transform.forward, direction, (moveComponent.RotationSpeed * Time.deltaTime), 0f);
                Quaternion targetRotation = Quaternion.LookRotation(newDirection);

                targetRotation.x = 0;
                targetRotation.z = 0;

                moveComponent.Transform.rotation = targetRotation;
            }
        }

        private Vector3 Converter(Vector3 inputVector)
        {
            Quaternion rotation = Quaternion.Euler(0, _sceneData.Camera.transform.localEulerAngles.y, 0);
            Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
            Vector3 result = isoMatrix.MultiplyPoint3x4(inputVector);

            return result;
        }
    }
}