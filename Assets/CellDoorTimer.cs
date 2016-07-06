using UnityEngine;
using System.Collections;

public class CellDoorTimer : MonoBehaviour
{
    public GameObject light;
    public AudioClip audioDoorBoom, audioDoorOpen, audioFinale;
    public AudioSource audioSrc;
    public Canvas canvas;

	void Start () {
	}
	
	void Update () {
	    if (Dialoguer.GetGlobalBoolean(2) == true)
	    {
	        StartCoroutine(OpenDoor());
	        Dialoguer.SetGlobalBoolean(2, false);
	    }
	}

    IEnumerator OpenDoor()
    {
        audioSrc.PlayOneShot(audioDoorBoom);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        audioSrc.PlayOneShot(audioDoorOpen);
        yield return new WaitForSeconds(3.0f);
        while (transform.position.y < 4.5)
        {
            transform.position += Vector3.up * 0.01f;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(4.0f);

        // end demo

        // show day text
        canvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "END";
        canvas.gameObject.SetActive(true);
        audioSrc.PlayOneShot(audioFinale);
    }
}
