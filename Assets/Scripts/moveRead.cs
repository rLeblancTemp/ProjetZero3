using UnityEngine;
using System.Collections;

public class moveRead : MonoBehaviour {

	// A placer sur le personnage

	//moveTempo moveTemp;
	Animator anim;
	public float speed;

	void Start()
	{
		//moveTemp = gameObject.transform.parent.GetComponent<moveTempo> ();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		if (Input.GetButton ("Vertical")) { // à remplacer par la commande d'action
			anim.SetBool ("movingForward", true);
			//transform.Translate (Vector3.forward * Time.deltaTime * speed);
		} else
			anim.SetBool ("movingForward", false);


		if (Input.GetKey(KeyCode.RightArrow)) { // à remplacer par la commande d'action
			anim.SetBool ("turnRight", true);
			transform.Rotate (new Vector3(0,90,0) * Time.deltaTime * speed/10);
		} else
			anim.SetBool ("turnRight", false);


		if (Input.GetKey(KeyCode.LeftArrow)) { // à remplacer par la commande d'action
			anim.SetBool ("turnLeft", true);
			transform.Rotate (new Vector3(0,-90,0) * Time.deltaTime * speed/10);
		} else
			anim.SetBool ("turnLeft", false);


	}
	
}

