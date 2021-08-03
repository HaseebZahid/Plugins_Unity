using NoxLibrary;
using System.Linq;
using UnityEngine;

public abstract class SingletonSO<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;
    public static T Instance => instance ?? (instance = Resources.LoadAll<T>("").FirstOrDefault());

    protected virtual void Awake()
    {
        if (typeof(T) == typeof(ScriptableObject))
        {
            Log.Error("Cannot create SingletonSO where T = ScriptableObject");
            Destroy(this);
        }
        else if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this as T)
        {
            Log.Warning("Tried to create duplicate SingletonSO of type: " + (typeof(T)).ToString());
            Destroy(this);
        }
        else
        {
            Log.Warning("Unpexpected error on SingletonSO.Awake for type: " + (typeof(T)).ToString());
        }
    }

    protected virtual void OnDestroy() => instance = null;
}
