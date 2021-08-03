using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class UnityEngineExtensions
{
    #region GetChild
    public static GameObject GetChild(this GameObject gameObject, int index)
        => gameObject.transform.GetChild(index).gameObject;

    public static GameObject GetChildObject(this GameObject gameObject, int index)
        => gameObject.transform.GetChild(index).gameObject;

    public static GameObject GetChildObject(this Transform transform, int index)
        => transform.GetChild(index).gameObject;

    public static Transform GetChildTransform(this GameObject gameObject, int index)
        => gameObject.transform.GetChild(index);

    public static Transform GetChildTransform(this Transform transform, int index)
        => transform.GetChild(index);

    public static Transform FindDeepChild(this Component component, string name)
        => component.transform.FindDeepChild(name);

    public static GameObject FindDeepChild(this GameObject gameObject, string name)
        => gameObject.transform.FindDeepChild(name).gameObject;

    public static Transform FindDeepChild(this Transform transform, string name)
    {
        if (name.Contains('/'))
        {
            Transform child = transform;
            foreach (string childName in name.Split('/'))
            {
                child = child.Find(childName);
                if (!child) break;
            }
            if (child) return child;
        }

        Transform t = transform.Find(name);
        if (t) return t;

        for (int i = 0; i < transform.childCount; i++)
        {
            t = FindDeepChild(transform.GetChild(i), name);
            if (t) return t;
        }
        return null;
    }

    public static Transform FindInactiveChild(this Transform transform, string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == name)
                return transform.GetChild(i);
        }
        return null;
    }
    #endregion

    #region GetOrAddComponent
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        => gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

    public static T GetOrAddComponent<T>(this Transform t) where T : Component
        => GetOrAddComponent<T>(t.gameObject);

    public static T GetOrAddComponent<T>(this Component c) where T : Component
        => GetOrAddComponent<T>(c.gameObject);
    #endregion

    #region HasComponent
    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        => gameObject.GetComponent<T>() != null;

    public static bool HasComponent<T>(this Transform transform) where T : Component
        => transform.gameObject.GetComponent<T>() != null;

    public static bool HasComponent<T>(this Component component) where T : Component
        => component.gameObject.GetComponent<T>() != null;

    public static bool HasComponentInParent<T>(this GameObject gameObject) where T : Component
        => gameObject.GetComponentInParent<T>() != null;

    public static bool HasComponentInParent<T>(this Transform transform) where T : Component
        => transform.gameObject.GetComponentInParent<T>() != null;

    public static bool HasComponentInParent<T>(this Component component) where T : Component
        => component.gameObject.GetComponentInParent<T>() != null;

    public static bool TryGetComponent<T>(this GameObject gameObject, out T t) where T : Component
    {
        if (gameObject == null) { t = null; return false; }
        t = gameObject.GetComponent<T>();
        return t != null;
    }

    public static bool TryGetComponent<T>(this Transform transform, out T t) where T : Component
    {
        if (transform == null) { t = null; return false; }
        t = transform.GetComponent<T>();
        return t != null;
    }

    public static bool TryGetComponent<T>(this Component component, out T t) where T : Component
    {
        if (component == null) { t = null; return false; }
        t = component.GetComponent<T>();
        return t != null;
    }

    public static bool TryGetComponentInParent<T>(this GameObject gameObject, out T t) where T : Component
    {
        if (gameObject == null) { t = null; return false; }
        t = gameObject.GetComponentInParent<T>();
        return t != null;
    }

    public static bool TryGetComponentInParent<T>(this Transform transform, out T t) where T : Component
    {
        if (transform == null) { t = null; return false; }
        t = transform.GetComponentInParent<T>();
        return t != null;
    }

    public static bool TryGetComponentInParent<T>(this Component component, out T t) where T : Component
    {
        if (component == null) { t = null; return false; }
        t = component.GetComponentInParent<T>();
        return t != null;
    }
    #endregion

    #region Vector

    public static Vector2 Add(this Vector2 v, float f)
    {
        v.x += f;
        v.y += f;
        return v;
    }

    public static Vector2 DegToVector(this float angleDeg) => RadToVector(angleDeg * Mathf.Deg2Rad);
    public static Vector2 RadToVector(this float angleRad) => new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

    public static Vector2 Set(this Vector2 v, float? x = null, float? y = null)
    {
        if (x.HasValue)
            v.x = x.Value;
        if (y.HasValue)
            v.y = y.Value;
        return v;
    }

    public static Vector3 Set(this Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        if (x.HasValue)
            v.x = x.Value;
        if (y.HasValue)
            v.y = y.Value;
        if (z.HasValue)
            v.z = z.Value;
        return v;
    }

    public static void Update(this ref Vector2 v, float? x = null, float? y = null)
    {
        if (x.HasValue)
            v.x = x.Value;
        if (y.HasValue)
            v.y = y.Value;
    }

    public static void Update(this ref Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        if (x.HasValue)
            v.x = x.Value;
        if (y.HasValue)
            v.y = y.Value;
        if (z.HasValue)
            v.z = z.Value;
    }

    /// <summary>
    /// Multiplies each passed parameter with their corresponding place in the vector.
    /// Example: if v = (2, 1) and x = 3, return will be (6, 1)
    /// </summary>
    /// <param name="v">The original vector to be multiplied</param>
    /// <param name="x">The factor by which to multiply the x component to the vector</param>
    /// <param name="y">The factor by which to multiply the y component to the vector</param>
    /// <returns></returns>
    public static Vector2 MultiplyIndividually(this Vector2 v, float? x = null, float? y = null)
    {
        if (x.HasValue)
            v.x *= x.Value;
        if (y.HasValue)
            v.y *= y.Value;
        return v;
    }

    /// <summary>
    /// Multiplies each passed parameter with their corresponding place in the vector.
    /// Example: if v = (2, 1, 1) and x = 3, return will be (6, 1, 1)
    /// </summary>
    /// <param name="v">The original vector to be multiplied</param>
    /// <param name="x">The factor by which to multiply the x component to the vector</param>
    /// <param name="y">The factor by which to multiply the y component to the vector</param>
    /// <param name="z">The factor by which to multiply the z component to the vector</param>
    /// <returns></returns>
    public static Vector3 MultiplyIndividually(this Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        if (x.HasValue)
            v.x *= x.Value;
        if (y.HasValue)
            v.y *= y.Value;
        if (z.HasValue)
            v.z *= z.Value;
        return v;
    }

    /// <summary>
    /// Sets the sign of each component in a vector individually, by getting the magnitudes from the vector and the signs from the parameters
    /// Example: if v = (-2, 2) and x = -2 and y = -1, return will be (-2, -2)
    /// </summary>
    /// <param name="v">The original vector to be updated</param>
    /// <param name="x">The new sign the x component shall have</param>
    /// <param name="y">The new sign the y component shall have</param>
    /// <returns>The vector with the changed components</returns>
    /// <seealso cref="UpdateSign(ref Vector2, float?, float?)"/>
    public static Vector2 SetSign(this Vector2 v, float? x = null, float? y = null)
    {
        if (x.HasValue)
            v.x = Mathf.Abs(v.x) * Mathf.Sign(x.Value);
        if (y.HasValue)
            v.y = Mathf.Abs(v.y) * Mathf.Sign(y.Value);
        return v;
    }

    /// <summary>
    /// Sets the sign of each component in a vector individually, by getting the magnitudes from the vector and the signs from the parameters
    /// Example: if v = (-2, 2, 1) and x = -2 and y = -1, return will be (-2, -2, 1)
    /// </summary>
    /// <param name="v">The original vector to be updated</param>
    /// <param name="x">The new sign the x component shall have</param>
    /// <param name="y">The new sign the y component shall have</param>
    /// <param name="z">The new sign the z component shall have</param>
    /// <returns>The vector with the changed components</returns>
    /// <seealso cref="UpdateSign(ref Vector3, float?, float?, float?)"/>
    public static Vector3 SetSign(this Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        if (x.HasValue)
            v.x = Mathf.Abs(v.x) * Mathf.Sign(x.Value);
        if (y.HasValue)
            v.y = Mathf.Abs(v.y) * Mathf.Sign(y.Value);
        if (z.HasValue)
            v.z = Mathf.Abs(v.z) * Mathf.Sign(z.Value);
        return v;
    }


    /// <summary>
    /// Updates the sign of each component in a vector individually, by getting the magnitudes from the vector and the signs from the parameters
    /// Example: if v = (-2, 2) and x = -2 and y = -1, return will be (-2, -2)
    /// </summary>
    /// <param name="v">The original vector to be updated</param>
    /// <param name="x">The new sign the x component shall have</param>
    /// <param name="y">The new sign the y component shall have</param>
    /// <seealso cref="SetSign(Vector2, float?, float?)"/>
    public static void UpdateSign(this ref Vector2 v, float? x = null, float? y = null)
    {
        if (x.HasValue)
            v.x = Mathf.Abs(v.x) * Mathf.Sign(x.Value);
        if (y.HasValue)
            v.y = Mathf.Abs(v.y) * Mathf.Sign(y.Value);
    }

    /// <summary>
    /// Updates the sign of each component in a vector individually, by getting the magnitudes from the vector and the signs from the parameters
    /// Example: if v = (-2, 2, 1) and x = -2 and y = -1, return will be (-2, -2, 1)
    /// </summary>
    /// <param name="v">The original vector to be updated</param>
    /// <param name="x">The new sign the x component shall have</param>
    /// <param name="y">The new sign the y component shall have</param>
    /// <param name="z">The new sign the z component shall have</param>
    /// <seealso cref="SetSign(Vector3, float?, float?, float?)"/>
    public static void UpdateSign(this ref Vector3 v, float? x = null, float? y = null, float? z = null)
    {
        if (x.HasValue)
            v.x = Mathf.Abs(v.x) * Mathf.Sign(x.Value);
        if (y.HasValue)
            v.y = Mathf.Abs(v.y) * Mathf.Sign(y.Value);
        if (z.HasValue)
            v.z = Mathf.Abs(v.z) * Mathf.Sign(z.Value);
    }

    public static Vector2 RotateClockwise(this Vector2 v) => new Vector2(v.y, -v.x);
    public static Vector2 RotateCounterClockwise(this Vector2 v) => new Vector2(-v.y, v.x);

    #endregion

    #region Transform

    public static void UpdatePosition(this Transform t, float? x = null, float? y = null, float? z = null) => t.position = t.position.Set(x, y, z);
    public static void UpdateLocalPosition(this Transform t, float? x = null, float? y = null, float? z = null) => t.localPosition = t.localPosition.Set(x, y, z);

    public static void UpdatePosition(this GameObject go, float? x = null, float? y = null, float? z = null) => go?.transform.UpdatePosition(z, y, z);
    public static void UpdatePosition(this Component c, float? x = null, float? y = null, float? z = null) => c?.transform.UpdatePosition(z, y, z);
    public static void UpdateLocalPosition(this GameObject go, float? x = null, float? y = null, float? z = null) => go?.transform.UpdateLocalPosition(z, y, z);
    public static void UpdateLocalPosition(this Component c, float? x = null, float? y = null, float? z = null) => c?.transform.UpdateLocalPosition(z, y, z);

    public static RectTransform GetRectTransform(this GameObject go) => go?.transform as RectTransform;
    public static RectTransform GetRectTransform(this Component c) => c?.transform as RectTransform;
    public static RectTransform GetRectTransform(this Transform t) => t as RectTransform;

    public static float GetWidth(this RectTransform rt) => rt?.rect.width ?? 0;
    public static float GetWidth(this GameObject go) => GetRectTransform(go)?.rect.width ?? 0;
    public static float GetWidth(this Component c) => GetRectTransform(c)?.rect.width ?? 0;
    public static float GetWidth(this Transform t) => GetRectTransform(t)?.rect.width ?? 0;
    public static float GetHeight(this RectTransform rt) => rt?.rect.height ?? 0;
    public static float GetHeight(this GameObject go) => GetRectTransform(go)?.rect.height ?? 0;
    public static float GetHeight(this Component c) => GetRectTransform(c)?.rect.height ?? 0;
    public static float GetHeight(this Transform t) => GetRectTransform(t)?.rect.height ?? 0;

    public static void SetWidth(this RectTransform rt, float value) => rt?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
    public static void SetWidth(this GameObject go, float value) => GetRectTransform(go)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
    public static void SetWidth(this Component c, float value) => GetRectTransform(c)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
    public static void SetWidth(this Transform t, float value) => GetRectTransform(t)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
    public static void SetHeight(this RectTransform rt, float value) => rt?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
    public static void SetHeight(this GameObject go, float value) => GetRectTransform(go)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
    public static void SetHeight(this Component c, float value) => GetRectTransform(c)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
    public static void SetHeight(this Transform t, float value) => GetRectTransform(t)?.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);

    public static int GetChildCount(this GameObject go) => go.transform.childCount;
    public static int GetChildCount(this Component c) => c.transform.childCount;

    public static T[] GetComponentsInImmediateChildren<T>(this GameObject gameObject) where T : Component
        => GetComponentsInImmediateChildren<T>(gameObject.transform);
    public static T[] GetComponentsInImmediateChildren<T>(this Component component) where T : Component
        => GetComponentsInImmediateChildren<T>(component.transform);
    public static T[] GetComponentsInImmediateChildren<T>(this Transform transform) where T : Component
    {
        List<T> list = new List<T>(transform.childCount);
        foreach (Transform child in transform)
            if (child.TryGetComponent<T>(out T t))
                list.Add(t);
        return list.ToArray();
    }

    #endregion

    #region SetActive
    public static void SetActive(this Transform transform, bool value) => transform.gameObject.SetActive(value);
    public static void ToggleActive(this GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf);
    public static void ToggleActive(this Transform transform) => transform.gameObject.SetActive(!transform.gameObject.activeSelf);
    #endregion

    #region GetParent
    public static GameObject GetParent(this GameObject gameObject) => gameObject.transform.parent.gameObject;
    public static Transform GetParentTransform(this GameObject gameObject) => gameObject.transform.parent;
    public static GameObject GetParentGameObject(this GameObject gameObject) => gameObject.transform.parent.gameObject;

    public static GameObject GetParentWithTag(this GameObject go, string tag)
    {
        if (go == null) return null;

        if (go.CompareTag(tag))
            return go;
        else
            return GetParentWithTag(go.transform.parent, tag)?.gameObject;
    }
    public static Transform GetParentWithTag(this Transform t, string tag)
    {
        if (t == null) return null;

        if (t.CompareTag(tag))
            return t;
        else
            return GetParentWithTag(t.parent, tag);
    }

    public static bool TryGetParentWithTag(this GameObject go, string tag, out GameObject parentWithTag)
    {
        parentWithTag = GetParentWithTag(go, tag);
        return parentWithTag != null;
    }

    public static bool TryGetParentWithTag(this Transform t, string tag, out Transform parentWithTag)
    {
        parentWithTag = GetParentWithTag(t, tag);
        return parentWithTag != null;
    }
    #endregion

    #region Destroy
    public static void Destroy(this GameObject go) => UnityEngine.Object.Destroy(go);
    public static void Destroy(this GameObject go, float t) => UnityEngine.Object.Destroy(go, t);
    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform t in transform) UnityEngine.Object.Destroy(t.gameObject);
    }
    public static void DestroyAllChildren(this GameObject gameObject)
    {
        foreach (Transform t in gameObject.transform) UnityEngine.Object.Destroy(t.gameObject);
    }
    #endregion

    #region Mathf

    /// <summary> Compares two floating point values and returns true if they are similar. </summary>
    public static bool Approximately(this float f1, float f2)
        => Mathf.Approximately(f1, f2);

    /// <summary> Compares two floating point values and returns true if they are within a defined tolerance. </summary>
    public static bool Approximately(this float f1, float f2, float tolerance)
        => Mathf.Abs(f1 - f2) <= tolerance;

    /// <summary> Compares the vector's components and returns true if they are similar to the parameter. </summary>
    public static bool Approximately(this Vector2 v1, float parameter)
        => Mathf.Approximately(v1.x, parameter) && Mathf.Approximately(v1.y, parameter);

    /// <summary> Compares the vector's components and returns true if they are similar to the parameter within a defined tolerance. </summary>
    public static bool Approximately(this Vector2 v1, float parameter, float tolerance)
        => Approximately(v1.x, parameter, tolerance) && Approximately(v1.y, parameter, tolerance);

    /// <summary> Compares two vectors and returns true if they are similar. </summary>
    public static bool Approximately(this Vector2 v1, Vector2 v2)
        => Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y);

    /// <summary> Compares two vectors and returns true if they are  within a defined tolerance. </summary>
    public static bool Approximately(this Vector2 v1, Vector2 v2, float tolerance)
        => Approximately(v1.x, v2.x, tolerance) && Approximately(v1.y, v2.y, tolerance);

    /// <summary> Compares two vectors and returns true if they are  within a defined tolerance. </summary>
    public static bool Approximately(this Vector2 v1, Vector2 v2, Vector2 tolerance)
        => Approximately(v1.x, v2.x, tolerance.x) && Approximately(v1.y, v2.y, tolerance.y);

    /// <summary>
    /// Returns the absolute value of the vector components.
    /// </summary>
    public static Vector2 Abs(this Vector2 v2)
    {
        v2.x = Mathf.Abs(v2.x);
        v2.y = Mathf.Abs(v2.y);
        return v2;
    }

    /// <summary>
    /// Returns the absolute value of the vector components.
    /// </summary>
    public static Vector3 Abs(this Vector3 v3)
    {
        v3.x = Mathf.Abs(v3.x);
        v3.y = Mathf.Abs(v3.y);
        v3.z = Mathf.Abs(v3.z);
        return v3;
    }

    #endregion

    #region ColliderPositions

    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetTopLeft(this Bounds bounds) => new Vector2(bounds.min.x, bounds.max.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetTopCenter(this Bounds bounds) => new Vector2(bounds.center.x, bounds.max.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetTopRight(this Bounds bounds) => new Vector2(bounds.max.x, bounds.max.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetCenterLeft(this Bounds bounds) => new Vector2(bounds.min.x, bounds.center.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetCenter(this Bounds bounds) => new Vector2(bounds.center.x, bounds.center.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetCenterRight(this Bounds bounds) => new Vector2(bounds.max.x, bounds.center.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetBottomLeft(this Bounds bounds) => new Vector2(bounds.min.x, bounds.min.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetBottomCenter(this Bounds bounds) => new Vector2(bounds.center.x, bounds.min.y);
    /// <summary>
    /// Gets the world position for this point of the bounds.
    /// </summary>
    public static Vector2 GetBottomRight(this Bounds bounds) => new Vector2(bounds.max.x, bounds.min.y);
    #endregion

    #region Texture

    public static bool IsBlank(this Texture2D texture) => IsBlank(texture, 0, 0, texture.width, texture.height);
    public static bool IsBlank(this Texture2D texture, Rect rect) => IsBlank(texture, (int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
    public static bool IsBlank(this Texture2D texture, int x, int y, int width, int height) => texture.GetPixels(x, y, width, height).All(color => color.a <= float.Epsilon);

    #endregion

    #region Find Objects

    /// <summary>
    /// Gets all components that implement an interface on the passed scene
    /// </summary>
    /// <typeparam name="T">The interface to be searched</typeparam>
    /// <param name="scene">The scene where the search should take place</param>
    /// <returns>A list with all component instances of type T on the passed scene</returns>
    public static List<Component> FindObjectsWithInterface(this Scene scene, Type type)
    {
        List<Component> results = new List<Component>();
        scene.GetRootGameObjects().ForEach(go => results.AddRange(go.GetComponentsInChildren(type)));
        return results;
    }

    /// <summary>
    /// Gets all components that implement an interface on the passed scene
    /// </summary>
    /// <typeparam name="T">The interface to be searched</typeparam>
    /// <param name="scene">The scene where the search should take place</param>
    /// <returns>A list with all component instances of type T on the passed scene</returns>
    public static List<T> FindObjectsWithInterface<T>(this Scene scene)
    {
        List<T> results = new List<T>();
        scene.GetRootGameObjects().ForEach(go => go.GetComponentsInChildren(results));
        return results;
    }

    #endregion

    #region Others

    public static Toggle AddListener(this Toggle toggle, UnityAction<bool> action)
    {
        toggle.onValueChanged.AddListener(action);
        return toggle;
    }

    /// <summary>
    /// Adds a listener to the toggles that will return their index when triggered
    /// </summary>
    /// <param name="toggles">An array of toggles to listen to</param>
    /// <param name="action">An action to be invoked with the toggle's original index</param>
    /// <returns></returns>
    public static Toggle[] AddListener(this Toggle[] toggles, UnityAction<int, bool> action)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int j = i; //Using temporary int because i gets updated as a reference if used directly in a delegate
            toggles[i].onValueChanged.AddListener((bool b) => action.Invoke(j, b));
        }
        return toggles;
    }

    #endregion

    #region NewCoroutines //Experimenting with EasySingleton

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static EasyCoroutine InvokeDelayed(YieldInstruction yieldInstruction, Action action)
        => new EasyCoroutine(yieldInstruction, action);

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static EasyCoroutine InvokeDelayed(CustomYieldInstruction yieldInstruction, Action action)
        => new EasyCoroutine(yieldInstruction, action);

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static EasyCoroutine InvokeAfter(this Action action, YieldInstruction yieldInstruction)
        => new EasyCoroutine(yieldInstruction, action);

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static EasyCoroutine InvokeAfter(this Action action, CustomYieldInstruction yieldInstruction)
        => new EasyCoroutine(yieldInstruction, action);

    #endregion

    #region Coroutines //If this is successfull enough, we could make use of singletons to remove the need to pass a monobehaviour reference

    public static IEnumerator InvokeDelayed(this MonoBehaviour mb, float time, Action a) =>
                                                                            InvokeDelayed(mb, new WaitForSeconds(time), a);

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="mb">A monobehaviour on which a coroutine will be started</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static IEnumerator InvokeDelayed(this MonoBehaviour mb, YieldInstruction yieldInstruction, Action action)
    {
        IEnumerator enumerator = action.EnumerateAfter(yieldInstruction);
        mb.StartCoroutine(enumerator);
        return enumerator;
    }

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="mb">A monobehaviour on which a coroutine will be started</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static IEnumerator InvokeDelayed(this MonoBehaviour mb, CustomYieldInstruction yieldInstruction, Action action)
    {
        IEnumerator enumerator = action.EnumerateAfter(yieldInstruction);
        mb.StartCoroutine(enumerator);
        return enumerator;
    }

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="mb">A monobehaviour on which a coroutine will be started</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static IEnumerator StartCoroutine(this MonoBehaviour mb, YieldInstruction yieldInstruction, Action action)
    {
        IEnumerator enumerator = action.EnumerateAfter(yieldInstruction);
        mb.StartCoroutine(enumerator);
        return enumerator;
    }

    /// <summary>
    /// Invokes an action only after the yield instruction is complete
    /// </summary>
    /// <param name="mb">A monobehaviour on which a coroutine will be started</param>
    /// <param name="yieldInstruction">The instruction to be awaited before the action is invoked</param>
    /// <param name="action">The action to be invoked once the instruction is complete</param>
    /// <returns>A reference to the coroutine that was created</returns>
    public static IEnumerator StartCoroutine(this MonoBehaviour mb, CustomYieldInstruction yieldInstruction, Action action)
    {
        IEnumerator enumerator = action.EnumerateAfter(yieldInstruction);
        mb.StartCoroutine(enumerator);
        return enumerator;
    }

    /// <summary>
    /// Returns an enumerator that fullfills the YieldInstruction then invokes the Action
    /// </summary>
    /// <param name="action">An action to be invoked after the YieldInstruction is complete</param>
    /// <param name="yieldInstruction">An instruction on what to wait for before invoking the Action</param>
    /// <returns>An IEnumerator that yield returns the YieldInstruction then invokes the Action</returns>
    public static IEnumerator EnumerateAfter(this Action action, YieldInstruction yieldInstruction)
    {
        yield return yieldInstruction;
        action.Invoke();
    }

    /// <summary>
    /// Returns an enumerator that fullfills the CustomYieldInstruction then invokes the Action
    /// </summary>
    /// <param name="action">An action to be invoked after the CustomYieldInstruction is complete</param>
    /// <param name="yieldInstruction">An instruction on what to wait for before invoking the Action</param>
    /// <returns>An IEnumerator that yield returns the CustomYieldInstruction then invokes the Action</returns>
    public static IEnumerator EnumerateAfter(this Action action, CustomYieldInstruction yieldInstruction)
    {
        yield return yieldInstruction;
        action.Invoke();
    }

    #endregion
}
