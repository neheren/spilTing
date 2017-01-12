using UnityEngine;
using System.Collections;

public class daynight : MonoBehaviour {

	public float daySpeed = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate(new Vector3(daySpeed, 0,0));
	}
}
