using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public sealed class Education : MonoBehaviour
{
    private enum Components { Text = 0, Video = 1 }

    private string[] sentencesList =
    {
        "Hello!",
        "You should tap the screen to jump and avoid the falling!",
        "Collect all needed letters and put them in correct slots to create a word!",
        "You have a health scale. The less health you have, the closer you are to defeat!",
        "This is a dictionary. You can use it if you don't know the correct word.",
        "The usage limit per level is 3. Use it wisely!",
        "Good luck!"
    };
    private string[] signs = new string[] { ".", ",", "!", "?" };
    private string _currentString;
    private string _sign;

    private float _firstSec = 0.01f;
    private float _secondSec = 0.02f;
    private int _currentIndex = 0;
    private int _touchCount;

    private bool _canTap = false;

    private Text _text;

    #region Video
    private string[] _videosAPI = new string[]
    {
        "JumpTutorial",
        "HealthSystemTutorial",
        "DictionaryTutorial",
        "InputWordsTutorial"
    };
    private VideoClip[] videos;
    private RawImage _videoPlayerObj;
    private VideoPlayer _videoPlayer;
    #endregion

    private EiquifAnimation _eiquifAnimation;
    private void OnEnable()
    {
        _text = transform.GetChild((int)Components.Text).GetComponent<Text>();
        //_videoPlayerObj = transform.GetChild((int)Components.Video).GetComponent<RawImage>();
    }
    private void Awake()
    {
        Time.timeScale = 1;

        _eiquifAnimation = transform.GetChild(2).GetComponent<EiquifAnimation>();
        LoadVideoTutorials();
        //_videoPlayerObj.gameObject.SetActive(false);
    }
    private void LoadVideoTutorials()
    {

    }
    private void Start() => Initialization();
    private void Update()
    {
        if (_touchCount != 2 && !_canTap)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                switch (_touchCount)
                {
                    case 0: FirstTouch(); break;
                    case 1: SecondTouch(); break;
                }
            }
        }
    }
    private void Initialization()
    {
        _canTap = true;
        _currentIndex = 0;
        TextIndex();
        StartCoroutine(TextOutputCoroutine());
    }
    private void FirstTouch()
    {
        int randomAnimIndex = Random.Range(0, 1);
        //_eiquifAnimation.EiquifChangeAnim?.Invoke(randomAnimIndex);

        StartCoroutine(TextOutputCoroutine());
        CurretnIndex();
        TextIndex();
        VideoIndex();

        _touchCount = 1;
    }
    private void SecondTouch()
    {
        _firstSec = 0;
        _secondSec = 0;
        _touchCount = 2;
    }
    private void CurretnIndex()
    {
        _currentIndex += 1;
        if (_currentIndex > 6) _currentIndex = 6;

        if (_currentIndex == 6)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
    private void TextIndex()
    {
        _text.text = sentencesList[_currentIndex];
        _currentString = _text.text;
        _text.text = "";
    }
    private void VideoIndex()
    {
        //switch (_currentIndex)
        //{
        //    case 2:
        //        _videoPlayerObj.gameObject.SetActive(true);
        //        _videoPlayer = transform.GetChild(1).GetComponent<VideoPlayer>();
        //        _videoPlayer.clip = videos[0]; break;
        //    case 3: _videoPlayer.clip = videos[1]; break;
        //    case 4: _videoPlayer.clip = videos[2]; break;
        //    case 5: _videoPlayer.clip = videos[3]; break;
        //}
    }
    private IEnumerator TextOutputCoroutine()
    {
        foreach (char abc in _currentString)
        {
            _text.text += abc;
            _sign = abc.ToString();

            if (_sign == (signs[0]).ToString()
             || _sign == (signs[1]).ToString()
             || _sign == (signs[2]).ToString()
             || _sign == (signs[3]).ToString())
            {
                yield return new WaitForSeconds(_firstSec);
            }
            else yield return new WaitForSeconds(_secondSec);
        }
        yield return new WaitForSeconds(1);
        _touchCount = 0;
        _canTap = true;
    }
}