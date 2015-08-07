using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Pool : MonoBehaviour { //remembers the buttons position in the pool if you suppres the disabled gameobject in pool, you'll fuck up everything (or recode it clean xD)

    [HideInInspector]
    public static GameObject ItemBeingDragged;


    public static GameObject PoolObject;

    [HideInInspector]
    public List<Vector3> ButtonsSpots;

    void Awake()
    {
        PoolObject = this.gameObject;
    }

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            ButtonsSpots.Add(child.gameObject.GetComponent<RectTransform>().localPosition);
//			child.gameObject.GetComponent<Button>().InstanciatePrefab();
//			Destroy(child.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
