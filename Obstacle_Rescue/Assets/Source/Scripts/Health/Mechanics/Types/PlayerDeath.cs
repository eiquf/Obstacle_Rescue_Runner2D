using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : Health
{
    private readonly PlayerAnimation _playerAnimation;
    private readonly MainCameraFactory _mainCameraFactory;

    public PlayerDeath
        (LivesSettings livesSettings, 
        List<GameObject> hurts, 
        PlayerDeath playerDeath, 
        MainCameraFactory mainCameraFactory, 
        PlayerAnimation playerAnimation) 
        : base(livesSettings, hurts, playerDeath)
    {
        _mainCameraFactory = mainCameraFactory;
        _playerAnimation = playerAnimation;
    }

    public override void Execute()
    {
        if (_hurts.Count == _livesSettings.MinLives)
        {
            QuestPanelExistingCheker();
            _playerAnimation.PlayerIsUpset?.Invoke();
            _mainCameraFactory.IsZoomed?.Invoke(true);
        }
    }
    private void QuestPanelExistingCheker()
    {
        //QuestPanel questPanel = FindFirstObjectByType<QuestPanel>();
        //if (questPanel != null)
        //    Destroy(questPanel.gameObject);
    }
}