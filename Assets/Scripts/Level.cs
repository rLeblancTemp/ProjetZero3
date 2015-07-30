using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level : MonoBehaviour, IPointerClickHandler {


    public int lvlNb;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        Loadlevel();
    }

    #endregion


    void Loadlevel()
    {
        if (lvlNb > 9)
        {
            Application.LoadLevel("Level" + lvlNb);
        }
        else
        {
            Application.LoadLevel("Level0" + lvlNb);
        }
    }
}
