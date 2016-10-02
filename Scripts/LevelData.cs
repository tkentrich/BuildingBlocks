using UnityEngine;
using System.Collections;

public static class LevelData {

	public const char EXIT =    'O';
	public const char BLOCK =   'B';
	public const char BRICK =   'X';
	public const char COIN =    'C';
	public const char DIAMOND = '$';

	private static Hashtable nameTable;
	private static Hashtable sizeTable;
	private static Hashtable startTable;
	private static Hashtable objectsTable;
	private static Hashtable unlockTable;
	private static bool initialized = false;

	public static void InitData() {
		MonoBehaviour.print("Initializing...");
		nameTable = new Hashtable();
		sizeTable = new Hashtable();
		startTable = new Hashtable();
		objectsTable = new Hashtable();
		unlockTable = new Hashtable();

		nameTable[-1] = "Quick";
		sizeTable[-1] = new Vector3(5, 1, 5);
		startTable[-1] = new Vector3(2, 0, 2);
		objectsTable[-1] =
			"O C C" +
			" C $ " +
			"     " +
			"     " +
			"     ";
		unlockTable[-1] = new int[] { 5 };

		nameTable[0] = "Intro 01 ??%";
		sizeTable[0] = new Vector3(5, 6, 5);
		startTable[0] = new Vector3(0, 0, 1);
		objectsTable[0] = 
			// 0	
			"    B" +
			"     " +
			"    B" +
			"     " +
			"     " +
			// 1
			"     " +
			" CXX " +
			"    B" +
			"     " +
			"     " +
			// 2
			" XXX " +
			"  XXX" +
			" XXX " +
			"     " +
			" XXXX" +
			// 3
			"  C  " +
			"     " +
			" X X " +
			"     " +
			" X  B" +
			// 4
			"     " +
			"     " +
			" X   " +
			"     " +
			" O   " +
			// 5
			"     " +
			"     " +
			" $   " +
			"     " +
			"     ";
		unlockTable[0] = new int[] {1};

		nameTable[1] = "Crossroad ??%";
		sizeTable[1] = new Vector3(5, 5, 5);
		startTable[1] = new Vector3(0, 0, 0);
		objectsTable[1] =
			// 0
			"B  XB" +
			"XX X " +
			"  XX " +
			" XXX " +
			"   XB" +
			// 1
			"  CXC" +
			"XX   " +
			"  BX " +
			" X X$" +
			"   X " +
			// 2
			"XXXXX" +
			"XXX X" +
			" XCXX" +
			" X XX" +
			"  XXX" +
			// 3
			"    X" +
			"     " +
			"   X " +
			"    X" +
			"    X" +
			// 4
			"   X " +
			"XX   " +
			"   X " +
			" X X " +
			"   XO";
		unlockTable[1] = new int[] { 2 };

		nameTable[2] = "Corridor ??%";
		sizeTable[2] = new Vector3(6, 4, 6);
		startTable[2] = new Vector3(1, 0, 1);
		objectsTable[2] =
			"B  XXX" +
			"C XX $" +
			"X XX X" +
			"X XX X" +
			"X    X" +
			"XXXXXX" +

			" C  XX" +
			"X X  X" +
			"X X  B" +
			"X XX X" +
			"X    X" +
			"XXCXXX" +

			"XX  XX" +
			"X X  X" +
			"X X   " +
			"X XX X" +
			"X    X" +
			"XXXXXX" +

			"O   XX" +
			"X X  X" +
			"X X   " +
			"X XX X" +
			"X    X" +
			"XXXXXX";
		
		unlockTable[2] = new int[] { 3 };

		nameTable[3] = "Original ??%";
		sizeTable[3] = new Vector3(8, 4, 8);
		startTable[3] = new Vector3(7, 0, 0);
		objectsTable[3] =
			// 0
			"  BXXXCX" +
			" B XXXX " +
			" X XBX  " +
			" X X X  " +
			" X X X  " +
			"    B  C" +
			" XXXXX  " +
			"        " +
			// 1
			"   XXX B" +
			" B XXXX " +
			" XXX X  " +
			" XX     " +
			" XXXXX  " +
			" X   X  " +
			" XXXXX  " +
			"        " +
			// 2
			"   XXXX " +
			"   XXXX " +
			" X X X  " +
			" XX X XX" +
			" XXXXX  " +
			" X   X  " +
			" XXXXX  " +
			"        " +
			// 3
			"     XX " +
			"  X XXX " +
			" XX  XX " +
			" X     O" +
			" X X    " +
			"        " +
			" XXXXX  " +
			"        ";
		unlockTable[3] = new int[] { 5 };

		nameTable[4] = "Double 2D ??%";
		sizeTable[4] = new Vector3(11, 8, 3);
		startTable[4] = new Vector3(1, 6, 2);
		objectsTable[4] = 
		// 0
			"XXXX X   XX" +
			"XXXXXXXXXXX" +
			"O   X    XX" +
			// 1
			"XXX      XX" +
			"XXXXXXXXXXX" +
			"  $       X" +
			// 2
			"B        XX" +
			"XXXXXXXXXXX" +
			"           " +
			// 3
			"B          " +
			"XXXXXXXXXX " +
			"           " +
			// 4
			"           " +
			"XXXXXXXXXXX" +
			"           " +
			// 5
			"XXXX       " +
			"XXXXXXXXXXX" +
			"XXXXXXXX   " +
			// 6
			"B          " +
			"XXXXXXXXXXX" +
			"B      B   " +
			// 7
			"   C       " +
			"XXX XXXXXXX" +
			"           ";
		unlockTable[4] = new int[] { 5 };

		nameTable[5] = "Level 05";
		sizeTable[5] = new Vector3(9, 5, 8);
		startTable[5] = new Vector3(1, 0, 1);
		objectsTable[5] = 
			// 0
			" C XXX   " +
			"BX      B" +
			"X   XX   " +
			"        X" +
			"X        " +
			"         " +
			"         " +
			"   X   X " +
			// 1
			" X   X   " +
			" X       " +
			" X   XXCX" +
			"X        " +
			"         " +
			"         " +
			"         " +
			"        C" +
			// 2
			" X X    X" +
			" X      X" +
			" X     XX" +
			"         " +
			"      X X" +
			" XXXXXX X" +
			"        X" +
			"XXX XXX X" +
			// 3
			"   X BXXX" +
			" X       " +
			"XX       " +
			" X       " +
			"         " +
			"         " +
			"         " +
			"X       X" +
			// 4
			"   $     " +
			"         " +
			"        X" +
			"         " +
			"         " +
			"         " +
			"         " +
			"O        ";
		unlockTable[5] = new int[] { 6 };

		nameTable[6] = "Intro 02";
		sizeTable[6] = new Vector3(5, 4, 5);
		startTable[6] = new Vector3(2, 1, 2);
		objectsTable[6] =
			"  XX " +
			"     " +
			"X X  " +
			"X    " +
			"   XX" +

			"XXB  " +
			"X XX " +
			"X    " +
			"     " +
			"   XX" +

			"    X" +
			"  XX " +
			"     " +
			"     " +
			"    O" +

			"  X  " +
			"  XX " +
			"     " +
			"     " +
			"     ";
		unlockTable[6] = new int[] { };

		initialized = true;
	}
	public static int[] InitialUnlock() {
		return InitialUnlock("default");
	}
	public static int[] InitialUnlock(string levelSet) {
		//return new int[] { 4, 0, 1, 3 };
		return CampaignLevels(levelSet);
	}

	public static int[] CampaignLevels() {
		return CampaignLevels("default");
	}
	public static int[] CampaignLevels(string levelSet) {
		switch (levelSet) {
			default:
			case "default":
				return new int[] { -1, 0, 6, 1, 2, 3, 4, 5 };
		}
	}

	public static void GetData(int id, out string name, out Vector3 size, out Vector3 start, out string objects, out int[] unlock) {
		if (!initialized) {
			InitData();
		}
		MonoBehaviour.print("GetData(" + id + ")");
		name = (string)(nameTable[id]);
		size = (Vector3)(sizeTable[id]);
		start = (Vector3)(startTable[id]);
		objects = (string)(objectsTable[id]);
		unlock = (int[])(unlockTable[id]);
	}

	public static string name(int id) {
		return (string)(nameTable[id]);
	}
		
}
		/*
			case 2:
				SetSize(8, 4, 8);
				Add(0, new string[] {
				});
				Add(1, new string[] {
					
				});
				defaultStart = new Vector3(7, 0, 0);
				break;
			case 3:
				// SetSize();
				break;
			case 0:
				SetSize(8, 4, 8);
				defaultStart = new Vector3(7, 0, 0);
				Add(ObjectType.Brick, 1, 0, 1);
				Add(ObjectType.Brick, 1, 1, 1);
				Add(ObjectType.Brick, 1, 2, 1);
				Add(ObjectType.Brick, 2, 0, 1);
				Add(ObjectType.Brick, 2, 1, 1);
				Add(ObjectType.Brick, 2, 2, 1);
				Add(ObjectType.Brick, 2, 3, 1);
				break;
		} 
		 */