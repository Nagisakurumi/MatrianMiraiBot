using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames
{
    public class GameInfo : IGameInfo
    {
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 列表
        /// </summary>
        public int Col { get; set; }
        /// <summary>
        /// 起始X
        /// </summary>
        public int StartX { get; set; }
        /// <summary>
        /// 起始Y
        /// </summary>
        public int StartY { get; set; }
        /// <summary>
        /// 标题高度
        /// </summary>
        public int TitleHeight { get; set; } = 55;
        /// <summary>
        /// 一个格子的像素
        /// </summary>
        public int Pixel { get; set; } = 20;
        /// <summary>
        /// 字体
        /// </summary>
        public Font TextFont { get; set; } = new Font(FontFamily.GenericMonospace, 10);
        /// <summary>
        /// 玩家数量
        /// </summary>
        public int PlayerCount { get; set; } = 2;
        /// <summary>
        /// 布局图片
        /// </summary>
        public Bitmap LayoutMap { get; set; } = null;
        /// <summary>
        /// 绘图
        /// </summary>
        public Graphics Draw { get; set; } = null;
        /// <summary>
        /// 胜利子数量
        /// </summary>
        public int VectorCount { get; set; } = 5;
        /// <summary>
        /// 画笔
        /// </summary>
        public Brush[] Brushes { get; set; } = new Brush[2] { new SolidBrush(Color.Red), new SolidBrush(Color.Black) };
        /// <summary>
        /// 用于悔棋
        /// </summary>
        public Brush WhiteBrush { get; set; } = new SolidBrush(Color.White);
        /// <summary>
        /// 用于悔棋
        /// </summary>
        public Brush BlackBrush { get; set; } = new SolidBrush(Color.Black);
        /// <summary>
        /// 布局
        /// </summary>
        public int[,] Layout = null;
        /// <summary>
        /// 当前下子的选手
        /// </summary>
        public FiveGamePlayer DoPlayer { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public GameInfo(int row, int col)
        {
            Row = row;
            Col = col;
            InitLayout(Row, Col);
        }

        /// <summary>
        /// 初始化布局
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        public void InitLayout(int r, int c)
        {
            Layout = new int[Row, Col];
            GameState = FiveGameState.Init;
            MaxPlayerCount = 2;

            int leftIndexWidth = Pixel;
            StartX = leftIndexWidth;
            StartY = TitleHeight + leftIndexWidth;

            int width = 0, height = 0;
            width = leftIndexWidth * 2 + Pixel * Col;
            height = TitleHeight + leftIndexWidth * 2 + Pixel * Row;
            if(LayoutMap != null)
            {
                this.Draw.Dispose();
                LayoutMap.Dispose();
            }
            //创建图片
            LayoutMap = new Bitmap(width, height);
            Draw = Graphics.FromImage(LayoutMap);

            SolidBrush black = new SolidBrush(Color.Black);
            Pen p = new Pen(black, 2);
            //背景
            Draw.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, width, height));

            for (int i = 0; i <= Row; i++)
            {
                int posHei = StartY + i * Pixel;
                if (i != Row)
                {
                    Draw.DrawString((i + 1).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(0, posHei));
                    Draw.DrawString((i + 1).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(LayoutMap.Width - leftIndexWidth, posHei));
                }
                Draw.DrawLine(p, new Point(StartX, posHei), new Point(LayoutMap.Width - leftIndexWidth, posHei));
            }

            for (int i = 0; i <= Col; i++)
            {
                int posWid = StartX + i * Pixel;
                if (i != Col)
                {
                    Draw.DrawString((i + 1).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(posWid, TitleHeight));
                    Draw.DrawString((i + 1).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(posWid, LayoutMap.Height - leftIndexWidth));
                }
                Draw.DrawLine(p, new Point(posWid, StartY), new Point(posWid, LayoutMap.Height - Pixel));
            }
        }
        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="brush"></param>
        private void DrawImage(int row, int col, Brush brush)
        {
            Draw.FillEllipse(brush, new Rectangle(StartX + col * Pixel, StartY + row * Pixel, Pixel, Pixel));
        }
        /// <summary>
        /// 更新显示玩家信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void UpdatePlayerInfo(int row, int col, bool isReset = false, bool init = false)
        {
            Draw.FillRectangle(WhiteBrush, new Rectangle(0, 0, LayoutMap.Width, TitleHeight));
            var p1 = GamePlayers.First() as FiveGamePlayer;
            var p2 = GamePlayers.Last() as FiveGamePlayer;
            int x = 5;
            int y_one = 0;
            int offset = 2;
            var Pixel = this.Pixel - 5;
            Draw.FillEllipse(Brushes[p1.Index - 1], new Rectangle(x, y_one, Pixel, Pixel));
            Draw.DrawString(":" + p1.PlayerNickName, TextFont, BlackBrush, new PointF(x + Pixel + offset, y_one));

            int y = y_one + Pixel + offset;
            Draw.FillEllipse(Brushes[p2.Index - 1], new Rectangle(x, y, Pixel, Pixel));
            Draw.DrawString(":" + p2.PlayerNickName, TextFont, BlackBrush, new PointF(x + Pixel + offset, y));
            if (!init)
            {
                string conent = isReset ? "玩家悔棋!" : "当前玩家:{0}, 下子:[{1},{2}]".Format(DoPlayer.PlayerNickName, row + 1, col + 1);

                Draw.DrawString(conent, TextFont, BlackBrush, new PointF(x, y + this.Pixel + offset));
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="gameInput"></param>
        /// <returns></returns>
        public async override Task<bool> Init(GameInput gameInput)
        {
            if (!IsFullPlayer)
            {
                return false;
            }
            if(GamePlayers.Count > 0)
            {
                return true;
            }
            int index = 1;
            foreach (var item in BaseInfos)
            {
                GamePlayers.Add(new FiveGamePlayer() { PlayerId = item.Id, PlayerNickName = item.Name, Number = 0, Index = index++ });
            }
            DoPlayer = GamePlayers[this.Random.Next(0, GamePlayers.Count)] as FiveGamePlayer;

            UpdatePlayerInfo(0, 0, false, true);
            return true;
        }
        /// <summary>
        /// 交换下子选手
        /// </summary>
        public void ChangePlayer()
        {
            if (DoPlayer.Index == 0)
            {
                DoPlayer = GamePlayers[1] as FiveGamePlayer;
            }
            else if (DoPlayer.Index == 1)
            {
                DoPlayer = GamePlayers[0] as FiveGamePlayer;
            }

        }
        /// <summary>
        /// 获取图片留
        /// </summary>
        /// <returns></returns>
        public Stream GetImageStream()
        {
            MemoryStream memoryStream = new MemoryStream();
            LayoutMap.Save(memoryStream, ImageFormat.Jpeg);
            return memoryStream;
        }
        /// <summary>
        /// 扩充
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task Expend(int r, int c)
        {
            var newR = Row + r;
            var newC = Col + c;
            int [,]layout = new int[Row + r, Col + c];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    layout[i, j] = Layout[i, j];
                }
            }


        }
        /// <summary>
        /// 设置布局
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<bool> SetLayout(int r, int c)
        {
            r--;
            c--;
            if (r >= Row || c >= Col || r < 0 || r < 0 || Layout[r, c] != 0)
                return false;
            Layout[r, c] = DoPlayer.Index;
            UpdatePlayerInfo(r, c, false);
            DrawImage(r, c, Brushes[DoPlayer.Index - 1]);
            var idx = DoPlayer.Index % GamePlayers.Count;
            DoPlayer = GamePlayers[idx] as FiveGamePlayer;
            return true;
        }
        /// <summary>
        /// 对上一步的悔棋
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<bool> ResetLast(int r, int c)
        {
            r--;
            c--;
            if (Layout[r, c] == 0) return false;
            Layout[r, c] = 0;
            UpdatePlayerInfo(r, c, true);
            //切换选手
            var idx = DoPlayer.Index % GamePlayers.Count;
            DoPlayer = GamePlayers[idx] as FiveGamePlayer;
            DrawImage(r, c, WhiteBrush);
            return true;
        }
        /// <summary>
        /// 当前方向是否可以
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="count"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsOk(int dir, int count, int i, int j, int value)
        {
            switch (dir)
            {
                case 0:
                    j--;
                    break;
                case 1:
                    i--;j--;
                    break;
                case 2:
                    i--;
                    break;
                case 3:
                    i--;
                    j++;
                    break;
                case 4:
                    j++;
                    break;
                case 5:
                    i++;
                    j++;
                    break;
                case 6:
                    i++;
                    break;
                case 7:
                    i++;
                    j--;
                    break;
                default:
                    break;
            }
            if (i >= Row || j >= Col || i < 0 || j < 0)
                return false;

            if (Layout[i, j] == value)
            {
                count++;
            }
            else return false;
            if (count >= 5) return true;
            return IsOk(dir, count, i, j, value);
        }
        /// <summary>
        /// 是否游戏结束
        /// </summary>
        /// <returns></returns>
        public IGamePlayer IsOver()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    int value = Layout[i, j];
                    if (value == 0) continue;
                    for (int k = 0; k < 8; k++)
                    {
                        if(IsOk(k, 1, i, j, value))
                        {
                            return GamePlayers.Where(p => (p as FiveGamePlayer).Index == value).FirstOrDefault();
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            Draw.Dispose();
            LayoutMap.Dispose();
        }
    }
}
