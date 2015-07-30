using UnityEngine;
using System.Collections;

public class surLaCase : MonoBehaviour {

	Vector3 vecto = new Vector3 (13, 1, 13);
	Vector3 vect = new Vector3 (15, 3, 13);

	void Start()
	{
		transform.localScale = vecto;
		print (vecto);
	}

void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			transform.localScale = Vector3.Lerp(vecto, vect, 2.0f);
		}
	}

void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			transform.localScale = Vector3.Lerp(vect, vecto, 2.0f);
		}
	}

}
