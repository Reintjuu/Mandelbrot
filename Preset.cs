using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
	public class Preset
	{
		public string Name { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
		public double Scale { get; set; }
		public int MaxIterations { get; set; }
		public Func<int, int, Color> ModifyColor { get; set; } =
			(mandelbrot, maxIterations) => mandelbrot == maxIterations || mandelbrot % 2 != 0 ? Color.Black : Color.White;

		public override string ToString()
		{
			return Name;
		}
	}
}
