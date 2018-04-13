using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectWiz.Managers;
using ProjectWiz.Models;
using Microsoft.Xna.Framework.Audio;

namespace ProjectWiz.Modules
{
    public class Animator : GameObject
    {
        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        public Animator(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }
    }
}
