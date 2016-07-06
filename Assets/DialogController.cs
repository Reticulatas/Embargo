using System;
using UnityEngine;
using System.Collections;
using DialoguerEditor;

public class DialogController : MonoBehaviour
{
    public RollText text;
    public float RaycastDist = 10.0f;
    public bool DialogRunning = false;
    public GameObject Click;

    void Awake()
    {
        Dialoguer.Initialize();
    }

	void Start () {
	    Dialoguer.events.onEnded += ClearText;
	    Dialoguer.events.onInstantlyEnded += ClearText;
        Dialoguer.events.onTextPhase += EventsOnOnTextPhase;
        Dialoguer.events.onWaitStart += EventsOnOnWaitStart;
        Dialoguer.events.onWaitComplete += Events_onWaitComplete;
        text.OnDone += () =>
        {
            if (DialogRunning)
                Dialoguer.ContinueDialogue(); 
        };
        DayController.get().OnDayChanged += DialogController_OnDayChanged;
    }

    private void DialogController_OnDayChanged(int obj)
    {
        DialogRunning = false;
    }

    private void Events_onWaitComplete()
    {
    }

    private void EventsOnOnWaitStart()
    {
    }

    void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0) && !DialogRunning)
        {
            RaycastHit hit;
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 50.0f);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, RaycastDist,
                1 << 8))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform == this.transform)
                {
                    Dialoguer.ContinueDialogue();
                    DialogRunning = true;
                }
            }
        }

        Click.SetActive(!DialogRunning);
    }

    private void EventsOnOnTextPhase(DialoguerTextData data)
    {
        text.text = data.text;
    }

    private void ClearText()
    {
        text.text = "";
    }
}
