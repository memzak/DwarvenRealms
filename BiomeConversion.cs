using Substrate;
using System;
using System.Drawing;

namespace DwarvenRealms
{
    class BiomeConversion
    {
        public int modTemperature = 0;
        public int modVegetation = 0;
        public int modVolcanism = 0;
        public int modEvilness = 0;
        public bool modRiver = false;
        //Somewhat important stuff.
        private Color colorKey;
        private int minecraftBiomeType;
        private int minecraftBiome = 255;
        //Layers of stuff.
        int[] layer1ID;
        int layer1Thickness;
        int[] layer2ID;
        int layer2Thickness;
        int[] layer3ID;
        int layer3Thickness;
        int[] layer4ID;

        public BiomeConversion()
        {
            colorKey = Color.FromArgb(0, 0, 0);
            minecraftBiomeType = BiomeID.WaterType;
            minecraftBiome = BiomeID.DeepOcean;
        }
        public BiomeConversion(Color color, int biometype)
        {
            colorKey = color;
            minecraftBiomeType = biometype;
        }
        public BiomeConversion(int red, int green, int blue, int biometype)
        {
            colorKey = Color.FromArgb(red, green, blue);
            minecraftBiomeType = biometype;
        }
        public BiomeConversion(int red, int green, int blue, int biometype, int temperatureMod, int vegetationMod, int volcanismMod, int evilnessMod, bool river = false)
        {
            colorKey = Color.FromArgb(red, green, blue);
            minecraftBiomeType = biometype;
            modTemperature += temperatureMod;
            modVegetation += vegetationMod;
            modVolcanism += volcanismMod;
            modEvilness += evilnessMod;
            modRiver = river;

            if (isRiver())
            {
                layer1ID = new int[] { BlockType.GRAVEL, BlockType.SAND, BlockType.DIRT, BlockType.DIRT, BlockType.CLAY_BLOCK };
                layer1Thickness = 1;
                layer2ID = new int[] { BlockType.DIRT };
                layer2Thickness = 3;
            }
        }

        public BiomeConversion(int red, int green, int blue, int biometype, int temperatureMod, int vegetationMod, int volcanismMod, int evilnessMod,
            int[] lay1ID = null, int lay1thick = 0,
            int[] lay2ID = null, int lay2thick = 0,
            int[] lay3ID = null, int lay3thick = 0,
            int[] lay4ID = null)
        {
            colorKey = Color.FromArgb(red, green, blue);
            minecraftBiomeType = biometype;
            modTemperature += temperatureMod;
            modVegetation += vegetationMod;
            modVolcanism += volcanismMod;
            modEvilness += evilnessMod;

            if (lay1ID != null)
                layer1ID = lay1ID; 
            else layer1ID = new int[] { 1 };
            layer1Thickness = lay1thick;
            if (lay2ID != null)
                layer2ID = lay2ID;
            else layer2ID = new int[] { 1 };
            layer2Thickness = lay1thick + lay2thick;
            if (lay3ID != null)
                layer3ID = lay3ID;
            else layer3ID = new int[] { 1 };
            layer3Thickness = lay1thick + lay2thick + lay3thick;
            if (lay4ID != null)
                layer4ID = lay4ID;
            else layer4ID = new int[] { 1 };
        
        }

        //Biome Gets n' Sets
        public void setBiome(int biome)
        {
            minecraftBiome = biome;
        }
        public int getBiome()
        {
            return minecraftBiome;
        }
        public int getNewBiome(Random rand, BiomeID biomeids, int biomeGroup = 0)
        {
            //More Stuff'll go here later.
            int randotemp = (rand.Next(0, 1000) - 500);

            if (isRiver())
            {//Are you a Lake?
                if ((randotemp + modTemperature) <= -400)
                    minecraftBiome = BiomeID.FrozenRiver;
                else
                {
                    if (minecraftBiomeType == BiomeID.SwampType || minecraftBiomeType == BiomeID.JungleType)
                        minecraftBiome = BiomeID.LushRiver;
                    else if (minecraftBiomeType == BiomeID.DesertType)
                        minecraftBiome = BiomeID.DryRiver;
                    else
                        minecraftBiome = BiomeID.River;
                }
            }
            else
            {//Oh, nevermind, you are a biome?
                //Let's get some random going...
                int randoveg = (rand.Next(0, 1000) - 500);

                if ((randotemp + modTemperature) >= 400)
                {//Hot Biome Groups
                    if ((randoveg + modVegetation) >= 400)
                        minecraftBiome = biomeids.BGroupHotHV[minecraftBiomeType][rand.Next(0, biomeids.BGroupHotHV[minecraftBiomeType].Length)];
                    else if ((randoveg + modVegetation) <= -400)
                        minecraftBiome = biomeids.BGroupHotLV[minecraftBiomeType][rand.Next(0, biomeids.BGroupHotLV[minecraftBiomeType].Length)];
                    else
                        minecraftBiome = biomeids.BGroupHot[minecraftBiomeType][rand.Next(0, biomeids.BGroupHot[minecraftBiomeType].Length)];
                }
                else if ((randotemp + modTemperature) <= -400)
                {//Cold Biome Groups
                    if ((randoveg + modVegetation) >= 400)
                        minecraftBiome = biomeids.BGroupColHV[minecraftBiomeType][rand.Next(0, biomeids.BGroupColHV[minecraftBiomeType].Length)];
                    else if ((randoveg + modVegetation) <= -400)
                        minecraftBiome = biomeids.BGroupColLV[minecraftBiomeType][rand.Next(0, biomeids.BGroupColLV[minecraftBiomeType].Length)];
                    else
                        minecraftBiome = biomeids.BGroupCol[minecraftBiomeType][rand.Next(0, biomeids.BGroupCol[minecraftBiomeType].Length)];
                }
                else
                {//Medium Biome Groups
                    if ((randoveg + modVegetation) >= 400)
                        minecraftBiome = biomeids.BGroupMedHV[minecraftBiomeType][rand.Next(0, biomeids.BGroupMedHV[minecraftBiomeType].Length)];
                    else if ((randoveg + modVegetation) <= -400)
                        minecraftBiome = biomeids.BGroupMedLV[minecraftBiomeType][rand.Next(0, biomeids.BGroupMedLV[minecraftBiomeType].Length)];
                    else
                        minecraftBiome = biomeids.BGroupMed[minecraftBiomeType][rand.Next(0, biomeids.BGroupMed[minecraftBiomeType].Length)];
                }   
                //This stuff checked outisde of this class for 3x3 blobs.
                //Volcanoes Take Preference over other stuff.
                if (biomeGroup == 1)
                    minecraftBiome = biomeids.BGroupVolcano[rand.Next(0, biomeids.BGroupVolcano.Length)];
                //Good / Evil Takes Preference over everything else.
                else if (biomeGroup == 2)
                {
                    minecraftBiome = biomeids.BGroupGood[minecraftBiomeType][rand.Next(0, biomeids.BGroupGood[minecraftBiomeType].Length)];
                }
                else if (biomeGroup == 3)
                {
                    minecraftBiome = biomeids.BGroupEvil[rand.Next(0, biomeids.BGroupEvil.Length)];
                }

            }

            return minecraftBiome;
        }
        public int getRandomBiome(Random rand, BiomeID biomeids)
        {//For TRUELY random biome selection.
            int a = rand.Next(0, biomeids.BGroupALL.Length);
            int b = rand.Next(0, biomeids.BGroupALL[a].Length);
            int c = rand.Next(0, biomeids.BGroupALL[a][b].Length);

            minecraftBiome = biomeids.BGroupALL[a][b][c];
            return biomeids.BGroupALL[a][b][c];
        }
        //Ensuring special changes are made to layers once biome has been selected.
        public void setNewLayers()
        {
            //I'll get around to finishing this, eventually.
            if (minecraftBiome == BiomeID.Volcano)
            {
                layer1ID = new int[] { BlockType.STONE, BlockType.STONE, BlockType.STONE, BlockType.OBSIDIAN };
                layer1Thickness = 3;
                layer2ID = new int[] { BlockType.STONE, BlockType.OBSIDIAN };
                layer2Thickness = 3;
            }
            else if (minecraftBiome == BiomeID.Crag)
            {
                layer1ID = new int[] { BlockType.STONE, BlockType.STONE, BlockType.COBBLESTONE, BlockType.COBBLESTONE };
                layer1Thickness = 3;
            }
            else if (minecraftBiome == BiomeID.MushroomIsland || minecraftBiome == BiomeID.MushroomIslandShore)
            {
                layer1ID = new int[] { BlockType.MYCELIUM };
            }
            else if (minecraftBiome == BiomeID.FungiForest)
            {
                layer1ID = new int[] { BlockType.GRASS, BlockType.GRASS, BlockType.GRASS, BlockType.MYCELIUM };
            }
            else if (minecraftBiome == BiomeID.Mesa || minecraftBiome == BiomeID.MesaBryce)
            {
                layer1ID = new int[] { BlockType.STAINED_CLAY };
                layer1Thickness = 2;
                layer2ID = new int[] { BlockType.STAINED_CLAY };
                layer2Thickness = 3;
                layer3ID = new int[] { BlockType.STAINED_CLAY };
                layer3Thickness = 5;
            }            
        }


        //Some basic Gets n' Sets
        public bool isRiver()
        {
            return modRiver;
        }
        public Color getColor()
        {
            return colorKey;
        }
        public int getBiomeType()
        {
            return minecraftBiomeType;
        }

        public void setRiver(bool river)
        {
            modRiver = river;
        }
        public void setColor(Color col)
        {
            colorKey = col;
        }
        public void setColor(int red, int green, int blue)
        {
            colorKey = Color.FromArgb(red, green, blue);
        }
        public void setBiomeType(int type)
        {
            if (type >= 0 && type < 7)
                minecraftBiomeType = type;
            else //Because why not, this seems like a fun idea.
                minecraftBiomeType = type % 7;
        }

        //Class manipulations.
        public bool colMatches(Color a)
        {
            return a.R == colorKey.R && a.G == colorKey.G && a.B == colorKey.B;
        }
        public void copy(BiomeConversion copy)
        {
            setRiver(copy.isRiver());
            setColor(copy.getColor());
            setBiome(copy.getBiome());
            setBiomeType(copy.getBiomeType());

            modTemperature = copy.modTemperature;
            modVegetation = copy.modVegetation;
            modVolcanism = copy.modVolcanism;
            modEvilness = copy.modEvilness;

            layer1ID = copy.layer1ID;
            layer1Thickness = copy.layer1Thickness;
            layer2ID = copy.layer2ID;
            layer2Thickness = copy.layer2Thickness;
            layer3ID = copy.layer3ID;
            layer3Thickness = copy.layer3Thickness;
            layer4ID = copy.layer4ID;
        }
        
        //Layer Stuff
        public int getBlockID(int depth, float x = 0, float y = 0)
        {
            x /= 8.0f;
            y /= 8.0f;
            if (depth <= layer1Thickness)
            {
                if (layer1ID.Length == 1)
                    return layer1ID[0];
                double rando = SimplexNoise.Noise.Generate(x, y, depth);
                rando += 1;
                rando /= 2;
                rando *= layer1ID.Length;
                return layer1ID[(int)Math.Floor(rando)];
            }
            else if (depth <= layer2Thickness)
            {
                if (layer2ID.Length == 1)
                    return layer2ID[0];
                double rando = SimplexNoise.Noise.Generate(x, y, depth);
                rando += 1;
                rando /= 2;
                rando *= layer2ID.Length;
                return layer2ID[(int)Math.Floor(rando)];
            }
            else if (depth <= layer3Thickness)
            {
                if (layer3ID.Length == 1)
                    return layer3ID[0];
                double rando = SimplexNoise.Noise.Generate(x, y, depth);
                rando += 1;
                rando /= 2;
                rando *= layer3ID.Length;
                return layer3ID[(int)Math.Floor(rando)];
            }
            else
            {
                if (layer4ID.Length == 1)
                    return layer4ID[0];
                double rando = SimplexNoise.Noise.Generate(x, y, depth);
                rando += 1;
                rando /= 2;
                rando *= layer4ID.Length;
                return layer4ID[(int)Math.Floor(rando)];
            }
        }
    }
}
