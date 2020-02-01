using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public new bool DontDestroyOnLoad;

    public static T Instance { get; private set; }

    public virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance == null)
        {
            Instance = this as T;
            if (DontDestroyOnLoad) DontDestroyOnLoad(Instance);
        }
    }
}