using System.Drawing;

namespace Mandelbrot
{
	public delegate Color ModifyColor(int mandelbrot, int maxIterations);

	public class Preset
	{
		public string Name { get; set; }
		public double CenterX { get; set; }
		public double CenterY { get; set; }
		public double Scale { get; set; }
		public int MaxIterations { get; set; }

		// The default color modifying function is saved here, that way there is no need to
		// provide a ModifyColor function by creating a preset every single time.
		public ModifyColor ModifyColor { get; set; } = (mandelbrot, maxIterations) =>
			mandelbrot == maxIterations || mandelbrot % 2 != 0
				? Color.Black
				: Color.White;

		// This will be showed in the UI.
		public override string ToString()
		{
			return Name;
		}
	}
}