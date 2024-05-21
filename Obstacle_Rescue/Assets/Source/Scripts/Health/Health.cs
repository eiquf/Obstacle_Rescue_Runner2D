using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public sealed class Health : MonoBehaviour
{
    #region Actions
    public Action OnPlayerDeath;
    public Action OnPlayerDamaged;
    public Action OnPlayerHealed;
    #endregion
    [SerializeField] private LivesSettings _livesSettings;

    private HealthPrefabsLoader _healthPrefabLoader = new();
    private GameObject _hurt;

    public GameObject[] _startHurts { get; private set; }

    private List<GameObject> _hurts = new List<GameObject>();

    #region Injects
    //private PlayerAnimation _playerAnimation;
    //private MainCamera _cameraController;

    private PropUIAnimation _propUIAnim = new PropUIAnimation();
    //[Inject]
    //private void Construct
    //    (MainCamera cameraController,
    //    PlayerAnimation playerAnimation)
    //{
    //    _playerAnimation = playerAnimation;
    //    _cameraController = cameraController;
    //}
    #endregion
    private void OnEnable()
    {
        //OnPlayerDeath += IsDead;
        OnPlayerDamaged += ChangeHealth;
        OnPlayerHealed += AddHealth;
    }
    private void OnDisable()
    {
        //OnPlayerDeath -= IsDead;
        OnPlayerDamaged -= ChangeHealth;
        OnPlayerHealed -= AddHealth;
    }
    private void Awake() => _hurt = _healthPrefabLoader.Execute();
    private void Start() => LoadStartsHurts();
    private void FixedUpdate()
    {
        if (_hurts.Count == _livesSettings.MinLives)
            OnPlayerDeath?.Invoke();
    }
    private void LoadStartsHurts()
    {
        _startHurts = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            _startHurts[i] = transform.GetChild(i).gameObject;

        _hurts.AddRange(_startHurts);
    }
    private void ChangeHealth()
    {
        if (_hurts.Count <= _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            Destroy(_hurts[0], 0.25f);
            _propUIAnim.OnDisable(_hurts[0].transform);
            _hurts.Remove(_hurts[0]);
        }
    }
    private void AddHealth()
    {
        if (_hurts.Count < _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            GameObject hurt = Instantiate(_hurt, transform);
            _propUIAnim.OnEnable(hurt.transform);
            _hurts.Add(hurt);
        }
    }
    //private void IsDead()
    //{
    //    QuestPanelExistingCheker();
    //    _playerAnimation.PlayerIsUpset?.Invoke();
    //    _cameraController.IsZoomed?.Invoke(true);
    //}
    //private void QuestPanelExistingCheker()
    //{
    //    QuestPanel questPanel = FindFirstObjectByType<QuestPanel>();
    //    if (questPanel != null)
    //        Destroy(questPanel.gameObject);
    //}
}
