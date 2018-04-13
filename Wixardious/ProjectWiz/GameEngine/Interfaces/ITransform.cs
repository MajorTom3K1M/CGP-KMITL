using System;
using Microsoft.Xna.Framework;

namespace ProjectWiz.GameEngine.Interfaces
{
    public interface ITransform
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Scale { get; set; }
    }
}
