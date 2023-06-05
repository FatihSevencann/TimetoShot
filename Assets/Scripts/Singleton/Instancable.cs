using UnityEngine;

public abstract class Instancable<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null || instance.Equals(null))
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }
            return instance;
        }
    }
}