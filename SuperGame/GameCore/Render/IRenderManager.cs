using System;

namespace GameCore.Render
{
    public interface IRenderManager
    {
        Camera ActiveCamera { get; set; }

        IRenderPrimitive CreatePrimitive();
        void DestroyPrimitive(IRenderPrimitive primitive);

        void BeginRender();

        void RunInUIThread(Action action);
    }
}