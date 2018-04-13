using System;
using ProjectWiz.GameEngine.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ProjectWiz.GameEngine.GameObjects
{
    public abstract class _GameObject : ILoadable, ITransform
    {
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }
    }
}
