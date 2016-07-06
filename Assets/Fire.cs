using UnityEngine;
using System.Collections;

public class Fire : BehaviourSingleton<Fire>
{
    public GameObject tooltip;
    public bool On;
    public GameObject OnObj;
    public BlockGuyDialog BlockGuyDialog;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!Dialoguer.GetGlobalBoolean(0))
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f) && hit.transform == this.transform)
        {
            tooltip.SetActive(true);

            // light fire
            if (Input.GetMouseButtonDown(0))
            {
                OnObj.SetActive(true);
                tooltip.SetActive(false);
                Dialoguer.SetGlobalBoolean(1, true);
                BlockGuyDialog.startedDialogForDay = false;
                BlockGuyDialog.GetComponentInChildren<DialogController>().DialogRunning = false;
                Dialoguer.SetGlobalBoolean(0, false);
            }
        }
        else
            tooltip.SetActive(false);
    }
}