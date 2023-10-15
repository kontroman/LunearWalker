using UnityEngine;

namespace Yohoho.Scripts.Components
{
    public struct MoveComponent
    {
        public float Speed;
        public float RotationSpeed;
        public Vector3 Direction;
        public Transform Transform;
        public CharacterController CharacterController;
    }
}