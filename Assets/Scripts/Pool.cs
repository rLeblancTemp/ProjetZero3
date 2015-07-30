using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Pool : MonoBehaviour {

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
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
