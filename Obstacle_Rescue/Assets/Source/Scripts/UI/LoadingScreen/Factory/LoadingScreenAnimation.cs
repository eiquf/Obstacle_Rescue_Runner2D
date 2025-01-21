using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenAnimation :IAnimation
{
    private readonly float _dotAnimationSpeed = 0.2f;
    private readonly Text _text;

    public LoadingScreenAnimation(Text text) => _text = text;
    public void PlayAnimation(Transform transform)
    {
        Sequence dotSequence = DOTween.Sequence();
        transform.gameObject.SetActive(true);

        string loadingTextBase = "Loading";

        dotSequence.AppendCallback(() =>
        {
            int i = 1;
            Action updateText = () =>
            {
                _text.text = loadingTextBase + new string('.', i);
                i = (i % 3) + 1;
            };

            DOTween.To(() => 0f, x => { updateText(); }, 1f, _dotAnimationSpeed).SetLoops(-1, LoopType.Restart);
        });

        dotSequence.SetLoops(-1, LoopType.Restart);
    }
}