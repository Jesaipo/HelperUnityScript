using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

using MyBox;
using System.Collections.Generic;

public class FadeHelper : MonoBehaviour
{

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DebugFadeIn()
    {
        StartFadeIn();
    }

    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DebugFadeOut()
    {
        StartFadeOut();
    }

    public float FadeDuration = 2.0f;

    public bool FadeInOnEnable = false;

    public bool FadeInOnStart = false;
    public bool FadeOutOnStart = false;

    public bool FadeAllChildren = false;
    [ConditionalField(nameof(FadeAllChildren))]
    public List<Image> m_ExcluedImageList = new List<Image>();

    [ConditionalField(nameof(FadeAllChildren), inverse: true)]
    public Image img;

    public UnityEvent m_FadeOver;

    private void OnEnable()
    {
        if(FadeInOnEnable)
        {
            StartFadeIn();
        }
    }

    private void Start()
    {
        if(FadeInOnStart)
        {
            StartFadeIn();
        }

        if (FadeOutOnStart)
        {
            StartFadeOut();
        }
    }

    public void StartFadeIn()
    {
        if(!this.isActiveAndEnabled)
        {
            return;
        }

        if (FadeAllChildren)
        {
            foreach(Image im in GetComponentsInChildren<Image>())
            {
                if (!m_ExcluedImageList.Contains(im))
                {
                    StartCoroutine(FadeImage(false, im));
                }
            }

            foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>())
            {
                    StartCoroutine(FadeSprite(false, sp));
            }

            foreach (TMPro.TextMeshProUGUI text in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                StartCoroutine(FadeText(false, text));
            }
        }
        else
        {
            StartCoroutine(FadeImage(false, img));
        }

        StartCoroutine(CallFadeOver());
    }

    public void StartFadeOut()
    {
        if (!this.isActiveAndEnabled)
        {
            return;
        }
        if (FadeAllChildren)
        {
            foreach (Image im in GetComponentsInChildren<Image>())
            {
                if (!m_ExcluedImageList.Contains(im))
                {
                    StartCoroutine(FadeImage(true, im));
                }
            }


            foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(FadeSprite(true, sp));
            }

            foreach (TMPro.TextMeshProUGUI text in GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                StartCoroutine(FadeText(true, text));
            }
        }
        else
        {
            StartCoroutine(FadeImage(true, img));
        }

        StartCoroutine(CallFadeOver());
    }

    IEnumerator CallFadeOver()
    {
        float cdTime = FadeDuration;

        while (cdTime > 0)
        {
            yield return new WaitForFixedUpdate();
            cdTime -= Time.deltaTime;
        }
        m_FadeOver.Invoke();
    }

    IEnumerator FadeImage(bool fadeAway, Image target)
    {
        float min = 0f;
        float max = 100f;
        FadeHelperException excp;
        if (target.TryGetComponent<FadeHelperException>(out excp))
        {
            min = excp.m_MinAlpha;
            max = excp.m_MaxAlpha;
        }
        //normalized
        min /= 100f;
        max /= 100f;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha

                //float alpha = i / FadeDuration;
                float alpha = min + (max - min) * (i / FadeDuration);
                if (alpha < target.color.a)
                {
                    target.color = new Color(target.color.r, target.color.g, target.color.b, alpha);
                }
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, min);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= FadeDuration; i += Time.deltaTime)
            {
                // set color with i as alpha
                //float alpha = i / FadeDuration;
                float alpha = min + (max - min) * (i / FadeDuration);
                if (alpha > target.color.a)
                {
                    target.color = new Color(target.color.r, target.color.g, target.color.b, alpha);
                }
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, max);
        }
    }


    IEnumerator FadeSprite(bool fadeAway, SpriteRenderer target)
    {
        float min = 0f;
        float max = 100f;
        FadeHelperException excp;
        if (target.TryGetComponent<FadeHelperException>(out excp))
        {
            min = excp.m_MinAlpha;
            max = excp.m_MaxAlpha;
        }
        //normalized
        min /= 100f;
        max /= 100f;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha

                //float alpha = i / FadeDuration;
                float alpha = min + (max - min) * (i / FadeDuration);
                if (alpha < target.color.a)
                {
                    target.color = new Color(target.color.r, target.color.g, target.color.b, alpha);
                }
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, min);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= FadeDuration; i += Time.deltaTime)
            {
                // set color with i as alpha
                //float alpha = i / FadeDuration;
                float alpha = min + (max - min) * (i / FadeDuration);
                if (alpha > target.color.a)
                {
                    target.color = new Color(target.color.r, target.color.g, target.color.b, alpha);
                }
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, max);
        }
    }

    IEnumerator FadeText(bool fadeAway, TMPro.TextMeshProUGUI target)
    {
        target.overrideColorTags = true;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = FadeDuration; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                
                target.color = new Color(target.color.r, target.color.g, target.color.b, i/FadeDuration);
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= FadeDuration; i += Time.deltaTime)
            {
                // set color with i as alpha

                target.color = new Color(target.color.r, target.color.g, target.color.b, i / FadeDuration);
                yield return null;
            }
            target.color = new Color(target.color.r, target.color.g, target.color.b, 1);
        }

    }
}
