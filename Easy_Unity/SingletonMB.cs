using UnityEngine;

public abstract class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// The only instance of this class (might be null)
    /// </summary>
    public static T Instance { get; protected set; }

    /// <summary>
    /// Registers new Instance or self-destructs if one already exists
    /// </summary>
    protected virtual void Awake()
    {
        if (typeof(T) == typeof(MonoBehaviour))
        {
            Log.Error("Cannot create SingletonMB where T = MonoBehaviour");
            OnDestroyInstance();
        }
        else if (Instance == null)
        {
            Instance = GetComponent<T>();
        }
        else if (Instance != GetComponent<T>())
        {
            Log.Warning("Tried to create duplicate SingletonMB of type: " + (typeof(T)).ToString());
            OnDestroyInstance();
        }
    }

    /// <summary>
    /// Registers new Instance or self-destructs if one already exists
    /// </summary>
    protected virtual void AwakeSilently()
    {
        if (typeof(T) == typeof(MonoBehaviour))
            OnDestroyInstance();
        else if (Instance == null)
            Instance = GetComponent<T>();
        else if (Instance != GetComponent<T>())
            OnDestroyInstance();
    }

    /// <summary>
    /// Destroys the new instance
    /// </summary>
    protected virtual void OnDestroyInstance() => Destroy(GetComponent<T>());

    /// <summary>
    /// When instance is destroyed we should clear our reference to it
    /// </summary>
    protected virtual void OnDestroy()
    {
        //I'm not sure either will ever return yes, but wildly nullifying Instance would be worse
        if (Instance == GetComponent<T>() || Instance == this)
            Instance = null;
    }

    /// <summary>
    /// Gets or Creates an instance as necessary.
    /// </summary>
    /// <returns>The only instance of this class (be it old or new)</returns>
    protected static T GetOrCreateInstance() => Instance ? Instance : Instance = new GameObject(nameof(T)).AddComponent<T>();

    /// <summary>
    /// Gets or Finds an instance as necessary
    /// </summary>
    /// <returns>The only instance of this class (be it previously known or not)</returns>
    protected static T GetOrFindInstance() => Instance ? Instance : Instance = FindObjectOfType<T>();
}
