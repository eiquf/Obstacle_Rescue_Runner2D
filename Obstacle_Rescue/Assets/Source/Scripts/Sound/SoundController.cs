using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundController : MonoBehaviour
{
    public Action<int> IsSoundPlay;
    public Action<int> IsSoundVolumeChanged;
    public Action<Image[]> IsImagesSetted;
    #region Scripts
    private readonly SoundSpritesLoader _spritesLoader = new();
    private readonly SoundLoader _soundLoader = new();

    private readonly SoundInitialization _SoundInitialization = new();
    private readonly SoundImagesInitialization _soundImageInitialization = new();

    private readonly BGM _bgm = new();
    private readonly SFX _sfx = new();
    #endregion
    public Image[] ButtonsImages { get; private set; } = new Image[2];
    public Sprite[] Sprites { get; private set; }
    public AudioClip[] AudioClips { get; private set; }
    [field: SerializeField] public AudioSource[] AudioSources { get; private set; }

    private void OnEnable()
    {
        IsSoundPlay -= SoundPlay;
        IsSoundVolumeChanged += ChangeSoundVolume;
        IsImagesSetted -= LoadImages;
    }
    private void OnDisable()
    {
        IsSoundPlay -= SoundPlay;
        IsSoundVolumeChanged -= ChangeSoundVolume;
        IsImagesSetted -= LoadImages;
    }
    private void Awake()
    {
        LoadComponents();
        Initialization();
    }
    private void Initialization() => _SoundInitialization.Execute(this);
    private void LoadComponents()
    {
        Sprites = _spritesLoader.Execute();
        AudioClips = _soundLoader.Execute();
    }
    private void LoadImages(Image[] images)
    {
        ButtonsImages[UIButtonsCount.SFX] = images[UIButtonsCount.SFX];
        ButtonsImages[UIButtonsCount.BGM] = images[UIButtonsCount.BGM];

        _soundImageInitialization.Execute(this);
    }
    private void SoundPlay(int index) => new SoundSFXPlay(this, index);
    private void ChangeSoundVolume(int index)
    {
        switch (index)
        {
            case UIButtonsCount.SFX: SFX(); break;
            case UIButtonsCount.BGM: BGM(); break;
        }
    }
    #region Buttons Logic
    private void SFX() => _sfx.Execute(this);
    private void BGM() => _bgm.Execute(this);
    #endregion
}