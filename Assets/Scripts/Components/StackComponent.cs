using Leopotam.Ecs;
using UnityEngine;
using System.Collections.Generic;

namespace Yohoho.Scripts.Components
{
    public struct StackComponent
    {
        public int Capacity;
        public EcsEntity CounterEntity;
        public EcsEntity DropzoneEntity;
        public Transform Transform;
        public Transform AttachmentTransform;
        public Stack<Transform> ItemsStack;
    }
}