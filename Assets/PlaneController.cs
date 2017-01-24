using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{
	public GameObject player;
	public GameObject PerlinPlane;
	public int sizeOfPlane;
	int oldXChunk;
	int oldYChunk;
	GameObject[,] chunks;
	Vector2 playerInChunk;
	public int kernelSize = 5;
	private IEnumerator co;
	// Use this for initialization
	void Start ()
	{
		float playerXPos = player.transform.position.x;
		float playerYPos = player.transform.position.z;
		Vector2 playerInChunk = new Vector2 ((playerXPos - (playerXPos % sizeOfPlane)), playerYPos - (playerYPos % sizeOfPlane));
		oldXChunk = (int)playerInChunk.x;
		oldYChunk = (int)playerInChunk.y;

		chunks = new GameObject[kernelSize, kernelSize];
		co = generateChunks ();
		StartCoroutine (co);
	}

	// Update is called once per frame
	void Update ()
	{
		Vector2 playerPos = new Vector2 (player.transform.position.x, player.transform.position.z);
		playerInChunk = new Vector2 ((int)(playerPos.x - (playerPos.x % sizeOfPlane)), (int)(playerPos.y - (playerPos.y % sizeOfPlane)));

		if (playerInChunk.x != oldXChunk || playerInChunk.y != oldYChunk) {
			oldXChunk = (int)playerInChunk.x;
			oldYChunk = (int)playerInChunk.y;

			print ("Player in new chunk");

			co = generateChunks ();
			StartCoroutine (co);
		}

	}

	private IEnumerator generateChunks ()
	{

		int halfKernel = kernelSize / 2;
		for (int y = 0; y < kernelSize; y++) {
			for (int x = 0; x < kernelSize; x++) {
				
				Vector3 currentPlanePos = new Vector3 (playerInChunk.x + (x - halfKernel) * sizeOfPlane, 0, playerInChunk.y + (y - halfKernel) * sizeOfPlane);
				if (chunks [x, y] == null)
					chunks [x, y] = (GameObject)Instantiate (PerlinPlane, currentPlanePos, Quaternion.identity, gameObject.transform);
				
				//if (Vector2.Distance (chunks [x, y].transform.position, currentPlanePos) > 100) {
				chunks [x, y].transform.position = currentPlanePos;
				meshmodify script = chunks [x, y].GetComponent<meshmodify> ();

				script.generatePerlin ();
				//}

				yield return true;
			}
		}
	}

}


//Vector3 pos = new Vector3 (playerInChunk.x + (x - 2) * sizeOfPlane, 0, playerInChunk.y + (y - 2) * sizeOfPlane);
//if (chunks [x, y] == null)
//	chunks [x, y] = (GameObject)Instantiate (PerlinPlane, pos, Quaternion.identity, gameObject.transform);
//
//print (Vector2.Distance (chunks [x, y].transform.position, playerInChunk));
//
//if (Vector2.Distance (chunks [x, y].transform.position, pos) > 100) {
//	print ("deleting");
//	Destroy (chunks [x, y]);
//}
