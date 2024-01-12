using System.Collections.Generic;
using UnityEngine;

    public class Waypoint : MonoBehaviour
    {
        public static List<Waypoint> Waypoints = new List<Waypoint>();
        /* Use it:
            if (Waypoint.TryGetWaypoint(out LevelManager levelManager)){_levelManager = levelManager;} 
        */

        private void Awake()
        {
            Waypoints.Add(this);
        }

        private void OnDestroy()
        {
            Waypoints.Remove(this);
        }

        public static bool TryGetWaypoint<T>(out T component) where T : class
        {
            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (Waypoints[i].gameObject.TryGetComponent(out T component1))
                {
                    component = component1;
                    return true;
                }
            }
            component = null;
            return false;
        }
    }
