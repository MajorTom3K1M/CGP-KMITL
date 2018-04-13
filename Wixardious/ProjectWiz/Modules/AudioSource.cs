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
    public class AudioSource : GameObject
    {
        public Dictionary<string, SoundEffectInstance> SoundEffects;
    }
}
