using UnityEngine;
using UnityEngine.UI;

public class PrefButtonsActivation : IUIPanelsInstantiate
{
    private Button[] _buttons;
    public PrefButtonsActivation(Button[] buttons) => _buttons = buttons;
    public void Execute(Transform transform)
    {
        foreach (Button button in _buttons)
        {
            button.gameObject.SetActive(true);
            button.transform.position = transform.transform.position;
        }
    }
}