using System;

namespace MetalMax
{
    /// <summary>
    /// 普通单例类
    /// </summary>
    /// <typeparam name="T">子类类型</typeparam>
    public class Singleton<T> : IDisposable where T : new()
    {
        private static T _Instance;
        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new T();
                }
                return _Instance;
            }
        }

        public virtual void Dispose() { }
    }
}
