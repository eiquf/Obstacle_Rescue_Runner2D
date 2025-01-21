using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundController : MonoBehaviour
{
    public Action<int> IsSoundPlay { get; private set; }
    public Action<Transform> IsImagesSet { get; private set; }

    public readonly BGM _bgm = new();
    public readonly SFX _sfx = new();
    private SoundSFXPlay _sfxPlay;

    private const int Amount = 2;

    public List<Image> ButtonsImages { get; private set; } = new();
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
    public AudioClip[] AudioClips { get; private set; }
    [field: SerializeField] public AudioSource[] AudioSources { get; private set; }


    private void OnEnable()
    {
        IsSoundPlay += SoundPlay;
        IsImagesSet += InitializeImages;
    }

    private void OnDisable()
    {
        IsSoundPlay -= SoundPlay;
        IsImagesSet -= InitializeImages;
    }

    private void Start()
    {
        new SoundInitialization().Execute(this);
        _sfxPlay = new(this);
        _sfx.Execute(this);
        _bgm.Execute(this);

    }
    private void InitializeImages(Transform buttonsParent)
    {
        ButtonsImages.Clear();

        Image SFX = buttonsParent.GetChild(1).GetComponent<Image>();
        Image BGM = buttonsParent.GetChild(2).GetComponent<Image>();

        List<Image> buttons = new() { SFX, BGM};
        ButtonsImages.AddRange(buttons);

        if (ButtonsImages.Count == Amount)
            new SoundImagesInitialization().Execute(this);
    }
    private void SoundPlay(int index) => _sfxPlay.SFXPlay?.Invoke(index);
}
