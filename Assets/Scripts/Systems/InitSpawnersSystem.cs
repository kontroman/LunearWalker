using System;
using Leopotam.Ecs;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Core;
using Yohoho.Scripts.Monbeh;

namespace Yohoho.Scripts.Systems
{
    public class InitSpawnersSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private SceneData _sceneData;

        public void Init()
        {
            foreach (Spawner spawnerView in GameObject.FindObjectsOfType<Spawner>())
            {
                EcsEntity spawnerEntity = _ecsWorld.NewEntity();
                ref var spawnerComponent = ref spawnerEntity.Get<SpawnerComponent>();

                spawnerView.Entity = spawnerEntity;

                spawnerComponent.Cooldown = _sceneData.SpawnTime;
                spawnerComponent.Transform = spawnerView.transform;
                spawnerComponent.Prefab = _sceneData.Item;

                spawnerComponent.SpawnPositions = new List<Transform>();
                foreach (Transform spawnPoint in spawnerComponent.Transform)
                    spawnerComponent.SpawnPositions.Add(spawnPoint);
            }
        }
    }
}