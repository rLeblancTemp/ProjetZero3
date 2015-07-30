using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IDropHandler {

	public GameObject item{
		get{
			if(transform.childCount>0)
			{
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			dragFinger.itemBeingDragged.transform.SetParent(transform); // quand tu es drag sur moi, je deviens ton parent
			//ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x,y) => x.HasChanged());
		}
	}
	#endregion
}
