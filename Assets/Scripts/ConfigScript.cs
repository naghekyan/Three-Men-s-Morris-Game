using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigScript : MonoBehaviour {

    public float m_snapRange = 1.7f;
	public int m_boardSize = 3;

	void Start () {
		
	}
	
	public float GetSnapingRange() {
		return m_snapRange;
	}

	public int GetBoardSize() {
		return m_boardSize;
	}
}
