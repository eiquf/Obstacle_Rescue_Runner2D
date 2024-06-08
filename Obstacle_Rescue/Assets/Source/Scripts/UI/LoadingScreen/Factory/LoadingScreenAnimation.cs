using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.UI;

public class LoadingScreenAnimation : LoadingScreen<IEnumerator>
{
    private readonly Text _loadingText;
    public LoadingScreenAnimation(Text text) => _loadingText = text;
    public override async Task Execute()
    {
        while (true)
        {
            _loadingText.text = "Loading" + _loadingDotSequence[_currentDotIndex];
            _currentDotIndex = (_currentDotIndex + 1) % _loadingDotSequence.Length;
            await Task.Delay(TimeSpan.FromSeconds(_dotAnimationSpeed));
        }
    }
}