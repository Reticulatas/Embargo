using System.Collections;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class RollText : MonoBehaviour
{
    string wantedText = "";
    public TMPro.TextMeshPro target;
    public float letterPause = 0.2f;
    public float donePause = 0.1f;
    public System.Action OnDone;
    public AudioSource audioSrc;
    public float audioPitchRange = 0.1f;
    private float audioPitch;

    public string text
    {
        get
        {
            return wantedText;
        }

        set
        {
            wantedText = value;
            StopAllCoroutines();
            StartCoroutine(TypeText());
        }
    }

    void Start()
    {
        target.text = wantedText;
        if (audioSrc != null)
            audioPitch = audioSrc.pitch;
    }

    IEnumerator TypeText()
    {
        target.text = "";
        bool tag = false;
        string tagStr = "";
        foreach (char letter in wantedText.ToCharArray())
        {
            if (letter == '<')
                tag = true;
            if (tag)
            {
                tagStr += letter;
                if (letter == '>')
                {
                    tag = false;
                    target.text += tagStr;
                    tagStr = "";
                    continue;
                }
                continue;
            }
            // play sound
            if (audioSrc != null)
            {
                audioSrc.pitch = audioPitch + Random.Range(-audioPitchRange, audioPitchRange);
                audioSrc.Play();
            }

            target.text += letter;
            yield return new WaitForSeconds(letterPause);
        }


        yield return new WaitForSeconds(donePause);
        if (OnDone != null)
            OnDone();
    }
}