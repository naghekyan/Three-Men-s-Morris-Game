using UnityEngine;

public class SnapToGridScript : MonoBehaviour {
    private readonly float m_targetReachingThreshold = 0.01f;
    private float m_positionLerpAlpha = 0.35f;
    private GridPositionProvider m_gridPositionProvider;
    private bool m_isDropped = false;

    void Start() {
        var gameController = GameObject.FindWithTag("GameController");
        SetGridPositionProvider(gameController);
    }

    private void SetGridPositionProvider(GameObject gameController) {
        var moveScript = gameController.GetComponent<MoveScript>();
        m_gridPositionProvider = moveScript.GetGridPositionProvider();
    }

    void Update() {
        SnapToValidPosition();
        SnapToGridOnDrop();
    }

    private void SnapToValidPosition() {
        var position = transform.position;

        if (m_gridPositionProvider.IsPivotInRangeExist(position)) {
            var combinedPosition = m_gridPositionProvider.GetCombinedPosition(position);
            var targetPosition = combinedPosition.GetWorldPosition();
            transform.position = Vector3.Lerp(position, targetPosition, m_positionLerpAlpha);

            if (IsDroppedAndRichedToGridPosition(position, targetPosition)) {
                Destroy(this);
            }
        }
    }

    private bool IsDroppedAndRichedToGridPosition(Vector3 position, Vector3 targetPosition) {
        return m_isDropped && Vector3.Distance(position, targetPosition) < m_targetReachingThreshold;
    }

    private void SnapToGridOnDrop() {
        if (Input.GetMouseButtonUp(0)) {
            m_isDropped = true;
            m_positionLerpAlpha = 0.2f;
        }
    }
}