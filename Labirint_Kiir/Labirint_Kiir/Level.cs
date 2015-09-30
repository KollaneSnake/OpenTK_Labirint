using System;
using OpenTK;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Labirint_Kiir
{
    struct Level
    {
        private Block[,] grid;
        private string filename;
        public Point playerStartPos;

        public Block this[int x, int y]
        {
            get
            {
                return grid[x, y];
            }
            set
            {
                grid[x, y] = value;
            }
        }
        public string FileName
        {
            get { return filename; }
        }
        public int Width { get { return grid.GetLength(0); } }
        public int Height { get { return grid.GetLength(1); } }
        public Level(int width, int height)
        {
            grid = new Block[width, height];
            filename = "none";
            playerStartPos = new Point(1, 1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        grid[x, y] = new Block(BlockType.Solid, x, y);
                    }
                    else
                    {
                        grid[x, y] = new Block(BlockType.Empty, x, y);
                    }
                }
            }
        }
        public Level(string filePath)
        {
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(stream);
                    int width = int.Parse(doc.DocumentElement.GetAttribute("width"));
                    int height = int.Parse(doc.DocumentElement.GetAttribute("height"));

                    grid = new Block[width, height];
                    this.filename = filePath;
                    playerStartPos = new Point(1, 1);

                    XmlNode tileLayer = doc.DocumentElement.SelectSingleNode("layer[@name='Tile Layer 1']");
                    XmlNodeList tiles = tileLayer.SelectSingleNode("data").SelectNodes("tile");

                    int x = 0, y = 0;
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        int gid = int.Parse(tiles[i].Attributes["gid"].Value);
                        switch (gid)
                        {
                            case 11:
                                grid[x, y] = new Block(BlockType.Solid, x, y);
                                break;
                            case 21:
                                grid[x, y] = new Block(BlockType.Ladder, x, y);
                                break;
                            case 31:
                                grid[x, y] = new Block(BlockType.LadderPlatform, x, y);
                                break;
                            case 41:
                                grid[x, y] = new Block(BlockType.Platform, x, y);
                                break;
                            case 1:
                                grid[x, y] = new Block(BlockType.Empty, x, y);
                                break;
                            default:
                                grid[x, y] = new Block(BlockType.Empty, x, y);
                                break;
                        }

                        x++;
                        if (x >= width)
                        {
                            x = 0;
                            y++;
                        }
                    }

                    //XmlNode objcetGroup = doc.DocumentElement.SelectSingleNode("objectgroup[@name='Object Layer 1']");
                    //XmlNodeList objects = objcetGroup.SelectNodes("object");

                    //for (int i = 0; i < objects.Count; i++)
                    //{
                    //    int xPos = int.Parse(objects[i].Attributes["x"].Value);
                    //    int yPos = int.Parse(objects[i].Attributes["y"].Value);

                    //    switch (objects[i].Attributes["name"].Value)
                    //    {
                    //        case "PlayerStartPos":
                    //            this.playerStartPos = new Point((int)(xPos / 128), (int)(yPos / 128));
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("xml problem in Level class {0},   {1}", filePath, e.Message);

                int height = 20;
                int width = 20;

                grid = new Block[width, height];
                filename = "none";
                playerStartPos = new Point(1, 1);

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                        {
                            grid[x, y] = new Block(BlockType.Solid, x, y);
                        }
                        else
                        {
                            grid[x, y] = new Block(BlockType.Empty, x, y);
                        }
                    }
                }
            }
        }
    }
    public enum BlockType
    {
        Solid,
        Empty,
        Platform,
        Ladder,
        LadderPlatform
    }
    struct Block
    {
        private BlockType type;
        private int posX, posY;
        private bool solid, platform, ladder;

        public BlockType Type { get { return type; } }
        public int X { get { return posX; } }
        public int Y { get { return posY; } }
        public bool IsSolid { get { return solid; } }
        public bool IsPlatform { get { return platform; } }
        public bool IsLadder { get { return ladder; } }

        public Block(BlockType type, int x, int y)
        {
            this.posX = x;
            this.posY = y;
            this.type = type;

            this.ladder = false;
            this.platform = false;
            this.solid = false;

            switch (type)
            {
                case BlockType.Solid:
                    solid = true;
                    break;
                case BlockType.Platform:
                    platform = true;
                    break;
                case BlockType.Ladder:
                    ladder = true;
                    break;
                case BlockType.LadderPlatform:
                    ladder = true;
                    platform = true;
                    break;
                default:

                    break;
            }
        }
    }
}
