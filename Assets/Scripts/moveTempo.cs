using UnityEngine;
using System.Collections;

public class moveTempo : MonoBehaviour {

	public float speed;

	public bool move;

	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			//transform.Translate (Vector3.forward * Time.deltaTime * speed);
			move = true;
		} else
			move = false;
	
	}
}
