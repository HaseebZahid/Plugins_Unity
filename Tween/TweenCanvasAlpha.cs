using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TweenCanvasAlpha : Tween
{

    [Header("Settings_TweenCanvasAplha")]
    [Tooltip("Position of this object at 0")]
    [SerializeField] protected float alpha0 = 0f;
    [Tooltip("Position of this object at 1")]
    [SerializeField] protected float alpha1 = 1f;

    //[Inject]
    [SerializeField]  CanvasGroup m_CanvasGroup;
    public CanvasGroup canvasGroup { get => m_CanvasGroup; }
    private void Awake() { m_CanvasGroup = GetComponent<CanvasGroup>(); }

    protected override void AssignTweenValueOnLerp(float lerpVariable)
    {
        m_CanvasGroup.alpha = Mathf.LerpUnclamped(alpha0, alpha1, lerpVariable);
    }
}
