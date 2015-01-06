//TODO: Get BiomeIDs from settings file for modded biomes. (instead of hardcoded)
//		Allow for 'toggleable' modded biomes if mod isn't insalled.
//		Rework how biome properties are dealt with. (temperature, vegetation, good/evil)
//		Allow for biome properties to be customized from settings file.
namespace DwarvenRealms
{
    public class BiomeID
    {
		
		//Vanilla Biome IDs
        public const int Ocean = 0;
        public const int Plains = 1;
        public const int Desert = 2;
        public const int ExtremeHills = 3;
        public const int Forest = 4;
        public const int Taiga = 5;
        public const int Swampland = 6;
        public const int River = 7;
        public const int Hell = 8;
        public const int Sky = 9;
        public const int FrozenOcean = 10;
        public const int FrozenRiver = 11;
        public const int IcePlains = 12;
        public const int IceMountains = 13;
        public const int MushroomIsland = 14;
        public const int MushroomIslandShore = 15;
        public const int Beach = 16;
        public const int DesertHills = 17;
        public const int ForestHills = 18;
        public const int TaigaHills = 19;
        public const int ExtremeHillsEdge = 20;
        public const int Jungle = 21;
        public const int JungleHills = 22;
        public const int JungleEdge = 23;
        public const int DeepOcean = 24;
        public const int StoneBeach = 25;
        public const int ColdBeach = 26;
        public const int BirchForest = 27;
        public const int BirchForestHills = 28;
        public const int RoofedForest = 29;
        public const int ColdTaiga = 30;
        public const int ColdTaigaHills = 31;
        public const int MegaTaiga = 32;
        public const int MegaTaigaHills = 33;
        public const int ExtremeHillsP = 34;
        public const int Savanna = 35;
        public const int SavannaPlateau = 36;
        public const int Mesa = 37;
        public const int MesaPlateauF = 38;
        public const int MesaPlateau = 39;
        public const int SunflowerPlains = 129;
        public const int DesertM = 131;
        public const int ExtremeHillsM = 131;
        public const int FlowerForest = 132;
        public const int TaigaM = 133;
        public const int SwamplandM = 134;
        public const int IcePlainsSpikes = 140;
        public const int JungleM = 149;
        public const int JungleEdgeM = 151;
        public const int BirchForestM = 155;
        public const int RoofedForestM = 157;
        public const int ColdTaigaM = 158;
        public const int MegaSpruceTaiga = 160;
        public const int MegaSpruceTaigaHills = 161;
        public const int ExtremeHillsPM = 162;
        public const int SavannaM = 163;
        public const int SavannaPlateauM = 164;
        public const int MesaBryce = 165;
        public const int MesaPlateauFM = 166;
        public const int MesaPlateauM = 167;
	
		//MODDED BIOMES AREN'T CONST 
		//They'll probably change to -1 when I properly set up toggleable mods/modded biomes.
		
		//Biomes O' Plenty BiomeIDs
        public const int Alps = 41;//Settings.Default.Alps;
        public const int AlpsForest = 118;
        public const int Arctic = 42;
        public const int BambooForest = 43;
        public const int Bayou = 44;
        public const int Bog = 45;
        public const int BorealForest = 46;
        public const int Brushland = 47;
        public const int Canyon = 48;
        public const int Chaparral = 49;
        public const int CherryBlossomGrove = 50;
        public const int ConiferousForest = 51;
        public const int ConiferousForestSnow = 52;
        public const int Crag = 53;
        public const int DeadForest = 54;
        public const int DeadSwamp = 55;
        public const int DeciduousForest = 56;
        public const int Fen = 57;
        public const int FlowerField = 58;
        public const int FrostForest = 59;
        public const int FungiForest = 60;
        public const int Garden = 61;
        public const int Glacier = 102;
        public const int Grassland = 62;
        public const int Grove = 63;
        public const int Heathland = 64;
        public const int Highland = 65;
        public const int JadeCliffs = 66;
        public const int LavenderFields = 67;
        public const int LushDesert = 68;
        public const int LushSwamp = 69;
        public const int Mangrove = 99;
        public const int MapleWoods = 70;
        public const int Marsh = 71;
        public const int Meadow = 72;
        public const int MeadowForest = 103;
        public const int Moor = 73;
        public const int Mountain = 74;
        public const int MysticGrove = 75;
        public const int Oasis = 104;
        public const int OminousWoods = 76;
        public const int Orchard = 105;
        public const int OriginValley = 77;
        public const int Outback = 78;
        public const int Prairie = 79;
        public const int Quagmire = 106;
        public const int Rainforest = 80;
        public const int RedwoodForest = 81;
        public const int SacredSprings = 82;
        public const int Scrubland = 107;
        public const int SeasonalForest = 83;
        public const int Shield = 84;
        public const int Shrubland = 85;
        public const int Silkglades = 108;
        public const int Sludgepit = 86;
        public const int SpruceWoods = 109;
        public const int Steppe = 87;
        public const int TemperateRainforest = 88;
        public const int Thicket = 89;
        public const int TropicalRainforest = 90;
        public const int Tropics = 97;
        public const int Tundra = 91;
        public const int Volcano = 98;
        public const int Wasteland = 92;
        public const int Wetland = 93;
        public const int Woodland = 94;
        public const int CoralReef = 95;
        public const int KelpForest = 96;
        public const int LushRiver = 115;
        public const int DryRiver = 116;
		
		//Thaumcraft BiomeIDs
        public const int Eerie = 194;
        public const int Eldritch = 195;
        public const int MagicalForest = 193;
        public const int Taint = 192;
		
		//Buildcraft BiomeIDs
        public const int OceanOilField = 126;
        public const int DesertOilField = 127;
		
		//Growthcraft BiomeIDs
        public const int BambooForestGrowthcraft = 170;
		
        //Types of Biomes IDs (for later arrays)
		public const int WaterType = 0;
		public const int PlainsType = 1;
		public const int ForestType = 2;
		public const int JungleType = 3;
		public const int DesertType = 4;
		public const int MountainType = 5;
		public const int SwampType = 6;

		//BiomeGroups based on type. TODO: Add vanilla biomes into the mix.
		//Index of Jagged array biome group based on BiomeType variables above.

		//Regular Biome Variants
        public int[][] BGroupCol;
        public int[][] BGroupMed;
        public int[][] BGroupHot;
        //Low Vegetation Biome Variants
        public int[][] BGroupColLV;
        public int[][] BGroupMedLV;
        public int[][] BGroupHotLV;
        //High Vegetation Biome Variants
        public int[][] BGroupColHV;
        public int[][] BGroupMedHV;
        public int[][] BGroupHotHV;
        //Other Variants
        public int[][] BGroupGood;
        public int[] BGroupEvil;
		public int[] BGroupVolcano;
		//Array O' Everything
		public int[][][] BGroupALL;

        public void getBiomeIDs()
        {
            //Regular Biome Variants
            BGroupCol = new int[7][]
            {
                new int[1] {FrozenOcean},
		        new int[4] {Arctic,Arctic,Tundra,IcePlains},
		        new int[4] {FrostForest,SpruceWoods,Taiga,Taiga},
		        new int[1] {Taiga},
		        new int[2] {Arctic,Glacier},
		        new int[2] {Alps,Glacier},
		        new int[1] {Wetland}
            };

            BGroupMed = new int[7][]
            {
                new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField},
                new int[13] {Chaparral,Chaparral,FlowerField,Grassland,Meadow,Meadow,Moor,Orchard,Prairie,Scrubland,Shrubland,Thicket,Plains},
		        new int[13] {BorealForest,CherryBlossomGrove,Grove,MapleWoods,MeadowForest,MysticGrove,RedwoodForest,SeasonalForest,SpruceWoods,Woodland,Forest,BirchForest,RoofedForest},
		        new int[5] {BambooForest,Rainforest,TemperateRainforest,TropicalRainforest,Jungle},
		        new int[5] {Outback,Steppe,Desert,Desert,DesertOilField},
		        new int[6] {Crag,Highland,Mountain,ExtremeHills,ExtremeHillsP,ExtremeHillsM},
		        new int[4] {Bayou,Bog,Wetland,Swampland}
            };

            BGroupHot = new int[7][]
            {
                new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField},
                new int[7] {Brushland,Heathland,Prairie,Scrubland,Shrubland,Thicket,Plains},
		        new int[5] {BorealForest,Fen,RedwoodForest,SeasonalForest,Woodland},
		        new int[5] {BambooForest,Rainforest,TemperateRainforest,TropicalRainforest,Jungle},
		        new int[5] {Outback,Outback,Desert,Desert,DesertOilField},
		        new int[3] {Canyon,Crag,Highland},
		        new int[5] {Bayou,Marsh,Sludgepit,Wetland,Swampland}
            };
		
		    //Low Vegetation Biome Variants
            BGroupColLV = new int[7][]
            {
                new int[1] {FrozenOcean},
                new int[2] {Arctic,Arctic},
		        new int[3] {FrostForest,Shield,Taiga},
		        new int[1] {FrostForest},
		        new int[3] {Arctic,Glacier,ColdBeach},
		        new int[2] {Crag,Glacier},
		        new int[1] {Quagmire}
            };

            BGroupMedLV = new int[7][]
            {
                new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField},
                new int[6] {Grassland,Highland,Shrubland,Steppe,Plains,Savanna},
		        new int[6] {DeadForest,Grove,MapleWoods,MeadowForest,Orchard,Shield},
		        new int[4] {Rainforest,RedwoodForest,SeasonalForest,Tropics},
		        new int[3] {Desert,Desert,DesertOilField},
		        new int[4] {Crag,Wasteland,ExtremeHillsM,ExtremeHillsPM},
		        new int[3] {DeadSwamp,Marsh,Quagmire}
            };

            BGroupHotLV = new int[7][]
            {
                new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField},
                new int[6] {Grassland,Highland,Moor,Shrubland,Steppe,Savanna},
		        new int[4] {DeadForest,Orchard,SeasonalForest,Shield},
		        new int[2] {RedwoodForest,Tropics},
		        new int[4] {Wasteland,Wasteland,Desert,DesertOilField},
		        new int[4] {Canyon,Wasteland,Mesa,MesaBryce},
		        new int[3] {DeadSwamp,Marsh,Quagmire}
            };

            //High Vegetation Biome Variants
            BGroupColHV = new int[7][]
            {
                new int[1] {FrozenOcean},
                new int[2] {Tundra,IcePlainsSpikes},
		        new int[4] {ConiferousForestSnow,SpruceWoods,ColdTaiga,Taiga},
		        new int[3] {SacredSprings,MegaTaiga,MegaSpruceTaiga},
		        new int[1] {Glacier},
		        new int[1] {ColdTaigaM},
		        new int[1] {Wetland}
            };

            BGroupMedHV = new int[7][]
            {
                new int[6] {Ocean,CoralReef,CoralReef,KelpForest,KelpForest,OceanOilField},
                new int[7] {Brushland,Garden,Heathland,LavenderFields,LavenderFields,Thicket,SunflowerPlains},
		        new int[12] {BambooForestGrowthcraft,BorealForest,ConiferousForest,DeciduousForest,JadeCliffs,RedwoodForest,SeasonalForest,SpruceWoods,Woodland,FlowerForest,BirchForestM,RoofedForestM},
		        new int[7] {BambooForestGrowthcraft,BambooForest,FungiForest,SacredSprings,TemperateRainforest,TropicalRainforest,Jungle},
		        new int[3] {LushDesert,Mangrove,Oasis},
		        new int[7] {Highland,Highland,Mountain,Mountain,SacredSprings,Shield,ExtremeHills},
		        new int[6] {Bog,LushSwamp,LushSwamp,Silkglades,Sludgepit,SwamplandM}
            };

            BGroupHotHV = new int[7][]
            {
                new int[6] {Ocean,CoralReef,CoralReef,KelpForest,KelpForest,OceanOilField},
                new int[5] {Brushland,Heathland,Prairie,Scrubland,Thicket},
		        new int[8] {BambooForestGrowthcraft,BorealForest,DeciduousForest,Fen,FungiForest,JadeCliffs,RedwoodForest,SeasonalForest},
		        new int[7] {BambooForestGrowthcraft,BambooForest,FungiForest,TemperateRainforest,TropicalRainforest,Tropics,Jungle},
		        new int[4] {LushDesert,Mangrove,Oasis,Oasis},
		        new int[4] {Canyon,Highland,Mountain,ExtremeHills},
		        new int[5] {Bog,LushSwamp,Silkglades,Sludgepit,SwamplandM}
            };
		
            //Other Biome Variants
            BGroupGood = new int[7][]
            {
                new int[3] {CoralReef,KelpForest,OceanOilField},
                new int[4] {Garden,OriginValley,FlowerField,MushroomIsland},
		        new int[4] {MysticGrove,SacredSprings,MagicalForest,FlowerForest},
		        new int[2] {MagicalForest,Tropics},
		        new int[2] {Oasis,MushroomIsland},
		        new int[1] {SacredSprings},
		        new int[2] {Oasis,Tropics}
            };
		
            BGroupEvil = new int[4] 
            {
                OminousWoods, Eerie, Taint, Silkglades
            };

		    BGroupVolcano = new int [1]    
            {
                Volcano
            };
		
		    //Array O' Everything
		    BGroupALL = new int[10][][]
            {
                BGroupCol, BGroupMed, BGroupHot, BGroupColLV, BGroupMedLV, BGroupHotLV, BGroupColHV, BGroupMedHV, BGroupHotHV, BGroupGood
            };
		
        }
		//Rules to remember:
		//Volcano if high volcanism.
		//Good / Evil extremes overrides previous selection.
		//
		
    }
	
}
