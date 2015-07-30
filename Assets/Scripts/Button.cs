using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    GameObject Slot;
    public Image ForwardPrefab;
    [HideInInspector]
    public bool Used = false;
    [HideInInspector]
    public enum type { Forward, Left, Right, A, B, C, Color1, Color2, Color3 }
    public type ButtonType;
    [HideInInspector]
    public colorType BlockColor = colorType.none;
    [HideInInspector]
    public GameObject LastParent = null;
    public Sprite FondBlanc;

    //Sprites
    int S_Type;
    public Sprite S_Forward;
    public Sprite S_Left;
    public Sprite S_Right;
    public Sprite S_A;
    public Sprite S_B;
    public Sprite S_C;

    Image _sprite;
    bool dragged = false;

    // Use this for initialization
    void Start() {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Slot = Pool.PoolObject;
        _sprite = this.GetComponent<Image>();

        switch (ButtonType)
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
            GetComponent<Image>().color = Color.red;
            S_Type = 6;
            break;

            case type.Color2:
            _sprite.sprite = FondBlanc;
            GetComponent<Image>().color = Color.green;
            S_Type = 7;
            break;

            case type.Color3:
            _sprite.sprite = FondBlanc;
            GetComponent<Image>().color = Color.blue;
            S_Type = 8;
            break;

        }

    }

    // Update is called once per frame
    void Update() {
    }
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
                Image NewForward;
                NewForward = (Image)Instantiate(ForwardPrefab, Slot.GetComponent<Pool>().ButtonsSpots[S_Type], Quaternion.identity);
                NewForward.transform.SetParent(Pool.PoolObject.transform, false);
                NewForward.rectTransform.localPosition = Slot.GetComponent<Pool>().ButtonsSpots[S_Type];
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
            if (transform.parent == GameObject.Find("MOC").transform)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion
}
