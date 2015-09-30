using System;
using System.Drawing;
using OpenTK;

namespace Labirint_Kiir
{
    class AnimatedSprite
    {
        private Texture2D Texture;
        private int Rows, Columns, currentFrame, totalFrames;

        public AnimatedSprite(Texture2D texture,int rows, int columns, int count)
        {
            this.Texture = texture;
            this.Rows = rows;
            this.Columns = columns;
            currentFrame = 0;
            totalFrames = count;
        }
        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }
        public void Draw(Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            RectangleF sourceRectangle = new RectangleF(width*column,height*row,width,height);
            //Vector2 destinationRectangle = location;//new Rectangle((int)location.X,(int)location.Y,width,height);

            SpriteBatch.Draw(Texture,location,new Vector2(0.4f,0.4f),Color.White,location,sourceRectangle);

        }
    }
}
