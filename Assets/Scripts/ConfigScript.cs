using UnityEngine;

public class ConfigScript : MonoBehaviour {
    public int m_boardSize = 3;

    public float m_snapRange = 1.7f;

    private void Start() {
    }

    public float GetSnapingRange() {
        return m_snapRange;
    }

    public int GetBoardSize() {
        return m_boardSize;
    }
}