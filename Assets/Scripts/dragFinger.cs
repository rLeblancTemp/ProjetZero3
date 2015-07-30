using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragFinger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;
	Transform otherParent;

    bool dragged = false;

	void Start()
	{
		otherParent = gameObject.transform.parent.parent.parent;
		//otherParent.SetParent(otherParent.transform.parent.parent); //GameObject.FindGameObjectWithTag ("Canvas");
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
        if (Lecture.playing == false)
        {
            dragged = true;
            itemBeingDragged = gameObject;
            startPosition = transform.position;

            startParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
	}

    #endregion


    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (dragged == true) {
        transform.position = Input.mousePosition;
        transform.SetParent(otherParent);
    }
	}

	#endregion


	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
        if (dragged == true)
        {
            dragged = false;
            itemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent == otherParent)
            {
                Destroy(gameObject);
            }

            if (transform.parent == startParent)
            { // reviens dans le Slot si je te glisses dans le vide
                transform.position = startPosition;
            }
        }
	}
	#endregion

}
