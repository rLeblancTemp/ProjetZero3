using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public List<GameObject> Levels;

	// Use this for initialization
	void Start () {
        int levelNumber = 1;
        foreach (Transform child in transform)
        {
            child.GetComponent<Level>().lvlNb = levelNumber;
            child.GetChild(0).GetComponent<Text>().text = levelNumber.ToString();
            Levels.Add(child.gameObject);
            levelNumber++;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
