using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Lecture : MonoBehaviour {

	#region Declarations
    [HideInInspector]
    public float moveDistance = 1.1f;
    [HideInInspector]
    public static Lecture LectureObject;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    GameObject CurrentFunction;
    GameObject LastFunction;

    public GameObject LevelDesign; // useless 
    [HideInInspector]
    public List<GameObject> LDList; //useless

    public GameObject MainCharacter;

    GameObject CurrentOrder;
    colorType CurrentOrderColor;
    int CurrentOrderNumber = 0;

    public GameObject CurrentCase;
    colorType CurrentCaseColor;
    GameObject FirstCase;

    GameObject CurrentSlot;
    float duration = 0.5f;

    public Image Highlight; //old

    Animator anim;

    GameObject _ParticulSystem;

    public GameObject _UI;
    public GameObject _VictoryScreen;

    public float AscnesionTime = 20; // speed of the final elevator animation

    static public bool  playing = false;


    bool dragged = false;
    bool tempFix = false; // i don't remember what it is for, get it clean xD
	bool tempfix2 = false; // don't remember too

	#endregion

	#region RedmetricsVariables
	int NumberOfTries = 1;
	float TimeToFinish = 0; //inSeconds

	float StartTime;
	float EndTime;
	#endregion
    // Use this for initialization
    void Awake()
    {
        _ParticulSystem = GameObject.Find("PS");
        LectureObject = this;
    }

    void Start () {
		StartTime = Time.time;
        moveDistance = 1.1f;
        FirstCase = CurrentCase;
        foreach (Transform child in LevelDesign.transform) //useless
        {
            LDList.Add(child.gameObject);
        }
        CurrentFunction = F1;
        CurrentOrderNumber = 0;
        CurrentSlot = F1.transform.GetChild(0).gameObject;
        anim = MainCharacter.GetComponent<Animator>();

        MainCharacter.transform.position = FirstCase.transform.position + new Vector3(0, 2, 0);
        MainCharacter.transform.rotation = FirstCase.transform.rotation;
		NumberOfTries++;
    }

    public void FirstPlay() //when you click on play button
    {
        if (!playing && tempFix == false)
        {

            if (MainCharacter.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                return;
            }
            tempFix = true;
			playing = true;
            Play();
            
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
			tempfix2 = true;
        }

        

        CurrentCaseColor = CurrentCase.GetComponent<Case>().CaseColor;
        CurrentOrderColor = CurrentOrder.GetComponent<Button>().BlockColor;

        Debug.Log("Current Case Color : " + CurrentCaseColor);
        Debug.Log("Current Action Color : " + CurrentOrderColor);

        if (CurrentOrderColor == colorType.none)
        {
            ExecuteOrder(CurrentOrder.GetComponent<Button>().ButtonType);
        }
        else if (CurrentOrderColor == CurrentCaseColor)
        {
            ExecuteOrder(CurrentOrder.GetComponent<Button>().ButtonType);
        }
        else
        {
            Debug.Log("Order Ignored");
            StartCoroutine("WaitAndNext", 0);
        }
        
       
    }

    IEnumerator WaitAndNext(float x)
    {
        yield return new WaitForSeconds(x);;
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
			if (CurrentSlot.transform.childCount != 0 && tempfix2 == true)
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
        MainCharacter.transform.DOMove(MainCharacter.transform.position + MainCharacter.transform.forward.normalized * moveDistance, duration).SetEase(Ease.Linear);
    }
    void Rotate(int Angle)
    {
        MainCharacter.transform.DORotate(new Vector3(0, Angle, 0), duration, RotateMode.LocalAxisAdd);
    }


    void Victory()
    {
		EndTime = Time.time;
		TimeToFinish = EndTime - StartTime;

		//Redmetrics
		if (GameObject.FindGameObjectWithTag("Redmetrics") !=null){
		CustomData _customData = new CustomData();
		_customData.Add("Level Finished", "Level " + Application.loadedLevel + " finished, " +
		                			"Time Taken to finish : " + (int)TimeToFinish + " seconds, " + 
		                		    "Number of trials : " + NumberOfTries);

		RedMetricsManager.get().sendEvent(TrackingEvent.LEVELFINISHED, _customData);
		}
		//

        _UI.SetActive(false);
        foreach (Transform child in _ParticulSystem.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        CurrentCase.transform.DOLocalMoveY(10, AscnesionTime);
        StartCoroutine("KatyPerry");
    }

    IEnumerator KatyPerry() // after fireworkds
    {
        yield return new WaitForSeconds(4);
        
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
