using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class BehaviourSingleton<T> : MonoBehaviour where T : class, new()
    {
        public static T instance;

        public BehaviourSingleton()
        {
            instance = this as T;
        }

        public static T get()
        {
            return instance;
        }
    }
