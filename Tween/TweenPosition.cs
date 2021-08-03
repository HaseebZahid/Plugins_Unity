using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Transform))]
public class TweenPosition : Tween
{

    [Header("Settings_TweenPosition")]
    [Tooltip("Position of this object at 0")]
    [SerializeField] protected Vector3 position0;
    [Tooltip("Position of this object at 1")]
    [SerializeField] protected Vector3 position1;
    [Tooltip("Whether the positions are local or world")]
    [SerializeField] protected bool useLocalPosition;

    protected override void AssignTweenValueOnLerp(float lerpVariable)
    {
        if (useLocalPosition)
        {
            transform.localPosition = Vector3.LerpUnclamped(position0, position1, lerpVariable);
        } else
        {
            transform.position = Vector3.LerpUnclamped(position0, position1, lerpVariable);
        }
    }
}
