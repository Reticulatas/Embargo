using System;
using UnityEngine;
using System.Collections;

public class BlockGuyDialog : MonoBehaviour
{

    public bool startedDialogForDay = false;
    public float DistanceThresholdForDialog = 5.0f;

    void Start()
    {
        DayController.get().OnDayChanged += BlockGuyDialog_OnDayChanged;
    }

    private void BlockGuyDialog_OnDayChanged(int day)
    {
        startedDialogForDay = false;
    }

    void FixedUpdate()
    {
        if (!startedDialogForDay)
        {
            var d = Vector3.Dot(this.transform.forward, Camera.main.transform.forward);
            if (d > 0.5f && Vector3.Distance(transform.position, Camera.main.transform.position) < DistanceThresholdForDialog)
            {
                Dialoguer.StartDialogue(
                    (DialoguerDialogues)
                        Enum.Parse(typeof (DialoguerDialogues), "BlockGuy" + DayController.get().Day.ToString()));

                startedDialogForDay = true;
            }
        }
    }
}
