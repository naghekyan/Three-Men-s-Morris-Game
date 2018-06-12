using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {
	private Animator m_anim;

	
	void Start () {
		m_anim = GetComponent<Animator>();
		MoveScript.OnGameOver += ShowGameOverAnim;
	}
	
	private void ShowGameOverAnim() {
		m_anim.SetTrigger("GameOver");
	}
}
