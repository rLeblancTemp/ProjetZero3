using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public GameObject ObjectUnderFoots;
    Vector3 Dir;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
        Dir = new Vector3(0, -1, 0);
	}

    

	// Update is called once per frame
	void Update () {

        if (this.transform.position.y < -10)
        {
            GameObject.Find("SOC").GetComponent<Lecture>().FirstPlay();
        }
        
        if (Physics.Raycast(transform.position, Dir, out hit, 2))
        {
            ObjectUnderFoots = hit.transform.gameObject;
        }
        else
        {
            ObjectUnderFoots = null;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("CASETAG : " + col.gameObject.tag);
        Lecture.LectureObject.CurrentCase = col.gameObject;
    }
}
