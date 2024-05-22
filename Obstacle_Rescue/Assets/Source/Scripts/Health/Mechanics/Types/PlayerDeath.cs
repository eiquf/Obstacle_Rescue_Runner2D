using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : Health
{
    public PlayerDeath(HealthFactory healthFactory, LivesSettings livesSettings, List<GameObject> hurts) : base(healthFactory, livesSettings, hurts)
    {
    }

    public override void Execute()
    {
        //if (hurts.Count == _livesSettings.MinLives)
        //{
        //    QuestPanelExistingCheker();
        //    _playerAnimation.PlayerIsUpset?.Invoke();
        //    _cameraController.IsZoomed?.Invoke(true);
        //}
    }
    //private void QuestPanelExistingCheker()
    //{
    //    QuestPanel questPanel = FindFirstObjectByType<QuestPanel>();
    //    if (questPanel != null)
    //        Destroy(questPanel.gameObject);
    //}
}