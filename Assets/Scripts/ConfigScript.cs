using UnityEngine;

public class ConfigScript : MonoBehaviour {
    private readonly int m_boardSize = 3;
    private readonly float m_snapRange = 1.7f;

    public float GetSnapingRange() {
        return m_snapRange;
    }

    public int GetBoardSize() {
        return m_boardSize;
    }
}