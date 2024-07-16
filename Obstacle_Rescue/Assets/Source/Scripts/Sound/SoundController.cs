using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundController : MonoBehaviour
{
    public Action<int> IsSoundPlay;
    public Action<int> IsSoundVolumeChanged;
    public Action<Image> IsImagesSet;

    private readonly BGM _bgm = new();
    private readonly SFX _sfx = new();
    private SoundSFXPlay _sfxPlay;

    private const int Amount = 2;

    public List<Image> ButtonsImages { get; private set; } = new();
    public Sprite[] Sprites { get; private set; }
    public AudioClip[] AudioClips { get; private set; }
    [field: SerializeField] public AudioSource[] AudioSources { get; private set; }

    private void OnEnable()
    {
        IsSoundPlay += SoundPlay;
        IsSoundVolumeChanged += ChangeSoundVolume;
        IsImagesSet += LoadImage;
    }

    private void OnDisable()
    {
        IsSoundPlay -= SoundPlay;
        IsSoundVolumeChanged -= ChangeSoundVolume;
        IsImagesSet -= LoadImage;
    }

    private void Awake()
    {
        LoadComponents();
        new SoundInitialization().Execute(this);
    }

    private void LoadComponents()
    {
        Sprites = new SoundSpritesLoader().Execute();
        AudioClips = new SoundLoader().Execute();

        _sfxPlay = new(this);
    }

    private void LoadImage(Image image)
    {
        ButtonsImages.Add(image);

        if (ButtonsImages.Count == Amount)
            new SoundImagesInitialization().Execute(this);
    }

    private void SoundPlay(int index) => _sfxPlay.SFXPlay?.Invoke(index);

    private void ChangeSoundVolume(int index)
    {
        if (index == UIButtonsCount.SFX) _sfx.Execute(this);
        else if (index == UIButtonsCount.BGM) _bgm.Execute(this);
    }
}