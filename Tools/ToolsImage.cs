using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace DcsBriefop.Tools
{
	internal static class ToolsImage
	{
		#region Bitmap
		private static Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		public static Bitmap GetCachedBitmap(string sBitmapName)
		{
			if (!m_bitmapCache.TryGetValue(sBitmapName, out Bitmap bmp))
			{
				if (File.Exists(sBitmapName))
				{
					try { bmp = new Bitmap(sBitmapName); }
					catch (Exception ex) { Log.Exception(ex); }
				}
				else
				{
					bmp = Properties.Resources.ResourceManager.GetObject(sBitmapName, Properties.Resources.Culture) as Bitmap;
				}

				if (bmp is null)
					throw new ExceptionDcsBriefop($"Failed to create bitmap for file {sBitmapName}");

				m_bitmapCache.Add(sBitmapName, bmp);
			}

			return bmp;
		}
		#endregion

		#region Color
		public static Bitmap ColorTint(this Bitmap sourceBitmap, Color colorTint)
		{
			float fRedTint = colorTint.R / 255f;
			float fGreenTint = colorTint.G / 255f;
			float fBlueTint = colorTint.B / 255f;
			float fAlphaTint = colorTint.A / 255f;

			return sourceBitmap.ColorTintFromWhite(fBlueTint, fGreenTint, fRedTint, fAlphaTint);
		}

		public static Bitmap ColorTintFromBlack(this Bitmap sourceBitmap, float blueTint, float greenTint, float redTint)
		{
			BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

			System.Runtime.InteropServices.Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
			sourceBitmap.UnlockBits(sourceData);

			float blue = 0;
			float green = 0;
			float red = 0;

			for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
			{
				blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * blueTint;
				green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * greenTint;
				red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * redTint;

				if (blue > 255)
				{ blue = 255; }

				if (green > 255)
				{ green = 255; }

				if (red > 255)
				{ red = 255; }

				pixelBuffer[k] = (byte)blue;
				pixelBuffer[k + 1] = (byte)green;
				pixelBuffer[k + 2] = (byte)red;
			}

			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			System.Runtime.InteropServices.Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public static Bitmap ColorTintFromWhite(this Bitmap sourceBitmap, float blueTint, float greenTint, float redTint, float alphaTint)
		{
			BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
			byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

			System.Runtime.InteropServices.Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
			sourceBitmap.UnlockBits(sourceData);

			float blue = 0;
			float green = 0;
			float red = 0;
			float alpha = 0;

			for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
			{
				blue = pixelBuffer[k] - (pixelBuffer[k]) * (1 - blueTint);
				green = pixelBuffer[k + 1] - (pixelBuffer[k + 1]) * (1 - greenTint);
				red = pixelBuffer[k + 2] - (pixelBuffer[k + 2]) * (1 - redTint);
				alpha = pixelBuffer[k + 3] - (pixelBuffer[k + 3]) * (1 - alphaTint);

				if (blue < 0)
				{ blue = 0; }

				if (green < 0)
				{ green = 0; }

				if (red < 0)
				{ red = 0; }

				if (alpha < 0)
				{ alpha = 0; }

				pixelBuffer[k] = (byte)blue;
				pixelBuffer[k + 1] = (byte)green;
				pixelBuffer[k + 2] = (byte)red;
				pixelBuffer[k + 3] = (byte)alpha;
			}

			Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
			BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

			System.Runtime.InteropServices.Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
			resultBitmap.UnlockBits(resultData);

			return resultBitmap;
		}

		public static float Lerp(this float start, float end, float amount)
		{
			if (amount > 1)
				amount = 1;
			if (amount < 0)
				amount = 0;

			float difference = end - start;
			float adjusted = difference * amount;
			return start + adjusted;
		}

		public static Color Lerp(this Color color, Color colorTo, float amount)
		{
			// start colours as lerp-able floats
			float sr = color.R, sg = color.G, sb = color.B;

			// end colours as lerp-able floats
			float er = colorTo.R, eg = colorTo.G, eb = colorTo.B;

			// lerp the colours to get the difference
			byte r = (byte)sr.Lerp(er, amount),
						g = (byte)sg.Lerp(eg, amount),
						b = (byte)sb.Lerp(eb, amount);

			// return the new colour
			return Color.FromArgb(r, g, b);
		}

		public static int PerceivedBrightness(Color c)
		{
			return (int)Math.Sqrt(
			c.R * c.R * .299 +
			c.G * c.G * .587 +
			c.B * c.B * .114);
		}
		#endregion

		#region Draw
		public static void DrawString(Graphics g, PointF position, string sString, Font font, Color color, bool bDrawShadow)
		{
			if (bDrawShadow)
			{
				using (Brush shadowBrush = new SolidBrush(Color.FromArgb(120, Color.Black)))
				{
					g.DrawString(sString, font, shadowBrush, position.X + 1, position.Y + 1);
				}
			}

			using (Brush brush = new SolidBrush(color))
			{
				g.DrawString(sString, font, brush, position.X, position.Y);
			}
		}

		public static void DrawStringAngledCentered(Graphics g, Point centerPosition, string sString, Font font, Color color, bool bDrawShadow, float fAngle, int iVerticalOffset)
		{
			GraphicsState state = g.Save();
			g.TranslateTransform(centerPosition.X, centerPosition.Y);
			if (fAngle != 0)
				g.RotateTransform(fAngle);

			SizeF textSize = g.MeasureString(sString, font);
			DrawString(g, new PointF(-(textSize.Width / 2), iVerticalOffset), sString, font, color, bDrawShadow);

			g.Restore(state);
		}
		#endregion
	}
}
