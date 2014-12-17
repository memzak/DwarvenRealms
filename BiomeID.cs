//TODO: Get BiomeIDs from settings file for modded biomes. (instead of hardcoded)
//		Allow for 'toggleable' modded biomes if mod isn't insalled.
//		Rework how biome properties are dealt with. (temperature, vegetation, good/evil)
//		Allow for biome properties to be customized from settings file.
namespace DwarvenRealms
{
    public class BiomeID
    {
	
		//Types of Biomes IDs (for later arrays)
		public const int WaterType = 0;
		public const int PlainsType = 1;
		public const int ForestType = 2;
		public const int JungleType = 3;
		public const int DesertType = 4;
		public const int MountainType = 5;
		public const int SwampType = 6;
		
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
		public int Alps = 41;//Settings.Default.Alps;
		public int AlpsForest = 118;
		public int Arctic = 42;
		public int BambooForest = 43;
		public int Bayou = 44;
		public int Bog = 45;
		public int BorealForest = 46;
		public int Brushland = 47;
		public int Canyon = 48;		
		public int Chaparral = 49;
		public int CherryBlossomGrove = 50;
		public int ConiferousForest = 51;
		public int ConiferousForestSnow = 52;
		public int Crag = 53;
		public int DeadForest = 54;
		public int DeadSwamp = 55;
		public int DeciduousForest = 56;
		public int Fen = 57;
		public int FlowerField = 58;
		public int FrostForest = 59;
		public int FungiForest = 60;
		public int Garden = 61;
		public int Glacier = 102;
		public int Grassland = 62;
		public int Grove = 63;
		public int Heathland = 64;
		public int Highland = 65;
		public int JadeCliffs = 66;
		public int LavenderFields = 67;
		public int LushDesert = 68;
		public int LushSwamp = 69;
		public int Mangrove = 99;
		public int MapleWoods 70;
		public int Marsh = 71;
		public int Meadow = 72;
		public int MeadowForest = 103;
		public int Moor = 73;
		public int Mountain = 74;
		public int MysticGrove = 75;
		public int Oasis = 104;
		public int OminousWoods = 76;
		public int Orchard = 105;
		public int OriginValley = 77;
		public int Outback = 78;
		public int Prairie = 79;
		public int Quagmire = 106;
		public int Rainforest = 80;
		public int RedwoodForest = 81;
		public int SacredSprings = 82;
		public int Scrubland = 107;
		public int SeasonalForest = 83;
		public int Shield = 84;
		public int Shrubland = 85;
		public int Silkglades = 108;
		public int Sludgepit = 86;
		public int SpruceWoods = 109;
		public int Steppe = 87;
		public int TemperateRainforest = 88;
		public int Thicket = 89;
		public int TropicalRainforest = 90;
		public int Tropics = 97;
		public int Tundra = 91;
		public int Volcano = 98;
		public int = Wasteland = 92;
		public int Wetland = 93;
		public int Woodland = 94;
		public int CoralReef = 95;
		public int KelpForest = 96;
		public int LushRiver = 115;
		public cot int DryRiver = 116;
		
		//Thaumcraft BiomeIDs
		public int Eerie = 194;
		public int Eldritch = 195;
		public int MagicalForest = 193;
		public int Taint = 192;
		
		//Buildcraft BiomeIDs
		public int OceanOilField = 126;
		public int DesertOilField = 127;
		
		//Growthcraft BiomeIDs
		public int BambooForestGrowthcraft = 170;
		
		//BiomeGroups based on type. TODO: Add vanilla biomes into the mix.
		
		//Regular Biome Variants
		public const int[][] BGroupCol = new int[7][];
        BGroupCol[WaterType] = new int[1] {FrozenOcean};
		BGroupCol[PlainsType] = new int[3] {Arctic,Arctic,Tundra};
		BGroupCol[ForestType] = new int[4] {FrostForest,SpruceWoods,Taiga,Taiga};
		BGroupCol[JungleType] = new int[1] {Taiga};
		BGroupCol[DesertType] = new int[2] {Arctic,Glacier};
		BGroupCol[MountainType] = new int[2] {Alps,Glacier};
		BGroupCol[SwampType] = new int[1] {Wetland};
		
		public const int[][] BGroupMed = new int[7][];
        BGroupMed[WaterType] = new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField};
        BGroupMed[PlainsType] = new int[13] {Chaparral,Chaparral,FlowerField,Grassland,Meadow,Meadow,Moor,Orchard,Prairie,Scrubland,Shrubland,Thicket,Plains};
		BGroupMed[ForestType] = new int[12] {BorealForest,CherryBlossomGrove,Grove,MapleWoods,MeadowForest,MysticGrove,OvergrownGreens,RedwoodForest,SeasonalForest,SpruceWoods,Woodland,Forest};
		BGroupMed[JungleType] = new int[5] {BambooForest,Rainforest,TemperateRainforest,TropicalRainforest,Jungle};
		BGroupMed[DesertType] = new int[5] {Outback,Steppe,Desert,Desert,DesertOilField};
		BGroupMed[MountainType] = new int[4] {Crag,Highland,Mountain,ExtremeHills};
		BGroupMed[SwampType] = new int[4] {Bayou,Bog,Wetland,Swamp};
		
		public const int[][] BGroupHot = new int[7][];
        BGroupHot[WaterType] = new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField};
        BGroupHot[PlainsType] = new int[7] {Brushland,Heathland,Prairie,Scrubland,Shrubland,Thicket,Plains};
		BGroupHot[ForestType] = new int[5] {BorealForest,Fen,RedwoodForest,SeasonalForest,Woodland};
		BGroupHot[JungleType] = new int[5] {BambooForest,Rainforest,TemperateRainforest,TropicalRainforest,Jungle};
		BGroupHot[DesertType] = new int[5] {Outback,Outback,Desert,Desert,DesertOilField};
		BGroupHot[MountainType] = new int[3] {Canyon,Crag,Highland};
		BGroupHot[SwampType] = new int[5] {Bayou,Marsh,Sludgepit,Wetland,Swamp};
		
		//Low Vegetation Biome Variants
		public const int[][] BGroupColLV = new int[7][];
        BGroupColLV[WaterType] = new int[1] {FrozenOcean};
        BGroupColLV[PlainsType] = new int[2] {Arctic,Arctic};
		BGroupColLV[ForestType] = new int[3] {FrostForest,Shield,Taiga};
		BGroupColLV[JungleType] = new int[1] {FrostForest};
		BGroupColLV[DesertType] = new int[2] {Arctic,Glacier};
		BGroupColLV[MountainType] = new int[2] {Crag,Glacier};
		BGroupColLV[SwampType] = new int[1] {Quagmire};
		
		public const int[][] BGroupMedLV = new int[7][];
        BGroupMedLV[WaterType] = new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField};
        BGroupMedLV[PlainsType] = new int[6] {Field,Grassland,Highland,Shrubland,Steppe,Plains};
		BGroupMedLV[ForestType] = new int[7] {DeadForest,Grove,MapleWoods,MeadowForest,Orchard,OvergrownGreens,Shield};
		BGroupMedLV[JungleType] = new int[5] {OvergrownGreens,Rainforest,RedwoodForest,SeasonalForest,Tropics};
		BGroupMedLV[DesertType] = new int[3] {Desert,Desert,DesertOilField};
		BGroupMedLV[MountainType] = new int[2] {Crag,Wasteland};
		BGroupMedLV[SwampType] = new int[3] {DeadSwamp,Marsh,Quagmire};
		
		public const int[][] BGroupHotLV = new int[7][];
        BGroupHotLV[WaterType] = new int[5] {Ocean,Ocean,CoralReef,KelpForest,OceanOilField};
        BGroupHotLV[PlainsType] = new int[7] {Field,Grassland,Highland,Moor,Shrubland,Steppe,Plains};
		BGroupHotLV[ForestType] = new int[5] {DeadForest,Orchard,OvergrownGreens,SeasonalForest,Shield};
		BGroupHotLV[JungleType] = new int[2] {RedwoodForest,Tropics};
		BGroupHotLV[DesertType] = new int[4] {Wasteland,Wasteland,Desert,DesertOilField};
		BGroupHotLV[MountainType] = new int[3] {Canyon,Crag,Wasteland};
		BGroupHotLV[SwampType] = new int[3] {DeadSwamp,Marsh,Quagmire};
		
		//High Vegetation Biome Variants
		public const int[][] BGroupColHV = new int[7][];
        BGroupColHV[WaterType] = new int[1] {FrozenOcean};
        BGroupColHV[PlainsType] = new int[1] {Tundra};
		BGroupColHV[ForestType] = new int[2] {ConiferousForestSnow,SpruceWoods};
		BGroupColHV[JungleType] = new int[1] {SacredSprings};
		BGroupColHV[DesertType] = new int[1] {Glacier};
		BGroupColHV[MountainType] = new int[1] {Glacier};
		BGroupColHV[SwampType] = new int[1] {Wetland};
		
		public const int[][] BGroupMedHV = new int[7][];
        BGroupMedHV[WaterType] = new int[6] {Ocean,CoralReef,CoralReef,KelpForest,KelpForest,OceanOilField};
        BGroupMedHV[PlainsType] = new int[7] {Brushland,Garden,Heathland,LavenderFields,LavenderFields,Pasture,Thicket};
		BGroupMedHV[ForestType] = new int[10] {BambooForestGrowthcraft,BorealForest,ConiferousForest,DeciduousForest,JadeCliffs,RedwoodForest,SeasonalForest,SpruceWoods,Woodland,Forest};
		BGroupMedHV[JungleType] = new int[7] {BambooForestGrowthcraft,BambooForest,FungiForest,SacredSprings,TemperateRainforest,TropicalRainforest,Jungle};
		BGroupMedHV[DesertType] = new int[3] {LushDesert,Mangrove,Oasis};
		BGroupMedHV[MountainType] = new int[7] {Highland,Highland,Mountain,Mountain,SacredSprings,Shield,ExtremeHills};
		BGroupMedHV[SwampType] = new int[6] {Bog,LushSwamp,LushSwamp,Silkglades,Sludgepit,Swamp};
		
		public const int[][] BGroupHotHV = new int[7][];
        BGroupHotHV[WaterType] = new nt[6] {Ocean,CoralReef,CoralReef,KelpForest,KelpForest,OceanOilField};
        BGroupHotHV[PlainsType] = new int[5] {Brushland,Heathland,Prairie,Scrubland,Thicket};
		BGroupHotHV[ForestType] = new int[8] {BambooForestGrowthcraft,BorealForest,DeciduousForest,Fen,FungiForest,JadeCliffs,RedwoodForest,SeasonalForest};
		BGroupHotHV[JungleType] = new int[7] {BambooForestGrowthcraft,BambooForest,FungiForest,TemperateRainforest,TropicalRainforest,Tropics,Jungle};
		BGroupHotHV[DesertType] = new int[4] {LushDesert,Mangrove,Oasis,Oasis};
		BGroupHotHV[MountainType] = new int[4] {Canyon,Highland,Mountain,ExtremeHills};
		BGroupHotHV[SwampType] = new int[5] {Bog,LushSwamp,Silkglades,Sludgepit,Swamp};	
		
		//Good Biomes Group
		public const int[][] BGroupGood = new int[7][];
        BGroupGood[WaterType] = new int[3] {CoralReef,KelpForest,OceanOilField};
        BGroupGood[PlainsType] = new int[3] {Garden,OriginValley,FlowerField};
		BGroupGood[ForestType] = new int[4] {MysticGrove,SacredSprings,MagicalForest,FlowerForest};
		BGroupGood[JungleType] = new int[2] {MagicalForest,Tropics};
		BGroupGood[DesertType] = new int[1] {Oasis};
		BGroupGood[MountainType] = new int[1] {SacredSprings};
		BGroupGood[SwampType] = new int[2] {Oasis,Tropics};	
		
		//Misc. Biome Groups
		public const int[] BGroupEvil = {OminousWoods,Eerie,Taint,Silkglades};
		public const int[] BGroupVolcano = {Deadlands,Volcano};
		
		//Array O' Everything
		public const int[][][] BGroupALL = {BGroupCol, BGroupMed, BGroupHot, BGroupColLV, BGroupMedLV, BGroupHotLV, BGroupColHV, BGroupMedHV, BGroupHotHV, BGroupGood};
		
		//Rules to remember:
		//Volcano if high volcanism.
		//Deadlands if REALLY (max) hot.
		//Good / Evil extremes overrides previous selection.
		//
		
    }
	
}
