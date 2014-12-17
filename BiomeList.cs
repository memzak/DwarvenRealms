﻿using Substrate;
using System.Drawing;

namespace DwarvenRealms
{
    static class BiomeList
    {
        public const int brook = 7;
        public const int stream = 7;
        public const int minorRiver = 7;
        public const int river = 7;
        public const int majorRiver = 7;
        public const int riverOcean = 7;

        public static BiomeConversion[] biomes = 
        {
            new BiomeConversion(0,0,0,255),                         // no biome
            new BiomeConversion(128,128,128,BiomeID.ExtremeHillsM, new int[]{BlockType.GRASS, BlockType.STONE, BlockType.STONE}, 1),    // mountain
            new BiomeConversion(0,224,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),      // temperate freshwater lake
            new BiomeConversion(0,192,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),      // temperate brackish lake
            new BiomeConversion(0,160,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),      // temperate saltwater lake
            new BiomeConversion(0,96,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),       // tropical freshwater lake
            new BiomeConversion(0,64,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),       // tropical brackish lake
            new BiomeConversion(0,32,255,BiomeID.Ocean, new int[]{BlockType.DIRT}, 4),       // tropical saltwater lake
            new BiomeConversion(0,255,255,BiomeID.FrozenOcean, new int[]{BlockType.DIRT}, 2),      // arctic ocean
            new BiomeConversion(0,0,255,BiomeID.DeepOcean, new int[]{BlockType.DIRT}, 2),        // tropical ocean
            new BiomeConversion(0,128,255,BiomeID.DeepOcean, new int[]{BlockType.DIRT}, 2),      // temperate ocean ()
            new BiomeConversion(64,255,255,BiomeID.IcePlains, new int[]{BlockType.SNOW_BLOCK, BlockType.SNOW_BLOCK, BlockType.SNOW_BLOCK, BlockType.ICE}, 1, new int[]{BlockType.ICE}, 2, new int[]{BlockType.ICE, BlockType.GRAVEL}, 1),     // glacier ()
            new BiomeConversion(128,255,255,BiomeID.IcePlains, new int[]{BlockType.SNOW_BLOCK}, 2, new int[]{BlockType.SNOW_BLOCK, BlockType.DIRT, BlockType.GRAVEL}, 1, new int[]{BlockType.DIRT, BlockType.GRAVEL, BlockType.ICE}, 1),    // tundra ()
            new BiomeConversion(96,192,128,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),     // temperate freshwater swamp ()
            new BiomeConversion(64,192,128,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),     // temperate saltwater swamp ()
            new BiomeConversion(96,255,128,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),     // temperate freshwater marsh ()
            new BiomeConversion(64,255,128,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),     // temperate saltwater marsh ()
            new BiomeConversion(96,192,64,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),      // tropical freshwater swamp ()
            new BiomeConversion(64,192,64,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),      // tropical saltwater swamp ()
            new BiomeConversion(64,255,96,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),      // mangrove swamp ()
            new BiomeConversion(96,255,64,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),      // tropical freshwater marsh ()
            new BiomeConversion(64,255,64,BiomeID.Swampland, new int[]{BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.WATER}, 1, new int[]{BlockType.DIRT}, 3),      // tropical saltwater marsh ()
            new BiomeConversion(0,96,64,BiomeID.ColdTaiga, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),        // taiga forest ()
            new BiomeConversion(0,96,32,BiomeID.Taiga, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),        // temperate conifer forest ()
            new BiomeConversion(0,160,32,BiomeID.RoofedForest, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),       // temperate broadleaf forest ()
            new BiomeConversion(0,96,0,BiomeID.BirchForestM, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),         // tropical conifer forest ()
            new BiomeConversion(0,128,0,BiomeID.FlowerForest, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),        // tropical dry broadleaf forest ()
            new BiomeConversion(0,160,0,BiomeID.Jungle, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),        // tropical moist broadleaf forest ()
            new BiomeConversion(0,255,32,BiomeID.Plains, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 5),       // temperate grassland ()
            new BiomeConversion(0,224,32,BiomeID.Savanna, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),       // temperate savanna ()
            new BiomeConversion(0,192,32,BiomeID.SunflowerPlains, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),       // temperate shrubland ()
            new BiomeConversion(255,160,0,BiomeID.Plains, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),      // tropical grassland ()
            new BiomeConversion(255,176,0,BiomeID.Savanna, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),      // tropical savanna ()
            new BiomeConversion(255,192,0,BiomeID.JungleEdge, new int[]{BlockType.GRASS}, 1, new int[]{BlockType.DIRT}, 3),      // tropical shrubland ()
            new BiomeConversion(255,96,32,BiomeID.MesaPlateau, new int[]{BlockType.SAND, BlockType.DIRT, BlockType.DIRT, BlockType.STAINED_CLAY}, 3, new int[]{BlockType.SANDSTONE}),      // badland desert ()
            new BiomeConversion(255,255,0,BiomeID.Desert, new int[]{BlockType.SAND}, 3, new int[]{BlockType.SANDSTONE}, 1),      // sand desert ()
            new BiomeConversion(255,128,64,BiomeID.Desert, new int[]{BlockType.SAND, BlockType.GRAVEL, BlockType.GRAVEL, BlockType.STONE}, 2), // rock desert ()
        };

        public static int GetBiomeIndex(Color input)
        {
            for (int i = 0; i < biomes.Length; i++)
            {
                if (biomes[i].matches(input))
                    return i;
            }
            return 0;
        }
		
		//For DEBUGing Cellular Automata
		public static int GetRandomBiome()
		{
			Random rand = new Random();
			int a = rand.Next(0, BGroupALL.Length);
			int b = rand.Next(0, BGroupALL[a].Length);
			int c = rand.Next(0, BGroupALL[a][b].Length);
			
			return BGroupALL[a][b][c];
		}
    }
}
