using System;
using System.Numerics;
using System.Threading;
using GameCore.Managers;
using GameCore.Objects;

namespace GameCore.Components
{
    public class AI
    {
        private readonly Character _character;
        private AIState state;
        private float time;

        public enum AIState
        {
            Default,
            Wait
        }

        public AI(Character character)
        {
            _character = character;
            state = AIState.Default;
        }

        public void OnTick(float dt)
        {
           // if (_character.World)
            
            if (state == AIState.Default)
            {
                time += dt;

                var dir = new Vector2((float) Math.Sin(time), (float) Math.Cos(time));
                _character.Velocity = dir * _character.Speed;
            }
        }
    }
}