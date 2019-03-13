/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;

namespace Manta
{
    /// <summary>
    /// Implements a generic singleton.
    /// </summary>
    /// <typeparam name="T">The type of the subclass.</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static readonly Lazy<T> m_instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static T Instance => m_instance.Value;
    }
}
