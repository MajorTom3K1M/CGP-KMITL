using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class NormalBobble : Bobble
    {

        MouseState mouseState, previousMouseState;
        public float Speed;
        public float Angle;
        public bool isNeverShoot = true;
        public enum BobbleColor { Red, Green, Blue, Yellow }
        public bool isInitialized;

        public NormalBobble(Texture2D texture) : base(texture)
        {
            if (isInitialized) isNeverShoot = false;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            int s = gameObjects.Count;
            GameObject a = gameObjects[s - 2];
            int j = (int)Math.Round(Position.Y / 44) % 2;
            int yGrid = (int) Math.Round(Position.Y / 44) * 44;
            int xGrid = j * (Singleton.BOBBLE_SIZE / 2) + (int) Math.Round(Position.X / 50) * 50;

            foreach (GameObject g in gameObjects)
            {
                if (!g.Equals(this) && g.Name.Equals("NormalBobble") && this.circleCollide(g) && isNeverShoot && g.IsActive)
                {
                    this.Speed = 0;
                    if (j == 1) xGrid = j * (Singleton.BOBBLE_SIZE / 2) + (int)(Math.Floor((Position.X) / 50)) * 50;
                    if (Position.Equals(g.Position)) xGrid -= 50;

                    if ((xGrid - 200) / (Singleton.BOBBLE_SIZE / 2) >= 15)
                    {
                        bool hasLeft, hasDown;
                        hasLeft = hasDown = false;

                        int v = (int) (this.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                        int w = (int) this.Position.Y / 44;

                        foreach(GameObject obj in gameObjects){
                            if (g.Name.Equals("NormalBobble") && g.IsActive)
                            {
                                int x = (int)(g.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                                int y = (int)g.Position.Y / 44;

                                if (v - 2 == x) hasLeft = true;
                                hasDown |= (v - 1 == x && w + 1 == y);
                            }
                        }

                        if(!hasLeft) xGrid = 13 * (Singleton.BOBBLE_SIZE / 2) + 200;
                        else if(!hasDown){
                            xGrid = 14 * (Singleton.BOBBLE_SIZE / 2) + 200;
                            yGrid += 44;
                        } 

                        //Console.WriteLine("Has Left : Has Down >> " + hasLeft + " : " + hasDown);
                    }

                    Position = new Vector2(xGrid, yGrid);
                    isNeverShoot = false;

                    //Console.WriteLine("Shooter >> " + ((xGrid - 200) / (Singleton.BOBBLE_SIZE / 2)) + " " + yGrid / 44);
                }
            }

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            Velocity.X = (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
            Velocity.Y = -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
            Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (Position.X <= 200) Angle = 180 - Angle;
            if (Position.X >= 550) Angle = 180 - Angle;

            if (Position.Y < 0 && isNeverShoot)
            {
                Speed = 0;
                xGrid = (int) Math.Round(Position.X / 50) * 50;
                yGrid = (int) Math.Round(Position.Y / 44);
                Position = new Vector2(j * (Singleton.BOBBLE_SIZE / 2) + xGrid, yGrid * 44);

                isNeverShoot = false;

                //Console.WriteLine("Shooter >> " + xGrid + " " + yGrid);
            }

            //Check if still and not an initialized one
            if (Velocity == Vector2.Zero && !isInitialized && !isNeverShoot)
            {
                destroyCluster(gameObjects, this);
                isInitialized = true;
            }

            base.Update(gameTime, gameObjects);
        }

        private void destroySeparate(List<GameObject> gameObjects){
            foreach (GameObject g in gameObjects)
            {
                if (g.Name == "NormalBobble" && g.IsActive)
                {
                    resetVisited(gameObjects);

                    g.IsWaited |= destroySeperateHandler(gameObjects, g);
                    resetVisited(gameObjects);
                }
            }

            foreach (GameObject g in gameObjects){
                int y = (int) g.Position.Y / 44;
                if (g.Name == "NormalBobble" && g.IsActive && !g.IsWaited && y != 0) g.IsActive = false;
            }

            resetWaited(gameObjects);
        }

        private bool destroySeperateHandler(List<GameObject> gameObjects, GameObject current){
            
            Stack<GameObject> s = new Stack<GameObject>();
            s.Push(current);

            while(s.Count != 0){
                current = s.Pop();

                //Depth-First Search Method (Finding Root)
                int j = (int)(current.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                int i = (int)current.Position.Y / 44;

                foreach (GameObject g in gameObjects)
                {
                    if (g.Name == "NormalBobble" && g.IsActive && !g.IsVisited)
                    {
                        int x = (int)(g.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                        int y = (int)g.Position.Y / 44;

                        bool isChecked = false;

                        if (i - 1 == y)
                        {
                            isChecked |= j - 1 == x;
                            isChecked |= j + 1 == x;
                        }
                        if (i == y)
                        {
                            isChecked |= j - 2 == x;
                            isChecked |= j + 2 == x;
                        }

                        if (isChecked)
                        {
                            s.Push(g);
                            g.IsVisited = true;

                            if (y == 0) return true;
                        }
                    }
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }

        private void resetVisited(List<GameObject> gameObjects){
            foreach (GameObject g in gameObjects)
            {
                if (g.Name == "NormalBobble" && g.IsActive) g.IsVisited = false;
            }
        }

        private void resetWaited(List<GameObject> gameObjects)
        {
            foreach (GameObject g in gameObjects)
            {
                if (g.Name == "NormalBobble" && g.IsActive) g.IsWaited = false;
            }
        }

        private void destroyCluster(List<GameObject> gameObjects, GameObject current)
        {
            resetVisited(gameObjects);

            Console.WriteLine(findCluster(gameObjects, current));
            resetVisited(gameObjects);

            if (findCluster(gameObjects, current) >= 3)
            {
                foreach(GameObject g in gameObjects){
                    if (g.Name == "NormalBobble" && g.IsActive && g.IsVisited) g.IsActive = false;
                }
            }

            resetVisited(gameObjects);

            if(current.Velocity == Vector2.Zero){
                destroySeparate(gameObjects);
            }
        }

        private int findCluster(List<GameObject> gameObjects, GameObject current)
        {
            int count = 0;

            Queue<GameObject> q = new Queue<GameObject>();
            q.Enqueue(current);

            //Breadth-First Search Method
            while(q.Count != 0){
                current = q.Dequeue();

                int j = (int) (current.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                int i = (int) current.Position.Y / 44;

                foreach (GameObject g in gameObjects)
                {
                    if (g.Name == "NormalBobble" && g.IsActive && !g.IsVisited && g.bobbleColor == current.bobbleColor)
                    {
                        bool isChecked = false;

                        int x = (int)(g.Position.X - 200) / (Singleton.BOBBLE_SIZE / 2);
                        int y = (int)g.Position.Y / 44;

                        if (i - 1 == y)
                        {
                            isChecked |= j - 1 == x;
                            isChecked |= j + 1 == x;
                        }
                        if (i == y)
                        {
                            isChecked |= j - 2 == x;
                            isChecked |= j + 2 == x;
                        }
                        if (i + 1 == y)
                        {
                            isChecked |= j - 1 == x;
                            isChecked |= j + 1 == x;
                        }

                        if (isChecked)
                        {
                            Console.WriteLine("Trigger >> " + x + " " + y);
                            q.Enqueue(g);
                            g.IsVisited = true;
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}
