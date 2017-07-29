﻿using Data.Gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EDGui.Utils
{

    public class UIColoring
    {
        public static Color DEAD_PLAYER = Color.FromArgb(255, 255, 255, 255);
        public static Color TEAM_1 = Color.FromArgb(255, 255, 0, 0);
        public static Color TEAM_2 = Color.FromArgb(255, 0, 0, 255);
        public static Color TEAM_3 = Color.FromArgb(255, 0, 255, 255);

        /// <summary>
        /// Changes the color of a given color by a correctionfactor for all rgb values
        /// </summary>
        /// <param name="color"></param>
        /// <param name="correctionFactor"></param>
        /// <returns></returns>
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
        }

        public static Color GetEntityColor(Entity e)
        {
            if (e is Player)
            {
                var p = e as Player;
                switch (p.GetTeam()){
                    case Team.T:
                        return TEAM_1;
                    case Team.CT:
                        return TEAM_2;
                }
            }
            else if (e is Entity){
                switch (e.Owner.GetTeam())
                {
                    case Team.One:
                        return TEAM_1;
                    case Team.Two:
                        return TEAM_2;
                    case Team.Three:
                        return TEAM_3;
                }
            }

            return DEAD_PLAYER;

        }

        public static Color RandomColor()
        {
            return Color.FromRgb(getRGB(0), getRGB(1), getRGB(2));
        }

        public static byte getRGB(int index)
        {
            int[] p = getPattern(index);
            return (byte) (getElement(p[0]) << 16 | getElement(p[1]) << 8 | getElement(p[2]));
        }

        public static int getElement(int index)
        {
            int value = index - 1;
            int v = 0;
            for (int i = 0; i < 8; i++)
            {
                v = v | (value & 1);
                v <<= 1;
                value >>= 1;
            }
            v >>= 1;
            return v & 0xFF;
        }

        public static int[] getPattern(int index)
        {
            int n = (int)Math.Pow(index, 0.3333333333333333);
            index -= (n * n * n);
            int[] p = new int[3];
            p[0] = n;
            p[1] = n;
            p[2] = n;
            if (index == 0)
            {
                return p;
            }
            index--;
            int v = index % 3;
            index = index / 3;
            if (index < n)
            {
                p[v] = index % n;
                return p;
            }
            index -= n;
            p[v] = index / n;
            p[++v % 3] = index % n;
            return p;
        }

    }
}