using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum colorType { none, C1, C2, C3 };//color of the button C1 : RED, C2 : Green, C2 : Blue

public class Button : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region Declarations
    GameObject Slot; //CurrentSlot in A, B or C
    public Image ButtonPrefab; // Prefab of itself (Button)
    
    [HideInInspector]
    public enum type { Forward, Left, Right, A, B, C, Color1, Color2, Color3 }
    public type ButtonType = type.Forward;

    [HideInInspector]
    public colorType BlockColor = colorType.none; //Actual Color
    [HideInInspector]
    public GameObject LastParent = null;
    public Sprite FondBlanc;

    //Sprites
    int S_Type; //used to position correctly the next instanciated button
    public Sprite S_Forward;
    public Sprite S_Left;
    public Sprite S_Right;
    public Sprite S_A;
    public Sprite S_B;
    public Sprite S_C;

    Image _sprite; //Image UI component

	//This could be replaced by an enum
	[HideInInspector]
	public bool Used = false;
    bool dragged = false;
	//
	#endregion

    // Use this for initialization
    void Start() 
	{
		ButtonPrefab = (Image)Resources.Load("Prefabs/buttonPrefab", typeof(Image));
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Slot = Pool.PoolObject;
        _sprite = this.GetComponent<Image>();
		setType(ButtonType);
    }

	#region Initialize Type

/// <summary>
/// Sets the type.
/// </summary>
/// <param name="_ButtonType">_ button type.</param>
	public void setType(type _ButtonType)
	{
		switch (_ButtonType)
		{
		case type.Forward:
			_sprite.sprite = S_Forward;
			S_Type = 0;
			break;
			
		case type.Left:
			_sprite.sprite = S_Left;
			S_Type = 1;
			break;
			
		case type.Right:
			_sprite.sprite = S_Right;
			S_Type = 2;
			break;
			
		case type.A:
			_sprite.sprite = S_A;
			S_Type = 3;
			break;
		case type.B:
			_sprite.sprite = S_B;
			S_Type = 4;
			break;
			
		case type.C:
			_sprite.sprite = S_C;
			S_Type = 5;
			break;
			
		case type.Color1:
			_sprite.sprite = FondBlanc;
			//GetComponent<Image>().color = Color.red;
			GetComponent<Image>().color = new Color(0.3f, 0.1f, 0.7f);
			S_Type = 6;
			break;
			
		case type.Color2:
			_sprite.sprite = FondBlanc;
			//GetComponent<Image>().color = Color.green;
			GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.15f);
			S_Type = 7;
			break;
			
		case type.Color3:
			_sprite.sprite = FondBlanc;
			GetComponent<Image>().color = Color.blue;
			S_Type = 8;
			break;
		}
	}

	#endregion

	#region AntiBug
//    void Update() { //AntiBug of the Drag&Drop Bug, use this if the bug is still happening
//		if (Used == true && dragged == false && transform.parent.name == "MOC")
//		{
//			Destroy(this.gameObject);
//		}
//
//    }

	#endregion
#region Drag&Drop

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Lecture.playing == false)
        {
            dragged = true;
            Pool.ItemBeingDragged = this.gameObject;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            this.gameObject.transform.SetParent(GameObject.Find("MOC").transform);
            if (!Used)
            {
                InstanciatePrefab();
            }
        }
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (dragged == true)
        {
            this.transform.position = Input.mousePosition; //+ new Vector3(0, 70, 0);
        }
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragged == true)
        {
            dragged = false;
            Used = true;
            Pool.ItemBeingDragged = null;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (transform.parent.name == "MOC"/*GameObject.Find("MOC").transform*/)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

#endregion

	public void InstanciatePrefab()
	{
		Image NewForward;
		NewForward = (Image)Instantiate(ButtonPrefab, Slot.GetComponent<Pool>().ButtonsSpots[S_Type], Quaternion.identity);
		NewForward.GetComponent<Button>().ButtonType = ButtonType;
		NewForward.transform.SetParent(Pool.PoolObject.transform, false);
		NewForward.rectTransform.sizeDelta = new Vector2 (28, 28);
		NewForward.rectTransform.localPosition = Slot.GetComponent<Pool>().ButtonsSpots[S_Type];
	}
}
