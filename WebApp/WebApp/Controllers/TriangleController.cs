using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Windows;
using System.Drawing;
using TriangleLib;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		//api/values/byRowCol?row=A&col=1
		[HttpGet("byRowCol")]
		public string Get(char row, short col)
		{
			string error;
			string res = row + col.ToString();
			List<Point> points = TriangleCalc.GetCoordinatesFromRowColumn(row, col, out error);
			foreach (Point point in points)
				res += "\r" + point.X + "," + point.Y;

			if (points.Count == 0)
				res += "\r" + error;

			return res;
		}

		//api/values/byPoints?x1=20&y1=30&x2=30&y2=30&x3=30&y3=40
		[HttpGet("byPoints")]
		public string Get(short x1, short y1, short x2, short y2, short x3, short y3)
		{
			Point p1 = new Point(x1, y1);
			Point p2 = new Point(x2, y2);
			Point p3 = new Point(x3, y3);

			string res = ("P1 " + p1.X + "," + p1.Y + " P2 " + p2.X + "," + p2.Y + " P3 " + p3.X + "," + p3.Y);
			res += "\r" + TriangleCalc.GetRowColumnFromVertices(p1, p2, p3);
			return res;
		}
	}
}
