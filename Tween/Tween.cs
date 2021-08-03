using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Tween : MonoBehaviour
{
    [Header("Settings_Tween")]
    [Tooltip("Duration in seconds for the transition animation")]
    [SerializeField] protected float animSpeed = 1f;
    [Tooltip("Duration is seconds between ShowUp and Hide animations (value <= 0 disables auto hide)")]
    [SerializeField] protected float animDuration = 1f;
    [Tooltip("Animation curve for this animation")]
    [SerializeField] protected AnimationCurve animCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [Tooltip("Should auto-loop on complete")]
    [SerializeField] protected bool loopable = false;
    [Tooltip("Should play on enable")]
    [SerializeField] protected bool playOnEnable = false;
    [Tooltip("Use realtime instead of scaled engine time")]
    [SerializeField] protected bool useUnscaledTime = false;

    [Header("Invoke_On_Complete")]
    [SerializeField]
    public UnityEvent onComplete = new UnityEvent();

    [Header("DEBUG_Tween")]
    [SerializeField] protected float elapsedTime = 0f;
    [SerializeField] protected bool isPlaying = false;

    protected void OnEnable()
    {
        if (playOnEnable)
            StartTween();
    }

    public void StartTween(AnimationCurve tweenCurve, float tweenSpeed, float tweenDuration)
    {
        animCurve = tweenCurve;
        animSpeed = tweenSpeed;
        animDuration = tweenDuration;
        StartTween();
    }

    public void StartTween()
    {
        isPlaying = true;
        elapsedTime = 0f;
    }

    protected void Update()
    {
        if (isPlaying)
        {
            if (useUnscaledTime)
                elapsedTime += Time.unscaledDeltaTime * animSpeed;
            else
                elapsedTime += Time.deltaTime * animSpeed;

            if (elapsedTime > animDuration)
            {
                isPlaying = loopable;
                elapsedTime = isPlaying ? elapsedTime - animDuration : animDuration;
                onComplete?.Invoke();
            }

            if (animCurve != null)
                AssignTweenValueOnLerp(animCurve.Evaluate(elapsedTime));
            else
                AssignTweenValueOnLerp(elapsedTime);
        }
    }

    protected abstract void AssignTweenValueOnLerp(float lerpVariable);
}
