using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class PlaceTagsSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<NumberComponent, PositionComponent> _filter;
        private ITagViewService _tagViewService;
        
        public void Init()
        {
            foreach (var entityIndex in _filter)
            {
                var numberComponent = _filter.Get1(entityIndex);
                
                var tagView = _tagViewService.GetTagView(numberComponent.Value);

                var positionComponent = _filter.Get2(entityIndex);
                tagView.SetupTag(numberComponent.Value, positionComponent.X, positionComponent.Y);
            }
        }
    }
}