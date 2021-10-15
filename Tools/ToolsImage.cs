using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DcsBriefop.Tools
{
	internal static class ToolsImage
	{
		public static Bitmap ColorTint(this Bitmap sourceBitmap, Color colorTint)
		{
			float fRedTint = colorTint.R / 255f;
			float fGreenTint = colorTint.G / 255f;
			float fBlueTint = colorTint.B / 255f;

			return sourceBitmap.ColorTint(fBlueTint, fGreenTint, fRedTint);
		}

		public static Bitmap ColorTint(this Bitmap sourceBitmap, float blueTint, float greenTint, float redTint)
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

		public static Icon GetIconResource(string sResource)
		{
			Icon ico = null;
			object oResource = Properties.Resources.ResourceManager.GetObject(sResource, Properties.Resources.Culture);
			if (oResource is Icon i)
				ico = i;
			else if (oResource is Bitmap b)
			{ 
				IntPtr icH = b.GetHicon();
				ico = Icon.FromHandle(icH);
				//DestroyIcon(icH);
			}

			return ico;
		}
	}
}
