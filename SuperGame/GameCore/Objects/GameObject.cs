using GameCore.Managers;
using Newtonsoft.Json;

namespace GameCore.Objects
{
    public abstract class GameObject
    {
        [JsonIgnore]
        public World World { get; set; }
        public string Name { get; set; }

        public virtual void OnTick(float dt)
        {
        }

        public virtual void OnAttachToWorld()
        {
            
        }
        public virtual void OnDetach()
        {
            
        }
    }
}