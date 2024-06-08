using System;
using UnityEngine;

public class EiquifAnimation : MonoBehaviour
{
    public Action<int> EiquifChangeAnim;

    private const string _partName = "ChangePart";

    private Animator _eiquifAnimator;

    private void OnEnable() => EiquifChangeAnim += EiquifPlayAnimation;
    private void Start() => _eiquifAnimator = GetComponent<Animator>();
    private void EiquifPlayAnimation(int index)
    {
        if (_eiquifAnimator != null)
            _eiquifAnimator.SetInteger(_partName, index);
    }
    private void OnDisable() => EiquifChangeAnim -= EiquifPlayAnimation;
}