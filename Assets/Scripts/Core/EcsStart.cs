using UnityEngine;
using Leopotam.Ecs;
using Yohoho.Scripts.Systems;

namespace Yohoho.Scripts.Core
{
    public class EcsStart : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsSystems _ecsSystems;

        [SerializeField] private UIData _uiData;
        [SerializeField] private SceneData _sceneData;

        private void Awake()
        {
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);
            
            _ecsSystems
                .Add(new InitPlayerSystem())
                .Add(new InitSpawnersSystem())
                .Add(new InitDropzonesSystem())
                .Add(new InputSystem())
                .Add(new PickupSystem())
                .Add(new InterfaceSystem())
                .Add(new SpawnerSystem())
                .Add(new DropzoneSystem())
                .Add(new MovementSystem())
                .Add(new AnimationSystem())

                .Inject(_uiData)
                .Inject(_sceneData);

            _ecsSystems.Init();
        }

        private void Update()
        {
            _ecsSystems?.Run();
        }

        private void Destroy()
        {
            if (_ecsSystems != null)
            {
                _ecsSystems.Destroy();
                _ecsSystems = null;

                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }
    }
}
