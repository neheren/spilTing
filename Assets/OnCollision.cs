using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {
	public GameObject explosion;
	public int maxExplosion = 6;
	private int explosions = 0;
	void OnCollisionEnter(Collision collision){
		explosions ++;

		if (explosions < maxExplosion) {
			//GameObject[] comp = this.gameObject.GetComponentsInChildren <GameObject>();

			foreach (Transform item in transform) {
				if (item.name == "AircraftFuselage" || item.name == "AircraftWingsJet" || item.name == "FPSController" ) {
					item.transform.parent = null;

					foreach (Transform innerItem in item.transform) {
						//innerItem.transform.parent = null;
					}
				}
			}
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
			Instantiate (explosion, gameObject.transform.position, Quaternion.identity);

		}
		Debug.Log (gameObject);

	}
}
