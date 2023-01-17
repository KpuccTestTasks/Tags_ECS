using System.Linq;
using System.Collections.Generic;
using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class ShuffleTagsSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<NumberComponent> _numbersFilter;
    
        public void Init()
        {
            List<int> values = new List<int>();

            for (int i = 0; i < 16 - 1; i++) // TODO: размер поля в конфиг
            {
                values.Add(i);
            }

            // TODO: валидировать не сгенерировалась ли сразу правильная раскладка пятнашек
            var random = new System.Random();
            var shuffled = values.OrderBy(_ => random.Next()).ToList(); 
            
            foreach (var entityIndex in _numbersFilter)
            {
                var entity = _numbersFilter.GetEntity(entityIndex);

                PositionComponent positionComponent = new PositionComponent();
                positionComponent.X = shuffled[entityIndex] / 4; // TODO: размер поля в конфиг
                positionComponent.Y = shuffled[entityIndex] % 4; // TODO: размер поля в конфиг

                entity.Replace(positionComponent);
            }
        }
    }
}