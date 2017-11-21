namespace GameCore.Render
{
    public class Animation
    {
        private int currentIndex;
        private float time;
        
        public string PathMask { get; set; }
        public int ImageCount { get; set; }
        public float Speed { get; set; }

        public void OnTick(float dt, IRenderPrimitive sourcePrimitive)
        {
            time += dt;

            if (time >= Speed)
            {
                SetImageByIndex((currentIndex + 1) % ImageCount, sourcePrimitive);
                time -= Speed;
            }
        }

        private void SetImageByIndex(int index, IRenderPrimitive sourcePrimitive)
        {
            sourcePrimitive.ImageName = string.Format(PathMask, index + 1);
            currentIndex = index;
        }
    }
}