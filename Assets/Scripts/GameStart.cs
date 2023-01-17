using ECS.Systems;
using Leopotam.Ecs;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private TagsBoard board;
    [SerializeField] private InputController inputController;
    
    private EcsWorld _world;
    private EcsSystems _systems;
    
    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems
            .Add(new CreateTagsSystem())
            .Add(new ShuffleTagsSystem())
            .Add(new PlaceTagsSystem())
            .Add(new InputSystem())
            .Inject(inputController)
            .Inject(board);

        _systems.Init();
    }

    private void Update()
    {
        _systems.Run();
    }
}