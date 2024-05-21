using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundController : MonoBehaviour
{
    public Action<int> IsSoundPlay;
    public Action<int> IsSoundVolumeChanged;
    #region Scripts
    private readonly SoundSpritesLoader _spritesLoader = new();
    private readonly SoundLoader _soundLoader = new();

    private readonly SoundInitialization _SoundInitialization = new();

    private readonly BGM _bgm = new();
    private readonly SFX _sfx = new();

    private SoundSFXPlay _soundSFXPlay;
    #endregion
    public Image[] ButtonsImages { get; private set; } = new Image[2];
    public Sprite[] Sprites { get; private set; }
    public AudioClip[] AudioClips { get; private set; }
    [field: SerializeField] public AudioSource[] AudioSources { get; private set; }

    private void OnEnable()
    {
        IsSoundPlay -= SoundPlay;
        IsSoundVolumeChanged += ChangeSoundVolume;
    }
    private void OnDisable()
    {
        IsSoundPlay -= SoundPlay;
        IsSoundVolumeChanged -= ChangeSoundVolume;

    }
    private void Awake()
    {
        LoadComponents();
        Initialization();
    }
    private void Initialization()
    {
        _SoundInitialization.Execute(this);
    }
    private void LoadComponents()
    {
        Sprites = _spritesLoader.Execute();
        AudioClips = _soundLoader.Execute();
    }
    private void SoundPlay(int index) => _soundSFXPlay = new(this, index);
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