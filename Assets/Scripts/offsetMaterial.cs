using UnityEngine;
using System.Collections;

public class offsetMaterial : MonoBehaviour {

	// Fait défiler l'offset du material, permet d'animer le background

	public float scrollSpeed = 0.05F;
	public Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		float offset = Time.time * scrollSpeed;
		rend.material.mainTextureOffset = new Vector2(0, offset);
		rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));	}
}
