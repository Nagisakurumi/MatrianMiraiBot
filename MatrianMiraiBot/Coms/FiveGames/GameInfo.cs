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
        /// 一个格子的像素
        /// </summary>
        public int Pixel { get; set; } = 20;
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
            Row = row + 1;
            Col = col + 1;
            Layout = new int[Row, Col];
            GameState = FiveGameState.Init;
            MaxPlayerCount = 2;

            //创建图片
            LayoutMap = new Bitmap(Col * Pixel + Pixel, Row * Pixel + Pixel);
            Draw = Graphics.FromImage(LayoutMap);

            SolidBrush black = new SolidBrush(Color.Black);
            Pen p = new Pen(black, 2);
            //背景
            Draw.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, LayoutMap.Width, LayoutMap.Height));

            for (int i = 1; i <= Row; i++)
            {
                if (i != Row)
                {
                    Draw.DrawString((i).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(0, i * Pixel));
                    Draw.DrawString((i).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(LayoutMap.Width - Pixel, i * Pixel));
                }
                int y = i * Pixel;
                Draw.DrawLine(p, new Point(Pixel, y), new Point(LayoutMap.Width - Pixel, y));
            }
            //Draw.DrawLine(p, new Point(Pixel, LayoutMap.Height), new Point(LayoutMap.Width, LayoutMap.Height));

            for (int i = 1; i <= Col; i++)
            {
                if (i != Col)
                {
                    Draw.DrawString((i).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(i * Pixel, 0));
                    Draw.DrawString((i).ToString(), new Font(FontFamily.GenericMonospace, 10), black, new PointF(i * Pixel, LayoutMap.Height - Pixel));
                }
                int x = i * Pixel;
                Draw.DrawLine(p, new Point(x, Pixel), new Point(x, LayoutMap.Height - Pixel));
            }
            //Draw.DrawLine(p, new Point(LayoutMap.Width, Pixel), new Point(LayoutMap.Width, LayoutMap.Height));
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
        /// 设置布局
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<bool> SetLayout(int r, int c)
        {
            if (r >= Row || c >= Col || r <= 0 || r <= 0 || Layout[r, c] != 0)
                return false;

            Layout[r, c] = DoPlayer.Index;
            Draw.FillEllipse(Brushes[DoPlayer.Index - 1], new Rectangle(c * Pixel, r * Pixel, Pixel, Pixel));
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
            if (Layout[r, c] == 0) return false;

            Layout[r, c] = 0;
            Draw.FillEllipse(WhiteBrush, new Rectangle(c * Pixel, r * Pixel, Pixel, Pixel));
            //切换选手
            var idx = DoPlayer.Index % GamePlayers.Count;
            DoPlayer = GamePlayers[idx] as FiveGamePlayer;
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
