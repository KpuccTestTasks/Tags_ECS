using System;
using System.Collections.Generic;
using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class MoveTagSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private ITagViewService _tagViewService;
        private EcsFilter<MovedComponent, NumberComponent, PositionComponent> _movedFilter;
        private EcsFilter<PositionComponent>.Exclude<NumberComponent> _emptyPositionsFilter;
        
        public void Run()
        {
            foreach (var entityIndex in _movedFilter)
            {
                var movedEntityPositionComponent = _movedFilter.Get3(entityIndex);

                foreach (int emptyPositionEntity in _emptyPositionsFilter)
                {
                    var emptyPositionComponent = _emptyPositionsFilter.Get1(emptyPositionEntity);

                    int diffX = movedEntityPositionComponent.X - emptyPositionComponent.X;
                    int diffY = movedEntityPositionComponent.Y - emptyPositionComponent.Y;

                    if ((Math.Abs(diffX) + Math.Abs(diffY)) != 1)
                        continue;
                    
                    var tag = _tagViewService.GetTagView(_movedFilter.Get2(entityIndex).Value);
                    tag.UpdatePosition(emptyPositionComponent.X, emptyPositionComponent.Y, true);
                    
                    var newNumber = new NumberComponent();
                    newNumber.Value = _movedFilter.Get2(entityIndex).Value;
                    
                    _emptyPositionsFilter.GetEntity(emptyPositionEntity).Replace(newNumber);
                    _movedFilter.GetEntity(entityIndex).Del<NumberComponent>();
                }
            }
        }
    }
}