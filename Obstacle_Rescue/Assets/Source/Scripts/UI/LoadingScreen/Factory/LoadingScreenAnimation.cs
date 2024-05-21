using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenAnimation : LoadingScreen<IEnumerator>
{
    private Text _loadingText;
    public LoadingScreenAnimation(Text text) => _loadingText = text;
    public override IEnumerator Execute()
    {
        while (true)
        {
            _loadingText.text = "Loading" + _loadingDotSequence[_currentDotIndex];
            _currentDotIndex = (_currentDotIndex + 1) % _loadingDotSequence.Length;
            yield return new WaitForSeconds(_dotAnimationSpeed);
        }
    }
}