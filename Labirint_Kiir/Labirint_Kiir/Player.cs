using System;
using OpenTK;
using System.Drawing;
using OpenTK.Input;

namespace Labirint_Kiir
{
    class Player
    {
        public Vector2 position, velocity;
        private Vector2 size=new Vector2(64,64);

        private Texture2D playerSprite;
        private bool climbing, facingRight, onLadder, grounded;

        public RectangleF ColRec
        {
            get { return new RectangleF(position.X - size.X / 2f, position.Y - size.Y / 2f, size.X, size.Y); }
        }
        public RectangleF DrawRec
        {
            get
            {
                RectangleF colRec = ColRec;
                colRec.X = colRec.X - 5;
                colRec.Width = colRec.Width + 10;
                return colRec;

            }
        }
        public Player(Vector2 startPos)
        {
            this.position = startPos;
            this.velocity = Vector2.Zero;
            climbing = false;
            facingRight = false;
            onLadder = false;
            grounded = false;
            //this.size = new Vector2(100, 45);
            //this.playerSprite = ContentPipe.LoadTexture("spriteDeejay.PNG");
            this.velocity.X = 1f;
        }
        public void Update(Level level)
        {
            HandleInput();

            this.velocity += new Vector2(0, 0.5f);
            this.position += velocity;
            ResolveCollision(level);
        }
        public void HandleInput()
        {

        }
        public void ResolveCollision(Level level)
        {
            int minX = (int)Math.Floor((this.position.X - size.X / 2f) / Game.GRINDSIZE);
            int minY = (int)Math.Floor((this.position.Y - size.Y / 2f) / Game.GRINDSIZE);
            int maxX = (int)Math.Floor((this.position.X + size.X / 2f) / Game.GRINDSIZE);
            int maxY = (int)Math.Floor((this.position.Y + size.Y / 2f) / Game.GRINDSIZE);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    RectangleF blockRec = new RectangleF(x * Game.GRINDSIZE, y * Game.GRINDSIZE, Game.GRINDSIZE, Game.GRINDSIZE);
                    if (level[x, y].IsSolid && this.ColRec.IntersectsWith(blockRec))
                    {
                        #region Resolve Collision
                        float[] depths = new float[4]
                        {
                            blockRec.Right-ColRec.Left,//posX
                            blockRec.Bottom-ColRec.Top,//posY
                            ColRec.Right-blockRec.Left,//negX
                            ColRec.Bottom-blockRec.Top//negY
                        };
                        Point[] directions = new Point[4]
                        {
                            new Point(1,0),
                            new Point(0,1),
                            new Point(-1,0),
                            new Point(0,-1)
                        };

                        float min = float.MaxValue;
                        Vector2 minDirection = Vector2.Zero;
                        for (int i = 0; i < 4; i++)
                        {
                            if (!level[x + directions[i].X, y + directions[i].Y].IsSolid && depths[i] < min)
                            {
                                min = depths[i];
                                minDirection = new Vector2(directions[i].X, directions[i].Y);
                            }
                        }
                        if (min == float.MaxValue)
                        {
                            continue;
                        }
                        this.position += minDirection * min;

                        if (this.velocity.X * minDirection.X < 0)
                        {
                            this.velocity.X = 0;
                        }
                        if (this.velocity.Y * minDirection.Y < 0)
                        {
                            this.velocity.Y = 0;
                        }
                        #endregion
                    }
                }
            }

        }
        //public void Draw()
        //{
        //    if (climbing)
        //    {
        //        SpriteBatch.Draw(playerSprite, this.position,
        //            new Vector2(DrawRec.Width / playerSprite.Width, DrawRec.Height / playerSprite.Height),
        //            Color.White, new Vector2(playerSprite.Width / 4f, playerSprite.Height / 2f),
        //            new RectangleF(playerSprite.Width / 2f, 0, playerSprite.Width / 2f, playerSprite.Height));
        //    }
        //    else
        //    {
        //        SpriteBatch.Draw(playerSprite,this.position,new Vector2(1,1),Color.White,
        //            new Vector2(playerSprite.Width / 4f, playerSprite.Height / 2f),
        //            new RectangleF(0, 0, playerSprite.Width / 2f, playerSprite.Height));
        //        //SpriteBatch.Draw(playerSprite, this.position,
                //    new Vector2(DrawRec.Width / playerSprite.Width, DrawRec.Height / playerSprite.Height),
                //    Color.White, new Vector2(playerSprite.Width / 4f, playerSprite.Height / 2f),
                //    new RectangleF(0, 0, playerSprite.Width / 2f, playerSprite.Height));
        //    }
        //}
    }
}
