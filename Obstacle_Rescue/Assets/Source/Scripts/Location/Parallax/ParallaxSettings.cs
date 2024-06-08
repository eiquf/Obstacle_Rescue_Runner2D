using UnityEngine;


[CreateAssetMenu(fileName = "Parallax settings", menuName = "Parallax settings")]
public class ParallaxSettings : ScriptableObject
{
    [field: SerializeField] public int hidePosition;
    [field: SerializeField] public int spawnPosition;
    [field: Space]
    [field: SerializeField][Range(0, 100)] public float Depth;
}
