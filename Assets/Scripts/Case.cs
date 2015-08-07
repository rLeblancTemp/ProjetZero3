using UnityEngine;
using System.Collections;


public class Case : MonoBehaviour {

    
    
    public colorType CaseColor = colorType.none;
    // Use this for initialization
    void Start () {
        var boxcol1 = (BoxCollider)gameObject.AddComponent(typeof(BoxCollider));
        boxcol1.isTrigger = true;
        boxcol1.size = new Vector3(0.5f, 10, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
