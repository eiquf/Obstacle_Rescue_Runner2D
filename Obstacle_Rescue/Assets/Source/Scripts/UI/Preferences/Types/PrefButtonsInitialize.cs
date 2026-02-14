using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PrefButtonsInitialize : IPreferences
{
    private readonly ButtonsActions _actions = new();
    private readonly Transform _preferencesCreatePos;
    private Button[] _buttons;

    private readonly InjectContainer _container;
    private readonly AnimationContext _animationContext = new();
    public PrefButtonsInitialize(Transform preferencesCreatePos, InjectContainer container)
    {
        _preferencesCreatePos = preferencesCreatePos;
        _container = container;
    }

    public void Execute()
    {
        _container.VibrationController.IsImageSet?.Invoke(_preferencesCreatePos);
        _container.SoundController.IsImagesSet?.Invoke(_preferencesCreatePos);

        _buttons = _preferencesCreatePos.GetComponentsInChildren<Button>();

        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;

            _buttons[i].onClick.AddListener(() =>
            {
                OnButtonClick(index);

                _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
                _animationContext.PlayAnimation(_buttons[index].transform);
            });
        }
    }

    private void OnButtonClick(int index)
    {
        IButtonAction action = _actions.GetPreferencesAction(index, _container, _container.SoundController._bgm, _container.SoundController._sfx, _container.VibrationController.Vibration);
        action?.Execute();
    }
}
