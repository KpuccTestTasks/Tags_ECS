using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class InputSystem : IEcsRunSystem
    {
        private IInputService _inputService;
        private EcsFilter<NumberComponent> _filter;
        
        public void Run()
        {
            int clickedTag = _inputService.GetSelectedTag();

            if (clickedTag != -1)
            {
                foreach (var entityIndex in _filter)
                {
                    if (_filter.Get1(entityIndex).Value == clickedTag)
                    {
                        var entity = _filter.GetEntity(clickedTag);
                        entity.Replace(new MovedComponent());
                        break;
                    }
                }
            }
        }
    }
}