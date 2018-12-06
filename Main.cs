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

		// In my case, the scale is actually independent of the canvas' size, because the application is responsive.
		// Because I wanted consistent values for each screen size, scale 1 shows the full range from MIN to MAX,
		// instead of the 0.01 in the assignment description.
		private double scale;
		private int maxIterations;

		// The function delegate to calculate the color based on the Mandelbrot value, this
		// represents the current delegate and can be overwritten by the given presets.
		private ModifyColor modifyColor;

		// A flag to indicate if we're currently running the animation or not. Used for starting and stopping.
		private bool runningAnimation;

		// A timer that is used to animate, with an interval of 1 ms.
		private readonly Timer animationTimer;

		#region Presets

		private readonly Preset[] presets =
		{
			new Preset
			{
				Name = "Default",
				CenterX = 0,
				CenterY = 0,
				Scale = 1,
				MaxIterations = 100
			},
			new Preset
			{
				Name = "Deep Ocean",
				CenterX = -1.16711590172762,
				CenterY = -0.276972817973466,
				Scale = .00625,
				MaxIterations = 100,
				ModifyColor = (mandelbrot, maxIterations) => mandelbrot == maxIterations || mandelbrot % 2 != 0
					? Color.Black
					: Color.FromArgb(mandelbrot % 128 * 2, mandelbrot % 32 * 7, mandelbrot % 16 * 14)
			},
			new Preset
			{
				Name = "Black & White",
				CenterX = 0.349372201726918,
				CenterY = 0.512074673562276,
				Scale = .001,
				MaxIterations = 100,
				ModifyColor = (mandelbrot, maxIterations) => mandelbrot == maxIterations || mandelbrot % 2 != 0
					? Color.White
					: Color.Black
			},
			new Preset
			{
				Name = "Sun Burned",
				CenterX = -0.545459469096025,
				CenterY = -0.600961486038721,
				Scale = .01,
				MaxIterations = 100,
				ModifyColor = (mandelbrot, maxIterations) =>
					Color.FromArgb(mandelbrot % 128 * 2, mandelbrot % 16 * 7, mandelbrot % 16 * 14)
			},
			new Preset
			{
				Name = "Bee Movie (2007)",
				CenterX = -0.744390975761286,
				CenterY = 0.172797221807631,
				Scale = 0.1,
				MaxIterations = 27,
				ModifyColor = (mandelbrot, maxIterations) => mandelbrot == maxIterations || mandelbrot % 2 != 0
					? Color.Gold
					: Color.FromArgb(mandelbrot % 64 * 2, mandelbrot % 8 * 18, mandelbrot % 16 * 10)
			},
			new Preset
			{
				Name = "Hackerman",
				CenterX = 0.35128659599691,
				CenterY = 0.588514292330695,
				Scale = 1.81898940354585E-15,
				MaxIterations = 300,
				ModifyColor = (mandelbrot, maxIterations) => mandelbrot == maxIterations || mandelbrot % 3 != 0
					? Color.Black
					: Color.Lime
			}
		};

		#endregion

		// Initially set to minimized to trigger first maximize button click properly.
		private FormWindowState lastWindowState = FormWindowState.Minimized;

		// Store the width and height here, to prevent calling the property getters all the time.
		private int canvasWidth;
		private int canvasHeight;

		public Main()
		{
			InitializeComponent();
			// "Click" OK on hitting Enter as well.
			AcceptButton = okButton;

			// Add items to the picture box and initialize the first preset.
			presetsComboBox.Items.AddRange(presets);
			presetsComboBox.SelectedIndex = 0;

			// Initialise the animator, it will update as fast as possible with an
			// interval of 1 ms, if one of the threads on the CPU can handle that,
			// since the Mandelbrot calculation is not multi-threaded.
			animationTimer = new Timer();
			animationTimer.Tick += AnimationTimer_Tick;
			animationTimer.Interval = 1;
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

		/// <summary>
		/// Resize the canvas based on the form size.
		/// </summary>
		private void ResizeMandelbrot(object sender)
		{
			Control control = (Control) sender;

			// Calculate the canvas' size to be the size of the Form - the height of the control panel (where the input fields are).
			canvasWidth = control.ClientSize.Width;
			canvasHeight = control.ClientSize.Height - canvas.Location.Y;
			canvas.Size = new Size(canvasWidth, canvasHeight);

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
			maxInput.Value = maxIterations;
		}

		private void DrawMandelbrot()
		{
			var bitmap = new Bitmap(canvasWidth, canvasHeight);

			// Calculate offsets, to always center the Mandelbrot, based on which side of the canvas is bigger.
			int sizeMin = Math.Min(canvasWidth, canvasHeight);
			double offsetX = (canvasWidth - sizeMin) / 2d;
			double offsetY = (canvasHeight - sizeMin) / 2d;

			for (int y = 0; y < canvasHeight; y++)
			{
				for (int x = 0; x < canvasWidth; x++)
				{
					// Calculate and map the range of the current x and y values to pass into the Mandelbrot
					// method. The scale and offsets are taken into account as well.
					double scaledX = MapRange(x, offsetX, sizeMin + offsetX, MIN, MAX) * scale + centerX;
					double scaledY = MapRange(y, offsetY, sizeMin + offsetY, MIN, MAX) * scale + centerY;

					int mandelbrot = CalculateMandelbrot(scaledX, scaledY, maxIterations);
					Color color = modifyColor(mandelbrot, maxIterations);
					bitmap.SetPixel(x, y, color);
				}
			}

			// Clear the previous bitmap, to prevent memory leaks.
			canvas.Image?.Dispose();
			canvas.Image = bitmap;
		}

		private int CalculateMandelbrot(double x, double y, int iterations)
		{
			double a = 0;
			double b = 0;

			int i = 0;
			// 4 is 2 squared, the compiler will probably optimize that but I'm not taking any risks here.
			while (a * a + b * b < 4 && i < iterations)
			{
				double aTemp = a * a - b * b + x;
				b = 2 * a * b + y;
				a = aTemp;

				i++;
			}

			return i;
		}

		/// <summary>
		/// Maps a value in from a range to another range, like:
		/// 200 in a range of 0-400 to 0 in a range of -2.5-2.5.
		/// Could also be an extension method, but I think that's a
		/// little out of scope for this course.
		/// </summary>
		private static double MapRange(double value, double fromMin, double fromMax, double toMin, double toMax)
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

			// Calculate offsets based on the bigger side. This way the mouse click will be correctly handled
			// and we will zoom on the correct spot.
			int sizeMin = Math.Min(canvasWidth, canvasHeight);
			double offsetX = (canvasWidth - sizeMin) / 2d;
			double offsetY = (canvasHeight - sizeMin) / 2d;
			centerX += MapRange(e.X, offsetX, sizeMin + offsetX, MIN, MAX) * scale;
			centerY += MapRange(e.Y, offsetY, sizeMin + offsetY, MIN, MAX) * scale;

			WriteValuesToUI();
			DrawMandelbrot();
		}

		private void AnimationTimer_Tick(object sender, EventArgs e)
		{
			scale /= 1.01;
			WriteValuesToUI();
			DrawMandelbrot();
		}

		private void PresetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Invalid selection.
			if (presetsComboBox.SelectedIndex == -1)
			{
				return;
			}

			// Update the values in the UI based on the selected preset.
			Preset currentPreset = (Preset) presetsComboBox.SelectedItem;
			centerX = currentPreset.CenterX;
			centerY = currentPreset.CenterY;
			scale = currentPreset.Scale;
			maxIterations = currentPreset.MaxIterations;
			modifyColor = currentPreset.ModifyColor;

			WriteValuesToUI();

			// Use the resize method instead to make sure the canvas is getting the correct sizes the first time.
			ResizeMandelbrot(this);
		}

		private void AnimationButton_Click(object sender, EventArgs e)
		{
			runningAnimation = !runningAnimation;

			if (runningAnimation)
			{
				animationButton.Text = "Stop Animation";
				animationTimer.Start();
			}
			else
			{
				animationButton.Text = "Start Animation";
				animationTimer.Stop();
			}

			animationButton.Text += " (CPU intensive)";
		}
	}
}