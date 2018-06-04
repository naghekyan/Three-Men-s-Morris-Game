using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
    public class GridPositionProvider
    {
        class RangeQueryCache
        {
            private Vector3 m_rangeCenter;
            private float m_rangeRadius;
            private GridPlaceholder m_validPlacholder;
            private float m_epislon = float.Epsilon;

            public  RangeQueryCache() {
                
            }

            public void SetValidPlacholderForRange(Vector3 rangeCenter, float rangeRadius, GridPlaceholder validPlacholder) {
                m_rangeCenter = rangeCenter;
                m_rangeRadius = rangeRadius;
                m_validPlacholder = validPlacholder;
            }

            public GridPlaceholder GetCachedValidPlacholder() {
                return m_validPlacholder;
            }

            public bool isEqualToChachedRange(Vector3 rangeCenter, float rangeRadius) {
                return Mathf.Abs (rangeRadius - m_rangeRadius) < m_epislon
                    && Mathf.Abs (rangeCenter.x - m_rangeCenter.x) < m_epislon
                    && Mathf.Abs (rangeCenter.y - m_rangeCenter.y) < m_epislon
                    && Mathf.Abs (rangeCenter.z - m_rangeCenter.z) < m_epislon; 
            }
        };

        private List<GridPlaceholder> m_gridPlaceholders = new List<GridPlaceholder>();

        private RangeQueryCache m_rangeQueryCache = new RangeQueryCache();

        public GridPositionProvider (GameObject[] listOfHoles)
        {
            m_rangeQueryCache = new RangeQueryCache ();

            foreach (GameObject hole in listOfHoles) {
                m_gridPlaceholders.Add (new GridPlaceholder(hole));
            }
        }

        public bool IsValidPositionInRangeExists(Vector3 position, float range) {
            if (m_rangeQueryCache.isEqualToChachedRange (position, range)) {
                return true;
            }

            float rangeSqr = range * range;
            foreach (GridPlaceholder placholder in m_gridPlaceholders) {
                Vector3 placeholderPosition = placholder.GetWorldPosition();
                Vector3 diffVector = position - placeholderPosition;
                float distanceSqr = diffVector.sqrMagnitude;
                if (distanceSqr < rangeSqr) {
                    m_rangeQueryCache.SetValidPlacholderForRange (position, range, placholder);
                    return true;
                }
            }

            return false;
        }

        public Vector3 GetValidPositionInRange(Vector3 position, float range) {
            if (m_rangeQueryCache.isEqualToChachedRange (position, range)) {
                return m_rangeQueryCache.GetCachedValidPlacholder().GetWorldPosition();
            }

            float rangeSqr = range * range;
            Vector3 result = position;
            foreach (GridPlaceholder placholder in m_gridPlaceholders) {
                Vector3 placeholderPosition = placholder.GetWorldPosition();
                Vector3 diffVector = position - placeholderPosition;
                float distanceSqr = diffVector.sqrMagnitude;

                if (distanceSqr < rangeSqr) {
                    m_rangeQueryCache.SetValidPlacholderForRange (position, range, placholder);
                    result = placeholderPosition;
                }
            }
            return result;
        }
    }
}

