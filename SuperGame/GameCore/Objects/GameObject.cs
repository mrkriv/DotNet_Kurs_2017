using GameCore.Managers;

namespace GameCore.Objects
{
    public abstract class GameObject
    {
        public World World { get; set; }
        public string Name { get; set; }

        public virtual void OnTick(float dt)
        {
        }

        public virtual void OnAttachToWorld()
        {
            
        }
    }
}