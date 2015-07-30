using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class R_Slot : MonoBehaviour, IDropHandler
{




    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //if (rect.Contains(Input.mousePosition))
        //    print("Inside");
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
        {

        //Debug.Log(transform.childCount);
        if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color1)
        {
            transform.GetChild(0).gameObject.GetComponent<Button>().BlockColor = colorType.C1;
            transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.red;
            return;
        } else

        if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color2)
        {
            transform.GetChild(0).gameObject.GetComponent<Button>().BlockColor = colorType.C2;
            transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
            return;
        }
        else
        if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color3)
        {
            transform.GetChild(0).gameObject.GetComponent<Button>().BlockColor = colorType.C3;
            transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.blue;
            return;
        }
        else if (Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color3 || Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color2 || Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color1)
            {
            return;
        }
        else

        if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().Used == false)
        {
            Destroy(transform.GetChild(0).gameObject, 0.01f);
        }
        else if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().Used == true)
        {
            GameObject tempChild;
            tempChild = transform.GetChild(0).gameObject;
            tempChild.transform.SetParent(Pool.ItemBeingDragged.GetComponent<Button>().LastParent.transform);
            tempChild.GetComponent<Button>().LastParent = Pool.ItemBeingDragged.GetComponent<Button>().LastParent;
            tempChild.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        }
        

        Pool.ItemBeingDragged.GetComponent<Button>().LastParent = this.gameObject;
        Pool.ItemBeingDragged.transform.SetParent(transform);
        Pool.ItemBeingDragged.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        

        

    }
    #endregion

}
