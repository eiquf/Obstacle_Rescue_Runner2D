using UnityEngine;
using UnityEngine.UI;

public class DictionaryPanelSpawn : IUIPanelsInstantiate
{
    private readonly int _indexOfSound = 0;

    private const int MINTAPS = 0;
    private int _currentTapsCount = 3;

    private string _textOfTheButton;

    private Text _text;
    private Button _button;
    public void Execute()
    {
        if (_currentTapsCount != MINTAPS)
        {//_soundEffectsAudio.PlayOneShot(_soundSetter.audioClips[_indexOfSound]);
            _currentTapsCount--;
            Text();
        }
    }
    private void Text()
    {
        _textOfTheButton = "x" + _currentTapsCount;
        _text.text = _textOfTheButton;
    }

    public void Execute(Transform transform)
    {
        
    }
}