using System;
using System.Threading.Tasks;
using UnityEngine.UI;

public class LoadingScreenAnimation : LoadingScreen<Text>
{
    public override async Task Execute(Text thing)
    {
        while (true)
        {
            thing.text = "Loading" + _loadingDotSequence[_currentDotIndex];
            _currentDotIndex = (_currentDotIndex + 1) % _loadingDotSequence.Length;
            await Task.Delay(TimeSpan.FromSeconds(_dotAnimationSpeed));
        }
    }
}