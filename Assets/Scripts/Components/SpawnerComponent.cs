using UnityEngine;
using System.Collections.Generic;

namespace Yohoho.Scripts.Components
{
    public struct SpawnerComponent
    {
        public float Cooldown;
        public float Timer;
        public Transform Transform;
        public GameObject Prefab;
        public List<Transform> SpawnPositions;
    }
}