using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float m_snapRange = 1.7f;

    public GridPositionProvider (GameObject[] listOfHoles, float snapRange) {
        foreach (GameObject hole in listOfHoles) {
            m_gridPlaceholders.Add (new GridPlaceholder(hole));
        }

        m_snapRange = snapRange;
    }

    public bool IsValidPositionInRangeExists(Vector3 position) {
        if (m_rangeQueryCache.isEqualToChachedRange (position, m_snapRange)) {
            return true;
        }

        float rangeSqr = m_snapRange * m_snapRange;
        foreach (GridPlaceholder placholder in m_gridPlaceholders) {
            Vector3 placeholderPosition = placholder.GetWorldPosition();
            Vector3 diffVector = position - placeholderPosition;
            float distanceSqr = diffVector.sqrMagnitude;
            if (distanceSqr < rangeSqr) {
                m_rangeQueryCache.SetValidPlacholderForRange (position, m_snapRange, placholder);
                return true;
            }
        }

        return false;
    }

    public GridPlaceholder GetGridPlacholderInRange(Vector3 position) {
        if (m_rangeQueryCache.isEqualToChachedRange (position, m_snapRange)) {
            return m_rangeQueryCache.GetCachedValidPlacholder();
        }

        float rangeSqr = m_snapRange * m_snapRange;
        GridPlaceholder result = null;
        foreach (GridPlaceholder placholder in m_gridPlaceholders) {
            Vector3 placeholderPosition = placholder.GetWorldPosition();
            Vector3 diffVector = position - placeholderPosition;
            float distanceSqr = diffVector.sqrMagnitude;

            if (distanceSqr < rangeSqr) {
                m_rangeQueryCache.SetValidPlacholderForRange (position, m_snapRange, placholder);
                result = placholder;
            }
        }
        return result;
    }
}

