using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Media;

namespace Labirint_Kiir
{
    class Game : GameWindow
    {
        public static int GRINDSIZE = 28, TILESIZE = 64;

        Texture2D texture, tileset;
        AnimatedSprite hero;
        AnimatedSprite runnerLeft, runnerRight, runnerClimb;
        View view;
        Level level;
        //Player player;
        int looper;
        GameState gameState;
        RunnerState runnerState;
        string melodyMenu, melodyGame;
        SoundPlayer simpleSound;
        int runnerPosX, runnerPosY;

        public enum GameState 
        {
            Menu,
            Game,
            Pair,
            Move,
            Puzzle
        }
        public enum RunnerState
        {
            Left,
            Right,
            Climb
        }

        public Game(int width, int height)
            : base(width, height)
        {
            Input.Initialize(this);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //texture = ContentPipe.LoadTexture("texture.png");
           

            GL.Enable(EnableCap.Texture2D);
            tileset = ContentPipe.LoadTexture("TileSet1.png");
            level = new Level("Content/labirint.tmx");
            texture = ContentPipe.LoadTexture("Waiting.png");
            hero = new AnimatedSprite(texture,1,5,4);
            texture = ContentPipe.LoadTexture("runnerLeft.png");
            runnerLeft = new AnimatedSprite(texture, 1, 4, 4);
            texture = ContentPipe.LoadTexture("runnerRight.png");
            runnerRight = new AnimatedSprite(texture, 1, 4, 4);
            texture = ContentPipe.LoadTexture("runnerClimb.png");
            runnerClimb = new AnimatedSprite(texture, 1, 2, 2);


            gameState=GameState.Menu;
            runnerState = RunnerState.Right;
            runnerPosX=50;
            runnerPosY=377;

            melodyMenu =  "Content/AKINALO.WAV";
            melodyGame = "Content/MS3ALL_F_00010.wav";

            simpleSound = new SoundPlayer(melodyMenu);
            simpleSound.PlayLooping();

            view = new View(Vector2.Zero, 2.0, 0);
            view.SetPosition(new Vector2(195,145));
            //player = new Player(new Vector2(200,150));

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (Input.KeyPress(OpenTK.Input.Key.Enter))
            {
                if (gameState==GameState.Menu)
                {
                    gameState = GameState.Game;
                    simpleSound = new SoundPlayer(melodyGame);
                    simpleSound.PlayLooping();
                }
                if (gameState==GameState.Game)
                {
                    
                }
            }
            if (Input.KeyPress(OpenTK.Input.Key.Left) && gameState==GameState.Game)
            {
                
                runnerState = RunnerState.Left;
                runnerLeft.Update();
                runnerPosX -= 1;
                if (Collusion.gravity(runnerPosX, runnerPosY, level))
                {
                    runnerPosY += 1;
                }
            }
            if (Input.KeyDown(OpenTK.Input.Key.Left) && gameState == GameState.Game)
            {

                runnerState = RunnerState.Left;
                runnerLeft.Update();
                runnerPosX -= 1;
                if (Collusion.gravity(runnerPosX, runnerPosY, level))
                {
                    runnerPosY += 1;
                }
            }
            if (Input.KeyPress(OpenTK.Input.Key.Right) && gameState == GameState.Game)
            {
                runnerState = RunnerState.Right;
                runnerRight.Update();
                runnerPosX += 1;
                if (Collusion.gravity(runnerPosX, runnerPosY, level))
                {
                    runnerPosY += 1;
                }
            }
            if (Input.KeyDown(OpenTK.Input.Key.Right) && gameState == GameState.Game)
            {
                runnerState = RunnerState.Right;
                runnerRight.Update();
                runnerPosX += 1;
                if (Collusion.gravity(runnerPosX, runnerPosY, level))
                {
                    runnerPosY += 1;
                }
            }
            if (Input.KeyPress(OpenTK.Input.Key.Up) && gameState == GameState.Game && Collusion.collide(runnerPosX, runnerPosY, level))
                {
                    runnerState = RunnerState.Climb;
                    runnerClimb.Update();
                    runnerPosY -= 1;
                }
            
                if (Input.KeyDown(OpenTK.Input.Key.Up) && gameState == GameState.Game && Collusion.collide(runnerPosX, runnerPosY, level))
                {
                    runnerState = RunnerState.Climb;
                    runnerClimb.Update();
                    runnerPosY -= 1;
                }            
                if (Input.KeyPress(OpenTK.Input.Key.Down) && gameState == GameState.Game && Collusion.collide(runnerPosX, runnerPosY, level))
                {
                    runnerState = RunnerState.Climb;
                    runnerClimb.Update();
                    runnerPosY += 1;
                }
                if (Input.KeyDown(OpenTK.Input.Key.Down) && gameState == GameState.Game && Collusion.collide(runnerPosX, runnerPosY, level))
                {
                    runnerState = RunnerState.Climb;
                    runnerClimb.Update();
                    runnerPosY += 1;
                }
            //if (Input.MousePress(OpenTK.Input.MouseButton.Left))
            //{
            //    Vector2 pos = new Vector2(Mouse.X, Mouse.Y) - new Vector2(this.Width, this.Height) / 2f;
            //    pos = view.ToWorld(pos);
            //    view.SetPosition(pos, TweentType.QuadraticInOut, 15);
            //}
            //if (Input.KeyDown(OpenTK.Input.Key.Right))
            //{
            //    view.SetPosition(view.PositionGoTo + new Vector2(5, 0), TweentType.QuadraticInOut, 15);
            //}
            //if (Input.KeyDown(OpenTK.Input.Key.Left))
            //{
            //    view.SetPosition(view.PositionGoTo + new Vector2(-5, 0), TweentType.QuadraticInOut, 15);
            //}
            //if (Input.KeyDown(OpenTK.Input.Key.Up))
            //{
            //    view.SetPosition(view.PositionGoTo + new Vector2(0, -5), TweentType.QuadraticInOut, 15);
            //}
            //if (Input.KeyDown(OpenTK.Input.Key.Down))
            //{
            //    view.SetPosition(view.PositionGoTo + new Vector2(0, 5), TweentType.QuadraticInOut, 15);
            //}

            //player.Update(level);
            //view.SetPosition(player.position, TweentType.QuadraticInOut, 160);
            //view.SetPosition(new Vector2(1,1));
            if(looper==15)
            {
                hero.Update();
                looper = 0;
            }
            else
            {
                looper++;
            }
            view.Update();
            Input.Update();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            #region GameState::MENU
            if (gameState == GameState.Menu)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color.CornflowerBlue);

                SpriteBatch.Begin(this.Width, this.Height);


                view.AllyTransform();
                hero.Draw(new Vector2(50, 99));
                //player.Draw();

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        RectangleF source = new RectangleF(0, 0, 0, 0);
                        switch (level[x, y].Type)
                        {
                            case BlockType.Empty:
                                source = new RectangleF(0 * TILESIZE, 0 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Solid:
                                source = new RectangleF(0 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Ladder:
                                source = new RectangleF(0 * TILESIZE, 2 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.LadderPlatform:
                                source = new RectangleF(0 * TILESIZE, 3 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                            case BlockType.Platform:
                                source = new RectangleF(0 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                        }
                        SpriteBatch.Draw(tileset, new Vector2(x * GRINDSIZE, y * GRINDSIZE), new Vector2((float)GRINDSIZE / TILESIZE), Color.White, Vector2.Zero, source);
                    }
                }

                this.SwapBuffers();
            }
            #endregion GameState::MENU
            #region GameState::GAME
            if (gameState == GameState.Game)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color.CornflowerBlue);

                SpriteBatch.Begin(this.Width, this.Height);


                view.AllyTransform();
                
                //player.Draw();

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        RectangleF source = new RectangleF(0, 0, 0, 0);
                        switch (level[x, y].Type)
                        {
                            case BlockType.Empty:
                                source = new RectangleF(0 * TILESIZE, 0 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Solid:
                                source = new RectangleF(0 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Ladder:
                                source = new RectangleF(0 * TILESIZE, 2 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.LadderPlatform:
                                source = new RectangleF(0 * TILESIZE, 3 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                            case BlockType.Platform:
                                source = new RectangleF(0 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                        }
                        SpriteBatch.Draw(tileset, new Vector2(x * GRINDSIZE, y * GRINDSIZE), new Vector2((float)GRINDSIZE / TILESIZE), Color.White, Vector2.Zero, source);
                    }
                }

                if (runnerState == RunnerState.Left)
                {
                    runnerLeft.Draw(new Vector2(runnerPosX, runnerPosY));
                }
                else if (runnerState == RunnerState.Right)
                {
                    runnerRight.Draw(new Vector2(runnerPosX, runnerPosY));

                }
                else if (runnerState == RunnerState.Climb)
                {
                    runnerClimb.Draw(new Vector2(runnerPosX, runnerPosY));
                }
                this.SwapBuffers();
            }
            #endregion GameState::GAME
            #region GameState::PAIR
            if (gameState == GameState.Pair)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color.CornflowerBlue);

                SpriteBatch.Begin(this.Width, this.Height);


                view.AllyTransform();
                hero.Draw(new Vector2(50, 99));
                //player.Draw();

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        RectangleF source = new RectangleF(0, 0, 0, 0);
                        switch (level[x, y].Type)
                        {
                            case BlockType.Empty:
                                source = new RectangleF(0 * TILESIZE, 0 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Solid:
                                source = new RectangleF(0 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Ladder:
                                source = new RectangleF(0 * TILESIZE, 2 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.LadderPlatform:
                                source = new RectangleF(0 * TILESIZE, 3 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                            case BlockType.Platform:
                                source = new RectangleF(0 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                        }
                        SpriteBatch.Draw(tileset, new Vector2(x * GRINDSIZE, y * GRINDSIZE), new Vector2((float)GRINDSIZE / TILESIZE), Color.White, Vector2.Zero, source);
                    }
                }

                this.SwapBuffers();
            }
            #endregion GameState::PAIR
            #region GameState::MOVE
            if (gameState == GameState.Move)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color.CornflowerBlue);

                SpriteBatch.Begin(this.Width, this.Height);


                view.AllyTransform();
                hero.Draw(new Vector2(50, 99));
                //player.Draw();

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        RectangleF source = new RectangleF(0, 0, 0, 0);
                        switch (level[x, y].Type)
                        {
                            case BlockType.Empty:
                                source = new RectangleF(0 * TILESIZE, 0 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Solid:
                                source = new RectangleF(0 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Ladder:
                                source = new RectangleF(0 * TILESIZE, 2 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.LadderPlatform:
                                source = new RectangleF(0 * TILESIZE, 3 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                            case BlockType.Platform:
                                source = new RectangleF(0 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                        }
                        SpriteBatch.Draw(tileset, new Vector2(x * GRINDSIZE, y * GRINDSIZE), new Vector2((float)GRINDSIZE / TILESIZE), Color.White, Vector2.Zero, source);
                    }
                }

                this.SwapBuffers();
            }
            #endregion GameState::MOVE
            #region GameState::PUZZLE
            if (gameState == GameState.Puzzle)
            {
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.ClearColor(Color.CornflowerBlue);

                SpriteBatch.Begin(this.Width, this.Height);


                view.AllyTransform();
                hero.Draw(new Vector2(50, 99));
                //player.Draw();

                for (int x = 0; x < level.Width; x++)
                {
                    for (int y = 0; y < level.Height; y++)
                    {
                        RectangleF source = new RectangleF(0, 0, 0, 0);
                        switch (level[x, y].Type)
                        {
                            case BlockType.Empty:
                                source = new RectangleF(0 * TILESIZE, 0 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Solid:
                                source = new RectangleF(0 * TILESIZE, 1 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.Ladder:
                                source = new RectangleF(0 * TILESIZE, 2 * TILESIZE, TILESIZE, TILESIZE);
                                break;
                            case BlockType.LadderPlatform:
                                source = new RectangleF(0 * TILESIZE, 3 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                            case BlockType.Platform:
                                source = new RectangleF(0 * TILESIZE, 4 * TILESIZE, TILESIZE, TILESIZE);
                                break;

                        }
                        SpriteBatch.Draw(tileset, new Vector2(x * GRINDSIZE, y * GRINDSIZE), new Vector2((float)GRINDSIZE / TILESIZE), Color.White, Vector2.Zero, source);
                    }
                }

                this.SwapBuffers();
            }
            #endregion GameState::PUZZLE
        }
    }
}
