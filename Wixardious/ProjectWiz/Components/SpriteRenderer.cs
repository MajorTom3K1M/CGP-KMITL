using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectWiz.Modules
{
    public class SpriteRenderer : GameObject
    {
        public Texture2D _texture;

        SpriteRenderer(Texture2D texture){
            _texture = texture;
        }
    }
}
