using UnityEngine;
using Zenject;

public class LevelInjectContainer : MonoBehaviour
{
    public Player Player { get; private set; }
    public MainCameraFactory CameraFactory { get; private set; }

    [Inject]
    public void Container
        (Player player, 
        MainCameraFactory cameraFactory)
    {
        Player = player;
        CameraFactory = cameraFactory;
    }
}