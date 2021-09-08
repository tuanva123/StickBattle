using UnityEngine;

/// <summary>
/// Ubh singleton mono behavior.
/// </summary>
public class UbhSingletonMonoBehavior<T> : UbhMonoBehaviour where T : UbhMonoBehaviour
{
    private static T m_instance;
    private static bool m_isQuitting;

    /// <summary>
    /// Get singleton instance.
    /// </summary>
    public static T instance
    {
        get
        {
            if (m_isQuitting || Application.isPlaying == false)
            {
                return null;
            }

            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();

                if (m_instance == null)
                {
                    Debug.Log("Created " + typeof(T).Name);
                    m_instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    /// <summary>
    /// Call from override Awake method in inheriting classes.
    /// Example : protected override void Awake () { base.Awake (); }
    /// </summary>
    protected virtual void Awake()
    {
        if (this != instance)
        {
            GameObject go = gameObject;
            Destroy(this);
            Destroy(go);
            return;
        }
    }

    /// <summary>
    /// Call from override OnDestroy method in inheriting classes.
    /// Example : protected override void OnDestroy () { base.OnDestroy (); }
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (this == m_instance)
        {
            m_instance = null;
        }
    }

    /// <summary>
    /// Call from override OnApplicationQuit method in inheriting classes.
    /// Example : protected override void OnApplicationQuit () { base.OnApplicationQuit (); }
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        m_isQuitting = true;
    }
}
