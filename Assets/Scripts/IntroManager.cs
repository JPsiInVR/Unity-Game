using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Playables;

public class IntroManager : MonoBehaviour
{
    private List<Intro> _intros;
    private void Awake()
    {
        _intros = new List<Intro>(GetComponentsInChildren<Intro>(includeInactive: true));
        _intros.ForEach(intro => intro.gameObject.SetActive(false));
    }

    public IEnumerator ShowIntroWithText(string[] introTexts, PlayableDirector playableDirector)
    {
        playableDirector.Pause();

        for (int i = 0; i < introTexts.Length; i++)
        {
            var introSequence = DOTween.Sequence();

            for (int j = 0; j < _intros.Count; j++)
            {
                _intros[j].gameObject.SetActive(true);
                _intros[j].Text.text = introTexts[i];

                if (j == 0)
                {
                    introSequence.Append(_intros[j].Text.DOFade(1, 1f));
                }
                else
                {
                    introSequence.Join(_intros[j].Text.DOFade(1, 1f));
                }
            }

            introSequence.AppendInterval(2);

            //fade out
            for (int j = 0; j < _intros.Count; j++)
            {
                if (j == 0)
                {
                    introSequence.Append(_intros[j].Text.DOFade(0, 1f));
                }
                else
                {
                    introSequence.Join(_intros[j].Text.DOFade(0, 1f));
                }

                if (i == introTexts.Length - 1)
                {
                    introSequence.Join(_intros[j].Background.DOFade(0, 1));

                    introSequence.OnComplete(() =>
                    {
                        foreach(var intro in _intros)
                        {                            
                            intro.Background.color = new Color(intro.Background.color.r, intro.Background.color.g, intro.Background.color.b, 1);
                            intro.gameObject.SetActive(false);
                        }

                        playableDirector.Play();
                    });
                }
            }

            introSequence.Play();

            yield return introSequence.WaitForCompletion();
        }
    }
}
