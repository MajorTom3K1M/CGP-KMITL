using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace ProjectWizardous
{
    public class InputComponent
    {
        public Dictionary<string, Keys> InputList;

        public InputComponent(Game currentScene)
        {
            InputList = new Dictionary<string, Keys>();
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) { }

        public virtual void Reset() { }

        public virtual void ChangeMappingKey(string Key, Keys newInput)
        {
            InputList[Key] = newInput;
        }

        public virtual void ReceiveMessage(int message) { }
    }
}
