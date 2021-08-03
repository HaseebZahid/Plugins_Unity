using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : Tween
{

    [Header("Settings_TweenScale")]
    [Tooltip("Scale of this object at 0")]
    [SerializeField] protected Vector3 scale0;
    [Tooltip("Scale of this object at 1")]
    [SerializeField] protected Vector3 scale1;

    protected override void AssignTweenValueOnLerp(float lerpVariable)
    {
        transform.localScale = Vector3.LerpUnclamped(scale0, scale1, lerpVariable);
    }
}
