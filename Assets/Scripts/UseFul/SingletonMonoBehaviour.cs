using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /// <summary>
    /// class singleton used for easy access to some class in the game
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// var to know when the game is being closed or not
        /// </summary>
        static bool _closingApplication;

        /// <summary>
        /// method called when the class start
        /// if the instance already exist, meaning that there are two class with the same type, so the new one must be removed
        /// </summary>
        protected virtual void Awake()
        {
            // if the instance is not null, meaning this class is the second of this type, so must be removed
            if (_instance != null)
            {
                Debug.LogWarning("Singleton Awake: Warning, Only Can be One instance of " + typeof(T).ToString() + " so the second one will be destroyed");
                Destroy(gameObject);
            }
            else
            {
                // if this instance is the first one, assing the class
                AssingInstance();
            }
        }

        /// <summary>
        /// method called when the instance should be assing. Only will work if only one object of Type T in the scene
        /// </summary>
        private static void AssingInstance()
        {
            // check all the candidates, if there are more than one, error
            var candidates = GameObject.FindObjectsOfType<T>();
            if (candidates.Length > 1)
            {
                Debug.LogError("More that one instance same type");
                return;
            }
            // if no candidate, create a new one
            else if (candidates.Length == 0)
            {
                GameObject singleton = new GameObject(typeof(T).ToString());
                _instance = singleton.AddComponent<T>();
            }
            else
            {
                // because only one candidate, this one is assing
                _instance = candidates[0];
            }
            // as set as dont destroy on load
            DontDestroyOnLoad(_instance);
        }

        /// <summary>
        /// static var to save the instance of the class
        /// if the application is closing, return a null pointer. Noone should call this class if the applicaion is closing, 
        /// if happen is because something is wrong
        /// </summary>
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_closingApplication)
                {
                    return null;
                }

                if (_instance == null)
                {
                    AssingInstance();
                }

                return _instance;
            }
        }

        /// <summary>
        /// method called when the application is closing, assing the var closing application to true
        /// </summary>
        private void OnApplicationQuit()
        {
            _closingApplication = true;
        }
}