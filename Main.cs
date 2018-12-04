using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Mandelbrot
{
	public partial class Main : Form
	{
		private const double MIN = -2.5;
		private const double MAX = 2.5;

		private double centerX;
		private double centerY;
		private double scale;
		private int maxIterations;

		// Initially set to minimized to trigger first maximize button click properly.
		private FormWindowState lastWindowState = FormWindowState.Minimized;
		private Timer animationTimer;

		public Main()
		{
			InitializeComponent();

			ReadUIValues();
			// Properly size the PictureBox and draw the first Mandelbrot.
			ResizeMandelbrot(this);

			animationTimer = new Timer();
			animationTimer.Tick += AnimationTimer_Tick;
			animationTimer.Interval = 1;
			//StartAnimation();
		}

		/// <summary>
		/// Making sure the maximize button works as well, to redraw the Mandelbrot.
		/// </summary>
		private void Main_Resize(object sender, EventArgs e)
		{
			if (WindowState != lastWindowState)
			{
				lastWindowState = WindowState;

				if (WindowState == FormWindowState.Maximized || WindowState == FormWindowState.Normal)
				{
					ResizeMandelbrot(sender);
				}
			}
		}

		private void Main_ResizeEnd(object sender, EventArgs e)
		{
			ResizeMandelbrot(sender);
		}

		private void ResizeMandelbrot(object sender)
		{
			Control control = (Control) sender;
			// Calculate the canvas' size to be the size of the Form - the height of the control panel (where the input fields are).
			canvas.Size = new Size(
				control.ClientRectangle.Size.Width,
				control.ClientRectangle.Size.Height - canvas.Location.Y);

			DrawMandelbrot();
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			ReadUIValues();
			DrawMandelbrot();
		}

		private void ReadUIValues()
		{
			double oldValue = centerX;
			centerXTextBox.Text = centerXTextBox.Text.Replace(',', '.');
			if (!double.TryParse(centerXTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out centerX))
			{
				centerX = oldValue;
			}

			oldValue = centerY;
			centerYTextBox.Text = centerYTextBox.Text.Replace(',', '.');
			if (!double.TryParse(centerYTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out centerY))
			{
				centerY = oldValue;
			}

			oldValue = scale;
			scaleTextBox.Text = scaleTextBox.Text.Replace(',', '.');
			if (!double.TryParse(scaleTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out scale))
			{
				scale = oldValue;
			}

			maxIterations = (int) maxInput.Value;

			WriteValuesToUI();
		}

		private void WriteValuesToUI()
		{
			centerXTextBox.Text = centerX.ToString(CultureInfo.InvariantCulture);
			centerYTextBox.Text = centerY.ToString(CultureInfo.InvariantCulture);
			scaleTextBox.Text = scale.ToString(CultureInfo.InvariantCulture);
		}

		private void DrawMandelbrot()
		{
			int height = canvas.ClientSize.Height;
			int width = canvas.ClientSize.Width;
			var bitmap = new Bitmap(width, height);

			// Calculate offsets, to always center the Mandelbrot.
			int sizeMin = Math.Min(width, height);
			double offsetX = (width - sizeMin) / 2d;
			double offsetY = (height - sizeMin) / 2d;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					double scaledX = MapRange(x, offsetX, sizeMin + offsetX, MIN, MAX) * scale + centerX;
					double scaledY = MapRange(y, offsetY, sizeMin + offsetY, MIN, MAX) * scale + centerY;

					int mandelbrot = CalculateMandelbrot(scaledX, scaledY, maxIterations);
					Color color = mandelbrot == maxIterations || mandelbrot % 2 != 0
						? Color.Black
						: Color.FromArgb(mandelbrot % 128 * 2, mandelbrot % 32 * 7, mandelbrot % 16 * 14);
					bitmap.SetPixel(x, y, color);
				}
			}

			// Clear the previous bitmap.
			canvas.Image?.Dispose();
			canvas.Image = bitmap;
		}

		private int CalculateMandelbrot(double x, double y, int iterations)
		{
			double a = 0;
			double b = 0;

			int i = 0;
			while (a * a + b * b < 4 && i < iterations)
			{
				double aTemp = a * a - b * b + x;
				b = 2 * a * b + y;
				a = aTemp;

				i++;
			}

			return i;
		}

		public static double MapRange(double value, double fromMin, double fromMax, double toMin, double toMax)
		{
			return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
		}

		private void Canvas_MouseClick(object sender, MouseEventArgs e)
		{
			// Zoom out on right click and zoom in on any other mouse button.
			if (e.Button == MouseButtons.Right)
			{
				scale *= 2;
			}
			else
			{
				scale /= 2;
			}

			int height = canvas.ClientSize.Height;
			int width = canvas.ClientSize.Width;
			int sizeMin = Math.Min(width, height);
			double offsetX = (width - sizeMin) / 2d;
			double offsetY = (height - sizeMin) / 2d;
			centerX += MapRange(e.X, offsetX, sizeMin + offsetX, MIN, MAX) * scale;
			centerY += MapRange(e.Y, offsetY, sizeMin + offsetY, MIN, MAX) * scale;

			WriteValuesToUI();
			DrawMandelbrot();
		}

		private void StartAnimation()
		{
			centerX = -1.12717668726378;
			centerY = -0.26968634665177;
			centerX = 0.349372201726918;
			centerY = 0.512074673562276;
			animationTimer.Start();
		}

		private void AnimationTimer_Tick(object sender, EventArgs e)
		{
			scale /= 1.01;
			WriteValuesToUI();
			DrawMandelbrot();
		}
	}
}