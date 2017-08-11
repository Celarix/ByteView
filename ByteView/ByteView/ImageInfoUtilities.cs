﻿namespace ByteView
{
	/// <summary>
	/// Utilities to get information from images for display.
	/// </summary>
	internal static class ImageInfoUtilities
	{
		/// <summary>
		/// Gets a string formatting the number of pixels in an image.
		/// </summary>
		/// <param name="pixelCount">The number of pixels.</param>
		/// <returns>
		/// A formatted number of pixels (i.e. 1,000,000 pixels becomes 1 megapixel)
		/// </returns>
		public static string GetPixelCountString(float pixelCount)
		{
			string[] suffixes = { " pixels", " kilopixels", " megapixels", " gigapixels" };
			int suffixIndex = 0;

			while (pixelCount >= 1000)
			{
				pixelCount /= 1000;
				suffixIndex++;
			}

			return $"{pixelCount:F2} {suffixes[suffixIndex]}";
		}

		/// <summary>
		/// Given a pair of coordinates on an image, along with the image's
		/// dimensions, returns the address in the source files that provided the
		/// bytes for this pixel.
		/// </summary>
		/// <param name="imageWidth">The width of the image in pixels.</param>
		/// <param name="imageHeight">The height of the image in pixels.</param>
		/// <param name="x">The X-coordinate of the pixel in question.</param>
		/// <param name="y">The Y-coordinate of the pixel in question.</param>
		/// <param name="bitDepth">The number of bits per pixel.</param>
		/// <param name="address">
		/// The address of the first byte used to make this pixel.
		/// </param>
		/// <param name="bitIndex">
		/// The index of the first bit (from 0 to 7) used to make this pixel, or
		/// -1 if the bit depth is higher than 4 bpp.
		/// </param>
		public static void GetAddressFromImageCoordinate(int imageWidth, int imageHeight,
		int x, int y, BitDepth bitDepth, out long address, out int bitIndex)
		{
			long outAddress = 0L;
			int outBitIndex = 0;

			if (bitDepth == BitDepth.OneBpp)
			{
				GetAddressFrom1BppImageCoordinate(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.TwoBpp)
			{
				GetAddressFrom2BppImageCoordinate(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.FourBpp)
			{
				GetAddressFrom4BppImageCoordinate(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.EightBpp)
			{
				GetAddressFrom8BppImageCoordinates(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.SixteenBpp)
			{
				GetAddressFrom16BppImageCoordinates(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.TwentyFourBpp)
			{
				GetAddressFrom24BppImageCoordinates(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}
			else if (bitDepth == BitDepth.ThirtyTwoBpp)
			{
				GetAddressFrom32BppImageCoordinates(imageWidth, imageHeight, x, y, out outAddress,
				out outBitIndex);
			}

			address = outAddress;
			bitIndex = outBitIndex;
		}

		public static string FormatAddress(long address, int bitIndex)
		{
			string addressInHex = address.ToString("X8");
			if (bitIndex >= 0)
			{
				return $"0x{addressInHex}:{bitIndex}";
			}
			return $"0x{addressInHex}";
		}

		private static void GetAddressFrom1BppImageCoordinate(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			long bitsPerRow = width;
			long bitInRow = x;
			long bitAddress = (bitsPerRow * y) + bitInRow;
			address = bitAddress / 8;
			bitIndex = (int)(bitAddress % 8);
		}

		private static void GetAddressFrom2BppImageCoordinate(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			long bitsPerRow = width * 2;
			long bitInRow = x * 2;
			long bitAddress = (bitsPerRow * y) + bitInRow;
			address = bitAddress / 8;
			bitIndex = (int)(bitAddress % 8);
		}

		private static void GetAddressFrom4BppImageCoordinate(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			long bitsPerRow = width * 4;
			long bitInRow = x * 4;
			long bitAddress = (bitsPerRow * y) + bitInRow;
			address = bitAddress / 8;
			bitIndex = (int)(bitAddress % 8);
		}

		private static void GetAddressFrom8BppImageCoordinates(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			int bytesPerRow = width;
			int byteInRow = x;
			long byteAddress = (bytesPerRow * y) + byteInRow;
			address = byteAddress;
			bitIndex = -1;
		}

		private static void GetAddressFrom16BppImageCoordinates(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			int bytesPerRow = width * 2;
			int byteInRow = x * 2;
			long byteAddress = (bytesPerRow * y) + byteInRow;
			address = byteAddress;
			bitIndex = -1;
		}

		private static void GetAddressFrom24BppImageCoordinates(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			int bytesPerRow = width * 3;
			int byteInRow = x * 3;
			long byteAddress = (bytesPerRow * y) + byteInRow;
			address = byteAddress;
			bitIndex = -1;
		}

		private static void GetAddressFrom32BppImageCoordinates(int width, int height, int x, int y,
		out long address, out int bitIndex)
		{
			int bytesPerRow = width * 4;
			int byteInRow = x * 4;
			long byteAddress = (bytesPerRow * y) + byteInRow;
			address = byteAddress;
			bitIndex = -1;
		}
	}
}
