
using UnityEngine;
namespace Holeio.essentials {
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance { get { return instance; } set { } }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.Log("Duplicate Character instance detected");
                Destroy(gameObject);
            }
        }
    }

}

