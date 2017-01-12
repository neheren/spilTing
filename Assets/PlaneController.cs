using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
	public GameObject player;
	public GameObject PerlinPlane;
	public int sizeOfPlane;
	int oldXChunk;
	int oldYChunk;
	GameObject[,] chunks;
	int xChunk;
	int yChunk;
	public int kernelSize = 5;
	private IEnumerator co;
	// Use this for initialization
	void Start () {
		float playerXPos = player.transform.position.x;
		float playerYPos = player.transform.position.z;
		xChunk = (int) (playerXPos - (playerXPos % sizeOfPlane));
		yChunk = (int) (playerYPos - (playerYPos % sizeOfPlane));
		oldXChunk = xChunk;
		oldYChunk = yChunk;

		chunks = new GameObject[kernelSize,kernelSize];
		co = generateChunks ();
		StartCoroutine (co);
	}

	// Update is called once per frame
	void Update () {
		float playerXPos = player.transform.position.x;
		float playerYPos = player.transform.position.z;
		xChunk = (int) (playerXPos - (playerXPos % sizeOfPlane));
		yChunk = (int) (playerYPos - (playerYPos % sizeOfPlane));

		if (xChunk != oldXChunk || yChunk != oldYChunk) {
			oldXChunk = xChunk;
			oldYChunk = yChunk;
			co = generateChunks ();
			StartCoroutine (co);
			//generateChunks ();
		}

	}

	private IEnumerator generateChunks(){
		for (int y = 0; y < kernelSize; y++) {
			for (int x = 0; x < kernelSize; x++) {
				//Destroy (chunks [x, y]);
				Vector3 pos = new Vector3 (xChunk + (x - 2) * sizeOfPlane, 0, yChunk + (y - 2) * sizeOfPlane);
				chunks[x,y] = (GameObject)Instantiate (PerlinPlane, pos, Quaternion.identity, gameObject.transform);
				yield return true;
			}
		}
	}

}
