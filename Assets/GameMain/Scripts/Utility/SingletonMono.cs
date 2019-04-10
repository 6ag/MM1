using System;
using UnityEngine;

namespace MetalMax
{
    /// <summary>
    /// 继承自MonoBehaviour的单例类
    /// </summary>
    /// <typeparam name="T">子类类型</typeparam>
    public class SingletonMono<T> : MonoBehaviour, IDisposable where T : MonoBehaviour
    {
        private static T _Instance;
        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _Instance = obj.GetOrAddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
                return _Instance;
            }
        }

        void Awake()
        {
            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        void Update()
        {
            OnUpdate();
        }

        void OnDestroy()
        {
            BeforeOnDestroy();
        }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void BeforeOnDestroy() { }

        public virtual void Dispose() { }
    }
}
