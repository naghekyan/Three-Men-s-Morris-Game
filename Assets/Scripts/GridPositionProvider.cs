using System.Collections.Generic;
using UnityEngine;

public class GridPositionProvider {
    private readonly List<GridPlaceholder> m_gridPlaceholders = new List<GridPlaceholder>();
    private readonly RangeQueryCache m_rangeQueryCache = new RangeQueryCache();
    private readonly float m_snapRange = 1.7f;

    public GridPositionProvider(GameObject[] listOfHoles, float snapRange) {
        foreach (var hole in listOfHoles) m_gridPlaceholders.Add(new GridPlaceholder(hole));

        m_snapRange = snapRange;
    }

    public bool IsValidPositionInRangeExists(Vector3 position) {
        if (m_rangeQueryCache.isEqualToChachedRange(position, m_snapRange)) return true;

        var rangeSqr = m_snapRange * m_snapRange;
        foreach (var placholder in m_gridPlaceholders) {
            var placeholderPosition = placholder.GetWorldPosition();
            var diffVector = position - placeholderPosition;
            var distanceSqr = diffVector.sqrMagnitude;
            if (distanceSqr < rangeSqr) {
                m_rangeQueryCache.SetValidPlacholderForRange(position, m_snapRange, placholder);
                return true;
            }
        }

        return false;
    }

    public GridPlaceholder GetGridPlacholderInRange(Vector3 position) {
        if (m_rangeQueryCache.isEqualToChachedRange(position, m_snapRange))
            return m_rangeQueryCache.GetCachedValidPlacholder();

        var rangeSqr = m_snapRange * m_snapRange;
        GridPlaceholder result = null;
        foreach (var placholder in m_gridPlaceholders) {
            var placeholderPosition = placholder.GetWorldPosition();
            var diffVector = position - placeholderPosition;
            var distanceSqr = diffVector.sqrMagnitude;

            if (distanceSqr < rangeSqr) {
                m_rangeQueryCache.SetValidPlacholderForRange(position, m_snapRange, placholder);
                result = placholder;
            }
        }

        return result;
    }

    private class RangeQueryCache {
        private readonly float m_epislon = float.Epsilon;
        private Vector3 m_rangeCenter;
        private float m_rangeRadius;
        private GridPlaceholder m_validPlacholder;

        public void SetValidPlacholderForRange(Vector3 rangeCenter, float rangeRadius,
            GridPlaceholder validPlacholder) {
            m_rangeCenter = rangeCenter;
            m_rangeRadius = rangeRadius;
            m_validPlacholder = validPlacholder;
        }

        public GridPlaceholder GetCachedValidPlacholder() {
            return m_validPlacholder;
        }

        public bool isEqualToChachedRange(Vector3 rangeCenter, float rangeRadius) {
            return Mathf.Abs(rangeRadius - m_rangeRadius) < m_epislon
                   && Mathf.Abs(rangeCenter.x - m_rangeCenter.x) < m_epislon
                   && Mathf.Abs(rangeCenter.y - m_rangeCenter.y) < m_epislon
                   && Mathf.Abs(rangeCenter.z - m_rangeCenter.z) < m_epislon;
        }
    }
}