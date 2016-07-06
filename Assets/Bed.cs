using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour
{
    public GameObject tooltip;
    public float BedCooldown = -1.0f;

    void Start()
    {
    }

    private void FixedUpdate()
    {
        if (BedCooldown > 0)
        {
            BedCooldown -= Time.deltaTime;
        }

        RaycastHit hit;
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 50.0f);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3.0f) && hit.transform == this.transform)
        {
            if (BedCooldown < 10.0f)
            {
                if (BedCooldown > 0)
                    tooltip.GetComponent<TMPro.TextMeshPro>().text = "Not tired yet";
                tooltip.SetActive(true);
            }

            if (BedCooldown < 0)
            {
                tooltip.GetComponent<TMPro.TextMeshPro>().text = "Pass the day";
                // sleep
                if (Input.GetMouseButtonDown(0))
                {
                    DayController.get().AdvanceDay();
                    tooltip.SetActive(false);
                    BedCooldown = 20.0f;
                }
            }
        }
        else
            tooltip.SetActive(false);
    }
}
