using UnityEngine;

[CreateAssetMenu(fileName = "Word settings", menuName = "Word settings")]
public class WordSettings : ScriptableObject
{
    [SerializeField] private int _timer;
    [SerializeField] private string _word;
    [SerializeField] private Sprite _dottedImage;
    public int Timer => _timer;
    public string Word => _word;
    public Sprite DottedImage => _dottedImage;
}
