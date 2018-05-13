using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TriangleLib
{
	public class TriangleCalc
	{
		// using constants, but we could always let the user pass in these vals through the API
		const short HEIGHT = 10;
		const short WIDTH = 10;

		const short ORIGIN_X = 0;
		const short ORIGIN_Y = 0;

		const int HYP_SQUARED = (HEIGHT * HEIGHT) + (WIDTH * WIDTH);

		static double GetDistance(Point p1, Point p2)
		{
			return Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2);
		}

		static bool IsPointValid(Point p)
		{
			double x = p.X - ORIGIN_X;
			double y = p.Y - ORIGIN_Y;

			if ((x % WIDTH) != 0)
				return false;

			return (y % HEIGHT) == 0;
		}

		public static List<Point> GetCoordinatesFromRowColumn(char row, short column, out string error)
		{
			List<Point> points = new List<Point>();
			if (!char.IsLetter(row))
			{
				error = "Invalid row";
				return points;
			}

			if (column < 1)
			{
				error = "Invalid column";
				return points;
			}

			error = "";

			short newRow = (short)(Char.ToLower(row) - 'a');
			short newColumn = column;

			bool isEvenColumn = (column % 2) == 0;
			if (isEvenColumn)
				newColumn--;

			// note that this will only work with right triangles but can work with both variable origins/widths/heights
			int topLeftX = ORIGIN_X + ((newColumn / 2) * WIDTH);
			int topLeftY = ORIGIN_Y + (newRow * HEIGHT);

			Point topLeft = new Point(topLeftX, topLeftY);
			points.Add(topLeft);

			if (isEvenColumn)
			{
				Point topRight = new Point(topLeft.X + WIDTH, topLeft.Y);
				Point bottomRight = new Point(topRight.X, topRight.Y + HEIGHT);
				points.Add(topRight);
				points.Add(bottomRight);
			}
			else
			{
				Point bottomLeft = new Point(topLeft.X, topLeft.Y + HEIGHT);
				Point bottomRight = new Point(bottomLeft.X + WIDTH, bottomLeft.Y);
				points.Add(bottomLeft);
				points.Add(bottomRight);
			}
			return points;
		}

		public static string GetRowColumnFromVertices(Point p1, Point p2, Point p3)
		{
			// do the points make a valid triangle?
			if (p1 == p2 || p2 == p3 || p3 == p1)
				return "One or more points are identical";

			if (!IsPointValid(p1))
				return "Invalid first point";

			if (!IsPointValid(p2))
				return "Invalid second point";

			if (!IsPointValid(p3))
				return "Invalid third point";

			double[] vals = new double[3];
			vals[0] = GetDistance(p1, p2);
			vals[1] = GetDistance(p2, p3);
			vals[2] = GetDistance(p3, p1);

			if (((vals[0] + vals[1]) != vals[2]) &&
				((vals[1] + vals[2]) != vals[0]) &&
				((vals[2] + vals[0]) != vals[1]))
				return "Not a right triangle";

			double max = vals.Max();
			if (max != HYP_SQUARED)
				return "Hypotenuse is too long meaning that this triangle is made up of smaller triangles";

			Point vertex;
			Point horizontal;
			Point vertical;

			// this algorithm will work for points in any order
			if (max == vals[0])
			{
				if (p3.Y == p1.Y)
				{
					horizontal = p1;
					vertical = p2;
				}
				else
				{
					horizontal = p2;
					vertical = p1;
				}
				vertex = p3;
			}
			else if (max == vals[1])
			{
				if (p1.Y == p2.Y)
				{
					horizontal = p2;
					vertical = p3;
				}
				else
				{
					horizontal = p3;
					vertical = p2;
				}
				vertex = p1;
			}
			else
			{
				if (p2.Y == p3.Y)
				{
					horizontal = p3;
					vertical = p1;
				}
				else
				{
					horizontal = p1;
					vertical = p3;
				}
				vertex = p2;
			}

			char row = 'a';
			int column = 0;

			// only 2 types of valid triangles
			// if vertex is bottom left
			if ((vertex.X + WIDTH == horizontal.X) && (vertex.Y - HEIGHT == vertical.Y))
			{
				row = (char)(((vertex.Y - ORIGIN_Y) / HEIGHT) + ('A' - 1));
				column = ((vertex.X - ORIGIN_X) / WIDTH);
				column *= 2;
				column++;
			}
			// if vertex is top right
			else if ((vertex.X - WIDTH == horizontal.X) && (vertex.Y + HEIGHT == vertical.Y))
			{
				row = (char)(((vertex.Y - ORIGIN_Y) / HEIGHT) + ('A'));
				column = ((vertex.X - ORIGIN_X) / WIDTH);
				column *= 2;
			}
			else
				return "Triangle is facing the wrong way";

			if (row < 'A' || column < 1)
				return "One or more points are outside the bounds";

			return row + column.ToString();
		}
	}
}
