using System.Linq;
using System.Collections.Generic;
using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class ShuffleTagsSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<NumberComponent> _numbersFilter;
    
        public void Init()
        {
            List<int> values = new List<int>();

            for (int i = 0; i < 16; i++) // TODO: размер поля в конфиг
            {
                values.Add(i);
            }
            
            var random = new System.Random();
            var shuffled = values.OrderBy(_ => random.Next()).ToList();

            // shuffled combination must be validated
            // (sum(shuffled[i] < shuffled[j]) + row_with_empty_tag) % 2 == 0
            // shuffled[j] must be after shuffled[i]
            int validationCount = 0;
            int itemToReplace1 = 0;
            int itemToReplace2 = 0;

            for (int i = shuffled.Count - 2; i >= 0; i--)
            {
                var biggerNumberPosition = shuffled[i];

                for (int j = 0; j < i; j++)
                {
                    var smallerNumberPosition = shuffled[j];

                    if (biggerNumberPosition < smallerNumberPosition)
                    {
                        validationCount++;

                        // remember 2 valid items in case of invalid combination
                        if (itemToReplace1 == itemToReplace2)
                        {
                            itemToReplace1 = i;
                            itemToReplace2 = j;
                        }
                    }
                }
            }

            int emptyPosition = shuffled[shuffled.Count - 1] / 4 + 1;

            if ((validationCount + emptyPosition) % 2 == 1 ||
                validationCount == 0)
            {
                // replacing 2 memoried items make combination valid
                (shuffled[itemToReplace1], shuffled[itemToReplace2]) =
                    (shuffled[itemToReplace2], shuffled[itemToReplace1]);
            }

            foreach (var entityIndex in _numbersFilter)
            {
                var entity = _numbersFilter.GetEntity(entityIndex);

                PositionComponent positionComponent = new PositionComponent();
                positionComponent.X = shuffled[entityIndex] % 4; // TODO: размер поля в конфиг
                positionComponent.Y = shuffled[entityIndex] / 4; // TODO: размер поля в конфиг

                entity.Replace(positionComponent);
            }

            // creating empty slot to make moves
            var emptySlotEntity = _world.NewEntity();
            PositionComponent emptyPositionComponent = new PositionComponent();
            emptyPositionComponent.X = shuffled[shuffled.Count - 1] % 4; // TODO: размер поля в конфиг
            emptyPositionComponent.Y = shuffled[shuffled.Count - 1] / 4; // TODO: размер поля в конфиг

            emptySlotEntity.Replace(emptyPositionComponent);
        }
    }
}