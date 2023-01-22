using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class CheckGameFinishedSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<NumberComponent, PositionComponent> _tagsFilter;
        private EcsFilter<MovedComponent> _moveFilter;
        
        public void Run()
        {
            if (_moveFilter.IsEmpty())
                return;

            bool isAllTagsOnPlace = true;
            
            foreach (var entity in _tagsFilter)
            {
                var positionComponent = _tagsFilter.Get2(entity);
                var numberComponent = _tagsFilter.Get1(entity);

                // TODO: размер поля в конфиг
                var rightTagPositionX = numberComponent.Value % 4;
                var rightTagPositionY = numberComponent.Value / 4;

                if (rightTagPositionX != positionComponent.X || rightTagPositionY != positionComponent.Y)
                {
                    isAllTagsOnPlace = false;
                    break;
                }
            }
            
            if (isAllTagsOnPlace)
                _world.Destroy();
        }
    }
}