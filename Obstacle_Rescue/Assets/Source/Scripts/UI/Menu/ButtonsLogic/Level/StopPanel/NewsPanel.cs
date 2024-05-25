using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class NewsPanel : MonoBehaviour
{
    public Action<bool> IsActivated;
    private RectTransform _panelRect;

    public float popOutDuration = 1f;
    public float popOutDistance = 200f;

    private float _fadeInDuration = 1f;
    private CanvasGroup _canvasGroup;

    private string _url;
    private string[] _urls = new string[]
    {
        "", "",""
    };
    private VideoPlayer _videoPlayer;
    private Button _button;
    private enum Scene { FirstLevel = 0, SecondLevel = 1, ThirdLevel = 2 }
    private void OnEnable() => IsActivated += Animation;
    private void OnDisable() => IsActivated -= Animation;
    private void Awake()
    {
        _panelRect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        Animation(true);
    }
    private void Start()
    {
        DOTween.timeScale = 1;

        _videoPlayer = GetComponentInChildren<VideoPlayer>();
        _button = GetComponentInChildren<Button>();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case (int)Scene.FirstLevel: _url = _urls[0]; break;
            case (int)Scene.SecondLevel: _url = _urls[1]; break;
            case (int)Scene.ThirdLevel: _url = _urls[2]; break;
        }
        _videoPlayer.url = _url;
        _button.onClick.AddListener(OpenYouTubeVideo);
    }
    private void OpenYouTubeVideo()
    {
        if (!string.IsNullOrEmpty(_url))
            Application.OpenURL(_url);
    }
    private void Animation(bool activation)
    {
        _canvasGroup.alpha = activation ? 0 : 1;
        float endValue = activation ? 1 : 0;
        _canvasGroup.DOFade(endValue, _fadeInDuration);

        Vector2 startPosition = activation ? new Vector2(Screen.width, 0f) : new Vector2(0f, 0f);
        _panelRect.anchoredPosition = startPosition;

        Vector2 targetPosition = activation ? new Vector2(0f, 0f) : new Vector2(Screen.width, 0f);

        DOTween.Sequence().Append(_panelRect.DOJump(targetPosition, 1f, 1, popOutDuration))
            .Join(_panelRect.DOAnchorPos(targetPosition, popOutDuration).SetEase(Ease.OutBack));
    }
}