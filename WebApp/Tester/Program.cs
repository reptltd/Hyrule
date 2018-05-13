using System;
using System.Collections.Generic;
using System.Drawing;
using TriangleLib;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
			// set boundaries
			char firstRow = 'A';
			char lastRow = 'F';
			short firstColumn = 1;
			short lastColumn = 12;

			// test beyond acceptable bounds
			//firstRow--;
			//lastRow++;
			//firstColumn--;
			//lastColumn++;
			
			// results will be used to feed the input into the next function
			Dictionary<string, List<Point>> results = new Dictionary<string, List<Point>>();

			for (char row = firstRow; row <= lastRow; row++)
			{
				for (short column = firstColumn; column <= lastColumn; column++)
				{
					string name = row + column.ToString();
					Console.WriteLine(name);
					string error;
					List<Point> points = TriangleCalc.GetCoordinatesFromRowColumn(row, column, out error);

					results.Add(name, new List<Point>());
					foreach (Point point in points)
					{
						Console.WriteLine(point.X + " " + point.Y);
						results[name].Add(point);
					}

					if (points.Count == 0)
						Console.WriteLine(error);
					//else
					//	Console.ReadKey();
				}
			}

			// use the points we got from our previous test and check the second function
			foreach(string name in results.Keys)
			{
				List<Point> points = results[name];
				string res = TriangleCalc.GetRowColumnFromVertices(points[0], points[1], points[2]);
				if (res == name)
					Console.WriteLine(res + " success");
				else
					Console.WriteLine(res + " failure");
			}

			//Console.ReadKey();

			// set boundaries
			short firstVertexX = 0;
			int lastVertexX = 60;
			short firstVertexY = 0;
			int lastVertexY = 60;

			// test beyond acceptable bounds 
			//firstVertexX--;
			//firstVertexY--;
			//lastVertexX++;
			//lastVertexY++;

			Point p1 = new Point(0, 0);
			Point p2 = new Point(0, 0);
			Point p3 = new Point(0, 0);

			// this is a super exhaustive test
			for (p1.X = firstVertexX; p1.X <= lastVertexX; p1.X++)
			{
				for (p1.Y = firstVertexY; p1.Y <= lastVertexY; p1.Y++)
				{
					for (p2.X = firstVertexX; p2.X <= lastVertexX; p2.X++)
					{
						for (p2.Y = firstVertexY; p2.Y <= lastVertexY; p2.Y++)
						{
							for (p3.X = firstVertexX; p3.X <= lastVertexX; p3.X++)
							{
								for (p3.Y = firstVertexY; p3.Y <= lastVertexY; p3.Y++)
								{
									Console.WriteLine(p1.X + "," + p1.Y + " " + p2.X + "," + p2.Y + " " + p3.X + "," + p3.Y);
									string res = TriangleCalc.GetRowColumnFromVertices(p1, p2, p3);
									Console.WriteLine(res);
									//if (res.Length < 10)
									//	Console.ReadKey();
								}
							}
						}
					}
				}
			}
			Console.ReadKey();
		}
    }
}
