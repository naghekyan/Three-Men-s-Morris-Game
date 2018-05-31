using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
    public class ValidPositionProvider
    {
        class RangeQueryCache
        {
            private Vector3 m_rangeCenter;
            private float m_rangeRadius;
            private Vector3 m_validPosition;
            private float m_epislon = float.Epsilon;

            public  RangeQueryCache() {
                
            }

            public void SetValidPositionForRange(Vector3 rangeCenter, float rangeRadius, Vector3 validPosition) {
                m_rangeCenter = rangeCenter;
                m_rangeRadius = rangeRadius;
                m_validPosition = validPosition;
            }

            public Vector3 GetCachedValidPosition() {
                return m_validPosition;
            }

            public bool isEqualToChachedRange(Vector3 rangeCenter, float rangeRadius) {
                return Mathf.Abs (rangeRadius - m_rangeRadius) < m_epislon
                    && Mathf.Abs (rangeCenter.x - m_rangeCenter.x) < m_epislon
                    && Mathf.Abs (rangeCenter.y - m_rangeCenter.y) < m_epislon
                    && Mathf.Abs (rangeCenter.z - m_rangeCenter.z) < m_epislon; 
            }
        };

        private List<Vector3> m_validPositions = new List<Vector3>();

        private RangeQueryCache m_rangeQueryCache = new RangeQueryCache();

        public ValidPositionProvider (GameObject[] listOfHoles)
        {
            m_rangeQueryCache = new RangeQueryCache ();

            foreach (GameObject hole in listOfHoles) {
                Vector3 holePosition = hole.transform.position;
                m_validPositions.Add (holePosition);
            }
        }

        public bool IsValidPositionInRangeExists(Vector3 position, float range) {
            if (m_rangeQueryCache.isEqualToChachedRange (position, range)) {
                return true;
            }

            float rangeSqr = range * range;
            foreach(Vector3 validPosition in m_validPositions) {
                Vector3 diffVector = position - validPosition;
                float distanceSqr = diffVector.sqrMagnitude;
                if (distanceSqr < rangeSqr) {
                    m_rangeQueryCache.SetValidPositionForRange (position, range, validPosition);
                    return true;
                }
            }

            return false;
        }

        public Vector3 GetValidPositionInRange(Vector3 position, float range) {
            if (m_rangeQueryCache.isEqualToChachedRange (position, range)) {
                return m_rangeQueryCache.GetCachedValidPosition();
            }

            float rangeSqr = range * range;
            Vector3 result = position;
            foreach (Vector3 validPosition in m_validPositions) {
                Vector3 diffVector = position - validPosition;
                float distanceSqr = diffVector.sqrMagnitude;

                if (distanceSqr < rangeSqr) {
                    m_rangeQueryCache.SetValidPositionForRange (position, range, validPosition);
                    result = validPosition;
                }
            }
            return result;
        }
    }
}

