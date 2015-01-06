using SimplexNoise;
using System;
using System.Drawing;
using DwarvenRealms.Properties;

namespace DwarvenRealms
{
    class DwarfWorldMap
    {
        int[,] biomeMap;
        int[,] elevationMap;
        int[,] smoothedElevationMap;
        int[,] waterMap;
        int[,] riverHeightMap;
        BiomeConversion[,] mapMap;

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
            mapMap = new BiomeConversion[tempBiomeMap.Width, tempBiomeMap.Height];
            for (int y = 0; y < tempBiomeMap.Height; y++)
            {
                for (int x = 0; x < tempBiomeMap.Width; x++)
                {
                    mapMap[x, y] = BiomeList.ColorMatchBiome(fetchColor(x, y, stride, colorsize, bmpdata));
                }
            }

            tempBiomeMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded biome map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void loadTemperatureMap(string path)
        {
            Bitmap tempTmpMap = (Bitmap)Bitmap.FromFile(path);
            // locking bitmap ...
            var bmpdata = tempTmpMap.LockBits(new Rectangle(0, 0, tempTmpMap.Width, tempTmpMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, tempTmpMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;
            for (int y = 0; y < mapMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(0); x++)
                {
                    mapMap[x, y].modTemperature += BiomeList.modTmpConversion(fetchColor(x, y, stride, colorsize, bmpdata));
                }
            }

            tempTmpMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded temperature map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void loadVegetationMap(string path)
        {
            Bitmap tempVegMap = (Bitmap)Bitmap.FromFile(path);
            // locking bitmap ...
            var bmpdata = tempVegMap.LockBits(new Rectangle(0, 0, tempVegMap.Width, tempVegMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, tempVegMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;
            for (int y = 0; y < mapMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(0); x++)
                {
                    mapMap[x, y].modVegetation += BiomeList.modVegConversion(fetchColor(x, y, stride, colorsize, bmpdata));
                }
            }

            tempVegMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded vegetation map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void loadVolcanismMap(string path)
        {
            Bitmap tempVolMap = (Bitmap)Bitmap.FromFile(path);
            // locking bitmap ...
            var bmpdata = tempVolMap.LockBits(new Rectangle(0, 0, tempVolMap.Width, tempVolMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, tempVolMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;
            for (int y = 0; y < mapMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(0); x++)
                {
                    mapMap[x, y].modVolcanism += BiomeList.modVolConversion(fetchColor(x, y, stride, colorsize, bmpdata));
                }
            }

            tempVolMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded volcanism map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void loadEvilMap(string path)
        {
            Bitmap tempEvilMap = (Bitmap)Bitmap.FromFile(path);
            // locking bitmap ...
            var bmpdata = tempEvilMap.LockBits(new Rectangle(0, 0, tempEvilMap.Width, tempEvilMap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, tempEvilMap.PixelFormat);
            int stride = bmpdata.Stride;
            int colorsize = System.Drawing.Bitmap.GetPixelFormatSize(bmpdata.PixelFormat) / 8;
            for (int y = 0; y < mapMap.GetUpperBound(1); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(0); x++)
                {
                    mapMap[x, y].modEvilness += BiomeList.modEvilConversion(fetchColor(x, y, stride, colorsize, bmpdata));
                }
            }

            tempEvilMap.UnlockBits(bmpdata); // unlocked bitmap
            Console.WriteLine("Loaded evil map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void calculateBiomeMap()
        {
            Random biomeRandomizer = new Random();
            BiomeID biomeids = new BiomeID();
            biomeids.getBiomeIDs();
            biomeMap = new int[mapMap.GetUpperBound(0), mapMap.GetUpperBound(1)];
            //Now to start selecting biomes...
            for (int y = 0; y < mapMap.GetUpperBound(0); y++)
            {
                for (int x = 0; x <  mapMap.GetUpperBound(1); x++)
                {
                        //Standard biome selection process.
                        biomeMap[x, y] = mapMap[x, y].getNewBiome(biomeRandomizer, biomeids);

                        //3x3 Blobs for rarer biome types. (Eg: Volcanoes)
                        int randovolc = (biomeRandomizer.Next(0, 1000) - 500);
                        int randoevil = (biomeRandomizer.Next(0, 1000) - 500);

                        if ((randovolc + mapMap[x, y].modVolcanism) >= 500)
                            placeBiomeBlob(x, y, mapMap[x, y].getNewBiome(biomeRandomizer, biomeids, 1));
                        if ((randoevil + mapMap[x, y].modEvilness) >= 450) 
                            placeBiomeBlob(x, y, mapMap[x, y].getNewBiome(biomeRandomizer, biomeids, 2));
                        else if ((randoevil + mapMap[x, y].modEvilness) <= -450)
                            placeBiomeBlob(x, y, mapMap[x, y].getNewBiome(biomeRandomizer, biomeids, 3));
                }
            }

            //Neigh = Minimum number of neighbours required to stay the same.
            //Convc = Minimum number of neighbours to turn into that type of biome. (0 to disable)
            //IterRange = Range Around Co-ordiante to check.
            for (int i = 0; i < 3; i++)
                iterateBiomeMap(biomeRandomizer, biomeids, 4, 0, 3);
            for (int i = 0; i < 6; i++)
                iterateBiomeMap(biomeRandomizer, biomeids, 4, 0, 2);
            for (int i = 0; i < 3; i++)
                iterateBiomeMap(biomeRandomizer, biomeids, 6, 0, 2);
            for (int i = 0; i < 5 ; i++)
                iterateBiomeMap(biomeRandomizer, biomeids, 4, 0, 1);

            //DEBUG DRAWING
            drawBiomeMap(mapMap);

            Console.WriteLine("Calculated biome map sized {0}x{1}", mapMap.GetUpperBound(0), mapMap.GetUpperBound(1));
        }

        public void placeBiomeBlob(int x, int y, int biome)
        {
            for (int iy = y - 2; iy <= y + 2; iy++)
                for (int ix = x - 2; ix <= x + 2; ix++)
                    if ( !(((iy < 0) || (iy >= mapMap.GetUpperBound(0))) || ((ix < 0) || (ix >= mapMap.GetUpperBound(1)))) )
                        biomeMap[ix, iy] = biome;
        }

        public void iterateBiomeMap(Random rand, BiomeID biomeids, int neigh, int conv, int iterRange)
		{
		//Cellular Automata Stuff
        //To avoid blending looking like it was done form top right to bottom left...
        ///I split up when each pixel is done.

            for (int y = 0; y < mapMap.GetUpperBound(0); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(1); x++)
                {
                    if (x % 3 == 0 )
                    {
                        mapMap[x, y].setBiome(neighCalc(x, y, neigh, conv, iterRange, rand));
                        biomeMap[x, y] = mapMap[x, y].getBiome();
                    }
                }
            }
            for (int y = 0; y < mapMap.GetUpperBound(0); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(1); x++)
                {
                    if (x % 3 == 1)
                    {
                        mapMap[x, y].setBiome(neighCalc(x, y, neigh, conv, iterRange, rand));
                        biomeMap[x, y] = mapMap[x, y].getBiome();
                    }
                }
            }
            for (int y = 0; y < mapMap.GetUpperBound(0); y++)
            {
                for (int x = 0; x < mapMap.GetUpperBound(1); x++)
                {
                    if (x % 3 == 2)
                    {
                        mapMap[x, y].setBiome(neighCalc(x, y, neigh, conv, iterRange, rand));
                        biomeMap[x, y] = mapMap[x, y].getBiome();
                    }
                }
            }
		}

        public int neighCalc(int x, int y, int neigh, int conv, int iR, Random rand)
        {
            int biomeWinner = 0;    //Output biome.
            int orthadj = 0;        //Number of neighbours that are similar / the same.
            int[] countBiomeNeigh = new int[256]; //Only 255 different biomes possible.
            for (int iy = y - iR; iy <= y + iR; iy++)
            {
                for (int ix = x - iR; ix <= x + iR; ix++)
                {
                    //If different biome color on original map, treat as a similar neighbour.
                    //The border it'll more likely turn into Deep Ocean Biomes.
                    if (((iy < 0) || (iy >= mapMap.GetUpperBound(0))) || ((ix < 0) || (ix >= mapMap.GetUpperBound(1))))
                    {
                        orthadj -= 1;
                        countBiomeNeigh[BiomeID.DeepOcean] += 2;
                    }
                    else
                    { //If biome is within bounds...
                        countBiomeNeigh[mapMap[ix, iy].getBiome()]++;
                        if (mapMap[ix, iy].getBiome() == mapMap[x, y].getBiome())
                            orthadj += 1;
                        //else if (mapMap[ix, iy].getBiome() != mapMap[x, y].getBiome())
                        //    if (mapMap[ix, iy].getColor() != mapMap[x, y].getColor())
                        //        orthadj += 1;
                    }
                }
            }
            biomeWinner = mapMap[x, y].getBiome();
            //Oh dear, it doesn't have enough neighbours to stay the same...
            if (orthadj < neigh)
            {
                int temp = -1;
                for (int i = 0; i < 256; i++)
                {
                    if (countBiomeNeigh[i] > temp && countBiomeNeigh[i] > conv)
                    {
                        temp = countBiomeNeigh[i];
                        biomeWinner = i;
                    }
                } //Wooo, we have a winner for the new biome.
                
            }
            return biomeWinner;
        }

        ///*DRAW BIOMEMAP TO IMAGE FOR DEBUGGING
        public void drawBiomeMap(BiomeConversion[,] mopMap)
        {
            Bitmap drawnBiomeMap = new Bitmap(mopMap.GetUpperBound(0), mopMap.GetUpperBound(1));
            Color tempColor = new Color();
            for (int y = 0; y < mopMap.GetUpperBound(0); y++)
            {
                for (int x = 0; x < mopMap.GetUpperBound(1); x++)
                {
                    tempColor = biomeToColor(mopMap[x, y].getBiome());
                    drawnBiomeMap.SetPixel(x, y, tempColor);
                }
            }
            drawnBiomeMap.Save("DEBUG1.bmp");
        }

        public Color biomeToColor(int biome)
        {
            biome = (biome * biome * 7) % 256;
            int cred = 0, cgreen = 0, cblue = 0;
            //Messy byte-to-color-stuff.
            if (biome < 40)
            {//25 REDS
                cred = (biome + 11) * 5;
            }
            else if (biome >= 40 && biome < 80)
            {//25 GREENS
                cgreen = (biome - 29) * 5;
            }
            else if (biome >= 80 && biome < 120)
            {//25 BLUES
                cblue = (biome - 69) * 5;
            }
            else if (biome >= 120 && biome < 150)
            {//25 YELLOWS
                cred = (biome - 99) * 5;
                cgreen = (biome - 99) * 5;
            }
            else if (biome >= 150 && biome < 180)
            {//25 CYANS
                cgreen = (biome - 129) * 5;
                cblue = (biome - 129) * 5;
            }
            else if (biome >= 180 && biome < 210)
            {//25 PURPLES
                cred = (biome - 159) * 5;
                cblue = (biome - 159) * 5;
            }
            else if (biome >= 210 && biome < 255)
            {//25 GREYS
                cred = (biome - 204) * 5;
                cgreen = (biome - 204) * 5;
                cblue = (biome - 204) * 5;
            }
            return Color.FromArgb(cred, cgreen, cblue);
        }
        //*/

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
