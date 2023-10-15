using Leopotam.Ecs;
using UnityEngine;
using Yohoho.Scripts.Core;
using Yohoho.Scripts.Components;
using Yohoho.Scripts.Monbeh;

namespace Yohoho.Scripts.Systems
{
    public class SpawnerSystem : IEcsRunSystem
    {
        private EcsWorld _ecsWorld;
        private EcsFilter<SpawnerComponent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var spawnerComponent = ref _filter.Get1(i);

                if (spawnerComponent.Timer < Time.time)
                {
                    Transform freeSpawnpoint = FindFreeSpawnpoint(spawnerComponent);
                    if (freeSpawnpoint != null)
                    {
                        CreateItem(freeSpawnpoint, spawnerComponent);

                        spawnerComponent.Timer = (Time.time + spawnerComponent.Cooldown);
                    }
                }
            }
        }

        private Transform FindFreeSpawnpoint(SpawnerComponent inputSpawner)
        {
            Transform spawnpointTransform = inputSpawner.SpawnPositions.Find(x => x.childCount == 0);

            if (!spawnpointTransform)
                return null;
            else
                return spawnpointTransform;
        }

        private void CreateItem(Transform spawnpointTransform, SpawnerComponent inputSpawner)
        {
            var newItem = Object.Instantiate(inputSpawner.Prefab, spawnpointTransform.position, Quaternion.identity);
            newItem.transform.SetParent(inputSpawner.SpawnPositions.Find(x => x.childCount == 0));

            var newItemEntity = _ecsWorld.NewEntity();
            ref var itemComponent = ref newItemEntity.Get<ItemComponent>();
            itemComponent.Transform = newItem.transform;
            //itemComponent.ModelTransform = itemComponent.Transform.GetChild(0);

            newItem.GetComponent<Item>().Entity = newItemEntity;
        }
    }
}