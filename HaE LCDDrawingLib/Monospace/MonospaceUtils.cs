﻿using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRageMath;

namespace IngameScript
{
	partial class Program
	{
        public static class MonospaceUtils
	    {
            public static char GetColorChar(byte R, byte G, byte B)
            {
                GetSeven(ref R);
                GetSeven(ref G);
                GetSeven(ref B);

                return CreateCustomColor(R, G, B);
            }

            public static char GetColorChar(Color color)
            {
                return GetColorChar(color.R, color.G, color.B);
            }

            private static char CreateCustomColor(byte r, byte g, byte b)
            {
                return (char)(0xE100 + (MathHelper.Clamp(r, 0, 7) << 6) + (MathHelper.Clamp(g, 0, 7) << 3) + MathHelper.Clamp(b, 0, 7));
            }

            private static void GetSeven(ref byte B)
            {
                double I = B / 256.0;
                I *= 7;
                B = (byte)(int)Math.Round(I);
            }

            public static void PlotLine(int x0, int y0, int x1, int y1, Canvas canvas, char color)
            {
                int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
                int dy = -Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
                int err = dx + dy, e2; /* error value e_xy */

                for (; ; )
                {  /* loop */
                    canvas.PaintPixel(color, x0, y0);
                    if (x0 == x1 && y0 == y1) break;
                    e2 = 2 * err;
                    if (e2 >= dy) { err += dy; x0 += sx; } /* e_xy+e_x > 0 */
                    if (e2 <= dx) { err += dx; y0 += sy; } /* e_xy+e_y < 0 */
                }
            }
        }
	}
}
