using UnityEngine;
using System.Collections;
using System.Reflection;
using System;


/// <summary>
/// 单例模式  没有继承MonoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T: class ,new()  {

    protected static T _intance=null;
        public static T Instance
        {
            get
            {
                if (_intance==null)
                {
                    _intance = new T();
                }
                return _intance;
            }
        }
        protected Singleton()
        {
         
        }
     
}
