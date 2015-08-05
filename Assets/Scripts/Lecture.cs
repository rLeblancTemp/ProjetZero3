using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Lecture : MonoBehaviour {

    [HideInInspector]
    public float moveDistance = 1.1f;
    [HideInInspector]
    public static Lecture LectureObject;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    GameObject CurrentFunction;
    GameObject LastFunction;

    public GameObject LevelDesign;
    [HideInInspector]
    public List<GameObject> LDList;

    public GameObject MainCharacter;

    GameObject CurrentOrder;
    colorType CurrentOrderColor;
    int CurrentOrderNumber = 0;

    public GameObject CurrentCase;
    colorType CurrentCaseColor;
    GameObject FirstCase;

    GameObject CurrentSlot;
    float duration = 0.5f;

    public Image Highlight;

    Animator anim;

    GameObject _ParticulSystem;

    public GameObject _UI;
    public GameObject _VictoryScreen;

    public float AscnesionTime = 20;

    static public bool  playing = false;


    bool dragged = false;
    bool tempFix = false;

    // Use this for initialization
    void Awake()
    {
        _ParticulSystem = GameObject.Find("PS");
        LectureObject = this;
    }

    void Start () {
        moveDistance = 1.1f;
        FirstCase = CurrentCase;
        foreach (Transform child in LevelDesign.transform)
        {
            LDList.Add(child.gameObject);
        }
        CurrentFunction = F1;
        CurrentOrderNumber = 0;
        CurrentSlot = F1.transform.GetChild(0).gameObject;
        anim = MainCharacter.GetComponent<Animator>();

        MainCharacter.transform.position = FirstCase.transform.position + new Vector3(0, 2, 0);
        MainCharacter.transform.rotation = FirstCase.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {


    }

    public void FirstPlay()
    {
     
        //CurrentOrder = F1.transform.GetChild(0).GetChild(0).gameObject;
        //CurrentOrderColor = CurrentOrder.GetComponent<Button>().BlockColor;
        if (!playing && tempFix == false)
        {

            if (MainCharacter.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                return;
            }
            tempFix = true;
            Play();
            playing = true;
        }
        else
        {
            foreach (Transform obj in F1.transform)
            {
                if (obj.childCount!=0)
                obj.GetChild(0).transform.localScale = Vector3.one;
            }
            foreach (Transform obj in F2.transform)
            {
                if (obj.childCount != 0)
                    obj.GetChild(0).transform.localScale = Vector3.one;
            }
            foreach (Transform obj in F3.transform)
            {
                if (obj.childCount != 0)
                    obj.GetChild(0).transform.localScale = Vector3.one;
            }
            MainCharacter.GetComponent<Rigidbody>().velocity = Vector3.zero;
            MainCharacter.transform.DOKill();
            StopAllCoroutines();
            CurrentFunction = F1;
            CurrentOrderNumber = 0;
            CurrentSlot = F1.transform.GetChild(0).gameObject;
            //Highlight.transform.position = new Vector3(-10000, -1000, -1000);
            MainCharacter.transform.position = FirstCase.transform.position + new Vector3(0, 2, 0);
            MainCharacter.transform.rotation = FirstCase.transform.rotation;
            anim.SetBool("movingForward", false);
            anim.SetBool("turnLeft", false);
            anim.SetBool("turnRight", false);
            playing = false;
            tempFix = false;
        }
    }

    public void Play()
    {
        if (MainCharacter.GetComponent<Character>().ObjectUnderFoots == null)
        {
            return;
        }
        if (CurrentSlot.transform.childCount == 0)
        {
            Debug.Log("No Order in the Slot");
            StartCoroutine("WaitAndNext", 0);
            return;
        }
        else
        {
            CurrentOrder = CurrentSlot.transform.GetChild(0).gameObject;
        }

        

        CurrentCaseColor = CurrentCase.GetComponent<Case>().CaseColor;
        CurrentOrderColor = CurrentOrder.GetComponent<Button>().BlockColor;

        Debug.Log("Current Case Color : " + CurrentCaseColor);
        Debug.Log("Current Action Color : " + CurrentOrderColor);

        if (CurrentOrderColor == colorType.none)
        {
            ExecuteOrder(CurrentOrder.GetComponent<Button>().ButtonType);
            //Highlight.transform.position = CurrentSlot.transform.position;
        }
        else if (CurrentOrderColor == CurrentCaseColor)
        {
            ExecuteOrder(CurrentOrder.GetComponent<Button>().ButtonType);
            //Highlight.transform.position = CurrentSlot.transform.position;
        }
        else
        {
            Debug.Log("Order Ignored");
            StartCoroutine("WaitAndNext", 0);
        }
        
       
    }

    IEnumerator WaitAndNext(float x)
    {
        //Debug.Log("CoroutineStart : " + Time.time);
        yield return new WaitForSeconds(x);
       
        //Debug.Log("CoroutineEnd : " + Time.time);
        anim.SetBool("movingForward", false);
        anim.SetBool("turnLeft", false);
        anim.SetBool("turnRight", false);
        Debug.Log("//////////////////////// NEXT ORDER ////////////////////////");
        NextOrder();
    }

    public void Reset()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void NextOrder()
    {
        if (CurrentCase.tag == "LastCase")
        {
            Debug.Log("Victory!");
            Victory();
            return;
        }

        if (CurrentOrderNumber + 1 < CurrentFunction.transform.childCount)
        {
            CurrentOrderNumber++;
            CurrentSlot = CurrentFunction.transform.GetChild(CurrentOrderNumber).gameObject;
            //Debug.Log(Time.time);
            if (CurrentSlot.transform.childCount != 0)
            {
                CurrentOrder.transform.DOScale(1f, 0.1f);
            }
            Play();
        } else
        {
            playing = false;
            Debug.Log("End of the function");
        }
    }


    void ExecuteOrder(Button.type OrderType)
    {
        Debug.Log("Executing order : " + OrderType);
        CurrentOrder.transform.DOScale(1.5f, 0.1f);
        switch (OrderType)
        {
            case Button.type.Forward:
            anim.SetBool("movingForward", true);
            GoForward();
                StartCoroutine("WaitAndNext", duration);
            break;

            case Button.type.Left:
            Rotate(-90);
            anim.SetBool("turnLeft", true);
            StartCoroutine("WaitAndNext", duration);
            break;

            case Button.type.Right:
            Rotate(90);
            anim.SetBool("turnRight", true);
            StartCoroutine("WaitAndNext", duration);
            break;

            case Button.type.A:
            CurrentFunction = F1;
            CurrentOrderNumber = -1;
            StartCoroutine("WaitAndNext", duration * 0.5f);
            break;

            case Button.type.B:
            CurrentFunction = F2;
            CurrentOrderNumber = -1;
            StartCoroutine("WaitAndNext", duration * 0.5f);
            break;

            case Button.type.C:
            CurrentFunction = F3;
            CurrentOrderNumber = -1;
            StartCoroutine("WaitAndNext", duration * 0.5f);
            break;
        }
    }

    void GoForward()
    {
        //Debug.Log(Time.time);
        MainCharacter.transform.DOMove(MainCharacter.transform.position + MainCharacter.transform.forward.normalized * moveDistance, duration).SetEase(Ease.Linear);
    }
    void Rotate(int Angle)
    {
        // MainCharacter.transform.Rotate(new Vector3(0, Angle, 0));
        MainCharacter.transform.DORotate(new Vector3(0, Angle, 0), duration, RotateMode.LocalAxisAdd);
    }


    void Victory()
    {
        _UI.SetActive(false);
        foreach (Transform child in _ParticulSystem.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        CurrentCase.transform.DOLocalMoveY(10, AscnesionTime);
        StartCoroutine("KatyPerry");
        //en cas de victoire;
    }

    IEnumerator KatyPerry()
    {
        yield return new WaitForSeconds(5);
        
        _VictoryScreen.SetActive(true);
        _ParticulSystem.transform.parent.gameObject.GetComponent<BlurOptimized>().enabled = true;
    }

    public void NextLevel()
    {
        //RedMetricsManager.get().sendEvent(TrackingEvent.LEVELFINISHED, "string");
        playing = false;
        Application.LoadLevel(Application.loadedLevel +1);
    }

    public void GotoLevelSelection()
    {
        playing = false;
        Application.LoadLevel("LevelSelection");
    }
}
