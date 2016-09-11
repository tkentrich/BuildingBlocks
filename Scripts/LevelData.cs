using UnityEngine;
using System.Collections;

public static class LevelData {

	public const char EXIT = 'O';
	public const char BLOCK = 'B';
	public const char BRICK = 'X';
	public const char COIN = 'C';
	public const char DIAMOND = '$';

	private static Hashtable nameTable;
	private static Hashtable sizeTable;
	private static Hashtable startTable;
	private static Hashtable objectsTable;

	public static void InitData() {
		nameTable = new Hashtable();
		sizeTable = new Hashtable();
		startTable = new Hashtable();
		objectsTable = new Hashtable();

		nameTable[0] = "Intro 01";
		sizeTable[0] = new Vector3(9, 5, 8);
		startTable[0] = new Vector3(1, 0, 1);
		objectsTable[0] = 
			// 0
			"   XXX   " +
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
			" X   XX X" +
			"X        " +
			"         " +
			"         " +
			"         " +
			"         " +
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
			"     BXXX" +
			" X       " +
			"XX       " +
			" X       " +
			"         " +
			"         " +
			"         " +
			"X       X" +
			// 4
			"         " +
			"         " +
			"        X" +
			"         " +
			"         " +
			"         " +
			"         " +
			"O        ";
		
	}

	public static Hashtable LevelNames() {
		return LevelNames("default");
	}
	public static Hashtable LevelNames(string levelSet) {
		Hashtable toReturn = new Hashtable();

		return toReturn;
	}

	public static void GetData(int id, out string name, out Vector3 size, out Vector3 start, out string objects) {
		if (nameTable == null) {
			InitData();
		}
		name = (string)(nameTable[id]);
		size = (Vector3)sizeTable[id];
		start = (Vector3)startTable[id];
		objects = (string)objectsTable[id];
	}
	
}
		/*
			default:
			case 1:
				SetSize(9, 5, 8);
				defaultStart = new Vector3(1, 0, 1);
				Add(0, new string[] {
					"   XXX   ",
					"BX      B",
					"X   XX   ",
					"        X",
					"X        ",
					"         ",
					"         ",
					"   X   X ",
				});
				Add(1, new string[] {
					" X   X   ",
					" X       ",
					" X   XX X",
					"X        ",
					"         ",
					"         ",
					"         ",
					"         ",
				});
				Add(2, new string[] {
					" X X    X",
					" X      X",
					" X     XX",
					"         ",
					"      X X",
					" XXXXXX X",
					"        X",
					"XXX XXX X",
				});
				Add(3, new string[] {
					"     BXXX",
					" X       ",
					"XX       ",
					" X       ",
					"         ",
					"         ",
					"         ",
					"X       X",
				});
				Add(4, new string[] {
					"         ",
					"         ",
					"        X",
					"         ",
					"         ",
					"         ",
					"         ",
					"O        ",
				});
				break;
			case 2:
				SetSize(8, 4, 8);
				Add(0, new string[] {
					"  BXXXCX",
					" B XXXX ",
					" X XBX  ",
					" X X X  ",
					" X X X  ",
					"    B  C",
					" XXXXX  ",
					"        ",
				});
				Add(1, new string[] {
					"   XXX B",
					" B XXXX ",
					" XXX X  ",
					" XX     ",
					" XXXXX  ",
					" X   X  ",
					" XXXXX  ",
					"        ",
				});
				Add(2, new string[] {
					"   XXXX ",
					"   XXXX ",
					" X X X  ",
					" XX X XX",
					" XXXXX  ",
					" X   X  ",
					" XXXXX  ",
					"        ",
				});
				Add(3, new string[] {
					"     XX ",
					"  X XXX ",
					" XX  XX ",
					" X     O",
					" X X    ",
					"        ",
					" XXXXX  ",
					"        ",
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