using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenRotation : Tween
{

    [Header("Settings_TweenRotation")]
    [Tooltip("Rotation of this object at 0")]
    [SerializeField] protected Vector3 rotation0;
    [Tooltip("Rotation of this object at 1")]
    [SerializeField] protected Vector3 rotation1;

    protected override void AssignTweenValueOnLerp(float lerpVariable)
    {
        transform.rotation = Quaternion.Euler(Vector3.LerpUnclamped(rotation0, rotation1, lerpVariable));
    }
}
