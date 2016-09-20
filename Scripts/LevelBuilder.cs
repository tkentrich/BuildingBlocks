using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

	public enum ObjectType {Player, Brick, Block, Coin, Diamond, Exit};

	public GameObject playerPrefab;
	public GameObject brickPrefab;
	public GameObject blockPrefab;
	public GameObject coinPrefab;
	public GameObject diamondPrefab;
	public GameObject exitPrefab;
	public Text inventoryText;
	public Canvas inventoryCanvas;
	public DisplayLevelResult displayResult;
	public Camera cam;

	public GameObject objectContainer;

	GameProgress prog;
	public int levelID;
	private string levelName;
	private Vector3 size;
	private Vector3 defaultStart;
	public Vector3 newStart;

	private LevelResult levelResult;
	private GameObject player;

	void Start () {
		prog = Object.FindObjectOfType<GameProgress>();
		if (prog == null) {
			SceneManager.LoadScene("Initialize");
		} else {
			levelID = prog.levelIndex;
			Build(levelID);
		}
	}

	void Build (int level) {
		levelResult = new LevelResult();

		string name;
		Vector3 size;
		Vector3 start;
		string objects;
		int[] unlock;
		LevelData.GetData(level, out name, out size, out start, out objects, out unlock);
		prog.WillUnlock(unlock);

		SetSize(size);
		Add(ObjectType.Player, start);
		player.GetComponent<PlayerInfo>().inventoryText = inventoryText;
		player.GetComponent<PlayerInfo>().inventoryCanvas = inventoryCanvas;
		player.GetComponent<PlayerController>().levelResult = levelResult;
		player.GetComponent<PlayerController>().displayResult = displayResult;
		cam.GetComponent<CameraController>().SetFocus(player.transform);
		Add(objects);
	}

	void SetSize(Vector3 size) {
		SetSize((int)size.x, (int)size.y, (int)size.z);
	}

	void SetSize(int x, int y, int z) {
		size.x = x;
		size.y = y;
		size.z = z;
		Transform ceiling = transform.Find("BoundingBox/Ceiling");
		ceiling.position = new Vector3(x / 2f - 0.5f, y - 0.5f, z / 2f - 0.5f);
		ceiling.localScale = new Vector3(x * .1f, 1f, z * .1f);
		Transform floor = transform.Find("BoundingBox/Floor");
		floor.position = new Vector3(x / 2f - 0.5f, -0.5f, z / 2f - 0.5f);
		floor.localScale = new Vector3(x * .1f, 1f, z * .1f);
		Transform wall = transform.Find("BoundingBox/Wall-N");
		wall.position = new Vector3(x / 2f - 0.5f, y  / 2f - 0.5f, z - 0.5f);
		wall.localScale = new Vector3(x * .1f, 1f, y * .1f);
		wall = transform.Find("BoundingBox/Wall-S");
		wall.position = new Vector3(x / 2f - 0.5f, y  / 2f - 0.5f, -0.5f);
		wall.localScale = new Vector3(x * .1f, 1f, y * .1f);
		wall = transform.Find("BoundingBox/Wall-E");
		wall.position = new Vector3(x - 0.5f, y  / 2f - 0.5f, z / 2f - 0.5f);
		wall.localScale = new Vector3(y * .1f, 1f, z * .1f);
		wall = transform.Find("BoundingBox/Wall-W");
		wall.position = new Vector3(-0.5f, y  / 2f - 0.5f, z / 2f - 0.5f);
		wall.localScale = new Vector3(y * .1f, 1f, z * .1f);
	}

	void Add (string data) {
		int x = 0;
		int y = 0;
		int z = (int)size.z - 1;

		foreach (char c in data) {
			switch (c) {
				default:
					break;
				case LevelData.BRICK:
					Add(ObjectType.Brick, x, y, z);
					break;
				case LevelData.BLOCK:
					Add(ObjectType.Block, x, y, z);
					break;
				case LevelData.EXIT:
					Add(ObjectType.Exit, x, y, z);
					break;
				case LevelData.COIN:
					Add(ObjectType.Coin, x, y, z);
					levelResult.AddCoin();
					break;
				case LevelData.DIAMOND:
					Add(ObjectType.Diamond, x, y, z);
					levelResult.AddDiamond();
					break;
			}
			x++;
			if (x == size.x) {
				x = 0;
				z--;
				if (z == -1) {
					z = (int)size.z - 1;
					y++;
				}
			}
		}
	}

	void Add (string[][] data) {
		int y = 0;
		foreach (string[] d in data) {
			Add(y++, d);
		}
	}

	void Add (int y, string[] data) {
		int z = data.Length - 1;
		foreach (string d in data) {
			int x = 0;
			foreach (char c in d) {
				switch (c) {
					default:
						break;
					case LevelData.EXIT:
						Add(ObjectType.Exit, x, y, z);
						break;
					case LevelData.BRICK:
						Add(ObjectType.Brick, x, y, z);
						break;
					case LevelData.BLOCK:
						Add(ObjectType.Block, x, y, z);
						break;
					case LevelData.COIN:
						Add(ObjectType.Coin, x, y, z);
						break;
					case LevelData.DIAMOND:
						Add(ObjectType.Diamond, x, y, z);
						break;
				}
				x++;
			}
			z--;
		}
	}

	void Add (ObjectType type, Vector3 pos) {
		Add(type, (int)pos.x, (int)pos.y, (int)pos.z);
	}

	void Add (ObjectType type, int x, int y, int z) {
		Add(type, x, y, z, 0, 0, 0);
	}

	void Add (ObjectType type, int x, int y, int z, int xr, int yr, int zr) {
		switch (type) {
			case ObjectType.Block:
				GameObject block = (GameObject)Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				block.transform.parent = objectContainer.transform;
				break;
			case ObjectType.Brick:
				Instantiate(brickPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				break;
			case ObjectType.Coin:
				Instantiate(coinPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				break;
			case ObjectType.Diamond:
				Instantiate(diamondPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				break;
			case ObjectType.Exit:
				Instantiate(exitPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				break;
			case ObjectType.Player:
				player = (GameObject)Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.Euler(xr, yr, zr));
				break;
		}
	}
}
