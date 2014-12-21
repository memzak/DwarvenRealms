﻿using SimplexNoise;
using System;
using System.Drawing;
using DwarvenRealms.Properties;

namespace DwarvenRealms
{
    class DwarfWorldMap
    {
        int[,] biomeMap;
		int[,] evilMap;
		int[,] temperatureMap;
		int[,] volcanoMap;
        int[,] elevationMap;
        int[,] smoothedElevationMap;
        int[,] waterMap;
        int[,] riverHeightMap;
        Color[,] colorMap;

        // unsafe is needed to use raw pointers.
        // This implementation is not ideal, because before and after calling this function, the user has to manually lock/unlock the bitmap. But there seems to be no other, better, cleaner, more elegant way.
        unsafe Color fetchColor(int x, int y, int stride, int colorsize, System.Drawing.Imaging.BitmapData bmpdata)
        {
            byte* p = (byte*)(void*)bmpdata.Scan0.ToPointer(); // A raw pointer to bytes! This is a rarity in C#.
            int cs = colorsize;
            byte* row = &p[y * stride];
            byte b = row[x*cs];
            byte g = row[x*cs + 1];
            byte r = row[x*cs + 2];
            return Color.FromArgb(r, g, b);
        }

        public void loadElevationMap(string path)
        {
            Bitmap elevationBitmap = (Bitmap)Bitmap.FromFile(path);
            // For speed improvement, the bitmap must be locked and accessed unsafely.
            //                                          v lock entire bitmap      v readonly                                   v use pixel format of bitmap
            var bmpdata = elevationBitmap.LockBits(new Rectangle(0, 0, elevationBitmap.Width, elevationBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, elevationBitmap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8; // Divide by 8 because 1 byte is 8 bits and FormatSize returns bits.

            elevationMap = new int[elevationBitmap.Width, elevationBitmap.Height];
            for (int y = 0; y < elevationBitmap.Height; y++)
            {
                for (int x = 0; x < elevationBitmap.Width; x++)
                {
                    Color point = fetchColor(x, y, stride, colorsize, bmpdata);
                    int height;
                    if (point.R == 0)
                        height = point.B;
                    else
                        height = point.B + 25;
                    elevationMap[x, y] = height;
                }
            }
            // Once we are done, immediately unlock the bitmap
            elevationBitmap.UnlockBits(bmpdata);

            smoothedElevationMap = Blur.gaussianBlur(elevationMap, 8);
            Console.WriteLine("Loaded elevation map sized {0}x{1}", elevationMap.GetUpperBound(0), elevationMap.GetUpperBound(1));
        }

        public void loadWaterMap(string path)
        {
            Bitmap waterBitMap = (Bitmap)Bitmap.FromFile(path);

            // locking bitmap...
            var bmpdata = waterBitMap.LockBits(new Rectangle(0, 0, waterBitMap.Width, waterBitMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, waterBitMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;

            waterMap = new int[waterBitMap.Width, waterBitMap.Height];
            riverHeightMap = new int[waterBitMap.Width, waterBitMap.Height];
            for (int y = 0; y < waterBitMap.Height; y++)
            {
                for (int x = 0; x < waterBitMap.Width; x++)
                {
                    Color point = fetchColor(x, y, stride, colorsize, bmpdata);
                    if (point.R == 0 && point.G == 0)
                    {
                        waterMap[x, y] = point.B + 25;
                        riverHeightMap[x, y] = -1;
                    }
                    else if (point.R == 0)
                    {
                        waterMap[x, y] = -1;
                        riverHeightMap[x, y] = point.B;
                    }
                    else
                    {
                        waterMap[x, y] = -1;
                        riverHeightMap[x, y] = -1;
                    }
                }
            }
            waterBitMap.UnlockBits(bmpdata); // unlocked bitmap

            Console.WriteLine("Loaded ocean map sized {0}x{1}", waterMap.GetUpperBound(0), waterMap.GetUpperBound(1));
        }
        public void loadBiomeMap(string path)
        {
            Bitmap tempBiomeMap = (Bitmap)Bitmap.FromFile(path);
            // locking bitmap ...
            var bmpdata = tempBiomeMap.LockBits(new Rectangle(0, 0, tempBiomeMap.Width, tempBiomeMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, tempBiomeMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;
			colorMap = new Color[tempBiomeMap.Width, tempBiomeMap.Height];
            biomeMap = new int[tempBiomeMap.Width, tempBiomeMap.Height];
            for (int y = 0; y < tempBiomeMap.Height; y++)
            {
                for (int x = 0; x < tempBiomeMap.Width; x++)
                {
					colorMap[x, y] = fetchColor(x, y, stride, colorsize, bmpdata);
                    //biomeMap[x, y] = BiomeList.GetBiomeIndex(colorMap[x, y]); 
					biomeMap[x, y] = BiomeList.GetRandomBiome(); //DEBUG
                }
            }
			//4 = Minimum number of neighbours to stay the same.
			for (int i = 0; i < 1; i++)
                iterateBiomeMap(biomeMap, colorMap, 4, tempBiomeMap.Height, tempBiomeMap.Width); 

            tempBiomeMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded biome map sized {0}x{1}", biomeMap.GetUpperBound(0), biomeMap.GetUpperBound(1));
        }
		
		public void iterateBiomeMap(int[,] biomeMap, Color[,] colorMap, int neigh, int Height, int Width)
		{
		//Cellular Automata Stuff
			for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
					int orthadj = 0; 				//Number of neighbours that are similar / the same.
					int[] countBiomeNeigh = new int[255]; //Only 255 different biomes possible.
					for (int iy = y-1; iy <= y+1; iy++) 
					{
						for (int ix = x-1; ix <= x+1; ix++) 
						{
							//If different biome color on original map, treat as a similar neighbour.
							//The border of the map gets the same treatment.
                            if ((iy < 0) || (iy > Height) || (ix < 0) || (ix > Width)) 
							{
								orthadj += 1;
							}
							else
							{ //If biome is within bounds...
								countBiomeNeigh[biomeMap[ix, iy]]++;
								if (biomeMap[ix, iy] == biomeMap[x, y])
									orthadj += 1;
								else if (biomeMap[ix, iy] != biomeMap[x, y])
									if (colorMap[ix, iy] == colorMap[ix, iy])
										orthadj += 1;
							}
						}
					}
					//Oh dear, it doesn't have enough neighbours to stay the same...
					if (orthadj < neigh)
					{
						int temp = -1;
						int biomeWinner = 0;
						for (int i = 0; i < 256; i++)
						{
							if (countBiomeNeigh[i] > temp && countBiomeNeigh != null)
							{
								temp = countBiomeNeigh[i];
								biomeWinner = i;
							}
						} //Wooo, we have a winner for the new biome.
						biomeMap[x, y] = biomeWinner;
					}
                }
            }
		}

        public enum InterpolationChoice
        {
            linear,
            cosine,
            cubic,
            catmulRom,
            hermite,
            fritschCarlson
        }
        InterpolationChoice interpolationChoice = InterpolationChoice.fritschCarlson;

        public int getElevation(int x, int y)
        {
            return getClampedCoord(elevationMap, x, y);
        }

        public double getElevation(double x, double y)
        {
            return getInterpolatedValue(elevationMap, interpolationChoice, x, y);
        }
        public double getSmoothedElevation(double x, double y)
        {
            return getInterpolatedValue(smoothedElevationMap, interpolationChoice, x, y);
        }

        

        public static double getInterpolatedValue(int[,] array, InterpolationChoice type, double x, double y)
        {
            int x1, y1;
            x1 = (int)Math.Floor(x);
            y1 = (int)Math.Floor(y);

            double z11 = getClampedCoord(array, x1, y1);
            double z12 = getClampedCoord(array, x1, y1 + 1);
            double z21 = getClampedCoord(array, x1 + 1, y1);
            double z22 = getClampedCoord(array, x1 + 1, y1 + 1);

            double z00 = getClampedCoord(array, x1 - 1, y1 - 1);
            double z01 = getClampedCoord(array, x1 - 1, y1);
            double z02 = getClampedCoord(array, x1 - 1, y1 + 1);
            double z03 = getClampedCoord(array, x1 - 1, y1 + 2);
            double z10 = getClampedCoord(array, x1, y1 - 1);
            double z13 = getClampedCoord(array, x1, y1 + 2);
            double z20 = getClampedCoord(array, x1 + 1, y1 - 1);
            double z23 = getClampedCoord(array, x1 + 1, y1 + 2);
            double z30 = getClampedCoord(array, x1 + 2, y1 - 1);
            double z31 = getClampedCoord(array, x1 + 2, y1);
            double z32 = getClampedCoord(array, x1 + 2, y1 + 1);
            double z33 = getClampedCoord(array, x1 + 2, y1 + 2);

            double muy = x - x1;
            double mux = y - y1;

            //flat land sometimes gives trouble, so a slight increase can help that.
            double roundingCorrection = 0.5;

            switch (type)
            {
                case InterpolationChoice.linear:
                    return Interpolate.BiLinearInterpolate(z11, z12, z21, z22, mux, muy) + roundingCorrection;
                case InterpolationChoice.cosine:
                    return Interpolate.BiCosineInterpolate(z11, z12, z21, z22, mux, muy) + roundingCorrection;
                case InterpolationChoice.cubic:
                    return Interpolate.BiCubicInterpolate(z00, z01, z02, z03, z10, z11, z12, z13, z20, z21, z22, z23, z30, z31, z32, z33, mux, muy) + roundingCorrection;
                case InterpolationChoice.catmulRom:
                    return Interpolate.BiCatmullRomInterpolate(z00, z01, z02, z03, z10, z11, z12, z13, z20, z21, z22, z23, z30, z31, z32, z33, mux, muy) + roundingCorrection;
                case InterpolationChoice.hermite:
                    return Interpolate.BiHermiteInterpolate(z00, z01, z02, z03, z10, z11, z12, z13, z20, z21, z22, z23, z30, z31, z32, z33, mux, muy, 0.75) + roundingCorrection;
                case InterpolationChoice.fritschCarlson:
                    return Interpolate.BiFritschCarlsonInterpolate(z00, z01, z02, z03, z10, z11, z12, z13, z20, z21, z22, z23, z30, z31, z32, z33, mux, muy) + roundingCorrection;

            }
            return -1.0;
        }

        /// <summary>
        /// Returns the water level at the specified location, excluding rivers.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Water level, if available, or -1</returns>
        public int getWaterbodyLevel(int x, int y)
        {
            return getClampedCoord(waterMap, x, y);
        }
        public int getRiverLevel(int x, int y)
        {
            return getClampedCoord(riverHeightMap, x, y);
        }

        public int getBiome(int x, int y)
        {
            return getClampedCoord(biomeMap, x, y);
        }
        public int getBiome(double x, double y)
        {
            return getBiome((int)Math.Floor(x), (int)Math.Floor(y));
        }

        /// <summary>
        /// Safely reads the value from an index grid.
        /// </summary>
        /// <param name="grid">A two dimensional array of ints to read from</param>
        /// <param name="x">X coord</param>
        /// <param name="y">Y coord</param>
        /// <returns>The value stored at the nearest valid grid cell, or MinValue if the grid is invalid entirely.</returns>
        public static int getClampedCoord(int[,] grid, int x, int y)
        {
            if (grid == null || grid.Length == 0)
                return int.MinValue;
            if (x < grid.GetLowerBound(0))
                x = grid.GetLowerBound(0);
            if (x > grid.GetUpperBound(0))
                x = grid.GetUpperBound(0);
            if (y < grid.GetLowerBound(1))
                y = grid.GetLowerBound(1);
            if (y > grid.GetUpperBound(1))
                y = grid.GetUpperBound(1);
            return grid[x, y];
        }
        public static double getClampedCoord(double[,] grid, int x, int y)
        {
            if (grid == null || grid.Length == 0)
                return double.MinValue;
            if (x < grid.GetLowerBound(0))
                x = grid.GetLowerBound(0);
            if (x > grid.GetUpperBound(0))
                x = grid.GetUpperBound(0);
            if (y < grid.GetLowerBound(1))
                y = grid.GetLowerBound(1);
            if (y > grid.GetUpperBound(1))
                y = grid.GetUpperBound(1);
            return grid[x, y];
        }

        int getFuzzyCoords(int[,] grid, double x, double y)
        {

            return int.MinValue;
        }

    }
}
