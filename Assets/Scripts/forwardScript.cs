using UnityEngine;
using System.Collections;

public class forwardScript : MonoBehaviour {

	public bool action;
	GameObject player;
	public float speed = 20f;

	void Start()
	{
		action = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		//print ("Player trouvé");
	}

	void Update () {
	if (action == true) {
			player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
	}
}
