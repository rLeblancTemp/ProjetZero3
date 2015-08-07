using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class R_Slot : MonoBehaviour, IDropHandler
{

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
        {

		//sets the right color if the button dragged is a color
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

		//do nothing if a color is dragged on an empty slot
        else if (Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color3 || Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color2 || Pool.ItemBeingDragged.GetComponent<Button>().ButtonType == Button.type.Color1)
            {
            return;
        }
        else

			//replace the button if you drag an other button on it from the original pool
        if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().Used == false)
        {
            Destroy(transform.GetChild(0).gameObject, 0.01f);
        }

		//switch button positions if you drag the button from a slot
        else if (transform.childCount == 1 && Pool.ItemBeingDragged.GetComponent<Button>().Used == true)
        {
            GameObject tempChild;
            tempChild = transform.GetChild(0).gameObject;
            tempChild.transform.SetParent(Pool.ItemBeingDragged.GetComponent<Button>().LastParent.transform);
            tempChild.GetComponent<Button>().LastParent = Pool.ItemBeingDragged.GetComponent<Button>().LastParent;
            tempChild.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        }
        
		//snap + prevent from being destroyed by Button.OnEndDrag()
        Pool.ItemBeingDragged.GetComponent<Button>().LastParent = this.gameObject;
        Pool.ItemBeingDragged.transform.SetParent(transform);
        Pool.ItemBeingDragged.GetComponent<Image>().rectTransform.localPosition = Vector3.zero;
        

        

    }
    #endregion

}
