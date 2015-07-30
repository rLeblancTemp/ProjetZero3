/*using UnityEngine;
using System.Collections;

public class readActions : MonoBehaviour {

	// faire lire ce script en appuyant sur un bouton
	// va chercher les components de ses enfants. Si tel script est détecté, le jouer, waitforSeconds, jouer le suivant...
	public GameObject actions; // le Canvas Actions

	Transform[] allChildren = GetComponentsInChildren(Transform);

	forwardScript scriptAvancer;
	public bool lire; // à remplacer

	void Start () { // lier cette commande à un eventTrigger

		readButton ();

		scriptAvancer = GetComponent<forwardScript> ();

		if(lire == true)
		{
			print ("scriptAvancer");
			scriptAvancer.action = true;
			print ("scriptAvancerTrue");
		}
	}
	

	void Update () {
	
	}

	void readButton(){
		foreach(Transform child in allChildren){
			// find leur script d'action et les lire les uns après les autres
		}
	}
}*/
