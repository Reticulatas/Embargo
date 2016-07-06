using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CinematicEffects;

public class DayController : BehaviourSingleton<DayController>
{
    public int Day = 1;
    public FirstPersonController Player;
    public GameObject BlockGuy, DeadGuy, Fire, FireSmall;
    public LensAberrations Vignette;
    public Canvas canvas;
    public AudioClip audioDaySwitch;
    public AudioSource audioSrc;

    private float vignetteIntensity;
    private bool skipFadeOut = true;
    public bool skip;

    public event Action<int> OnDayChanged;

	void Start () {
        canvas.gameObject.SetActive(false);
	    vignetteIntensity = Vignette.vignette.intensity;
        AdvanceDay(Day);
	}

    public void AdvanceDay(int SetDay = -1)
    {
        if (SetDay == -1)
            ++Day;
        else
            Day = SetDay;
        Debug.Log("Day " + Day);

        if (!skip)
            StartCoroutine(DayAdvance());
        else
            SetupDay();
    }

    IEnumerator DayAdvance()
    {
        Player.enabled = false;

        if (!skipFadeOut)
        {
            // fade out
            while (Vignette.vignette.intensity < 3)
            {
                Vignette.vignette.intensity += 0.01f;
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForFixedUpdate();

        }
        skipFadeOut = false;

        // show day text
        canvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Day " + Day.ToString();
        canvas.gameObject.SetActive(true);
        audioSrc.PlayOneShot(audioDaySwitch);

        // do setup
        SetupDay();

        yield return new WaitForSeconds(3.0f);

        canvas.gameObject.SetActive(false);

        yield return new WaitForFixedUpdate();

        // fade in
        while (Vignette.vignette.intensity > vignetteIntensity)
        {
                Vignette.vignette.intensity -= 0.01f;
            yield return new WaitForFixedUpdate();
        }
        Vignette.vignette.intensity = vignetteIntensity;
        Vignette.vignette.center = new Vector2(0.5f,0.5f);

        Player.enabled = true;

        yield return new WaitForFixedUpdate();

    }
	
    void SetupDay()
    {
        DeadGuy.SetActive(false);
        if (Dialoguer.ready)
            Dialoguer.EndDialogue();

        switch (Day)
        {
            case 2:
                break;
            case 4:
                Fire.SetActive(false);
                FireSmall.SetActive(true);
                break;
            case 5:
            case 6:
                BlockGuy.SetActive(false);
                break;
            case 7:
            case 8:
                BlockGuy.SetActive(false);
                DeadGuy.SetActive(true);
                break;
        }

        if (OnDayChanged != null)
            OnDayChanged(Day);
    }
}
