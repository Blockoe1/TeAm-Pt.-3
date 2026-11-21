/*
 * Marlow Greenan
 * Created: 4/19/2026
 * Last Updated: 6/20/2026
 * 
 *  A manager for the sceneloader system.
 */
using UnityEngine;

namespace MarUtility.ExecutionManagement
{
    public class Manager : MonoBehaviour
    {
        [SerializeField, Tooltip("When this is initialized.\nSCENELOADER_AWAKE is recommended to avoid execution order issues.")]
            private InitializeTime _initializeTime;

        [SerializeField]
        private Manager[] _subManagers;

        [SerializeField] private bool dontDestroyMePlease = false;

        #region GS
        public InitializeTime InitializeTime { get => _initializeTime; set => _initializeTime = value; }
        public Manager[] SubManagers { get => _subManagers; }
        #endregion

        private void Awake()
        {
            if (_initializeTime == InitializeTime.AWAKE)
            {
                Initialize();
                if (dontDestroyMePlease)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
        private void Start()
        {
            if (_initializeTime == InitializeTime.START)
                Initialize();
        }

        public virtual void Initialize() 
        {
            foreach (Manager sub in _subManagers)
                sub.Initialize();
        }
    }
    public enum InitializeTime
    {
        MANUAL = 000,
        AWAKE = 100,
        START = 200
    }
}


