using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class CreateTagsSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        
        public void Init()
        {
            for (int i = 0; i < 16 - 1; i++) // TODO: размер игрового поля в конфиг
            {
                var newEntity = _world.NewEntity();

                var numberComponent = new NumberComponent();
                numberComponent.Value = i;

                newEntity.Replace(numberComponent);
            }
        }
    }
}