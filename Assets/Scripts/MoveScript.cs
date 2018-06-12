using UnityEngine;

public class MoveScript : MonoBehaviour {
    public delegate void GameOverAction();
    public static event GameOverAction OnGameOver;
    
    private BoardCellColor m_colorToMove = BoardCellColor.Blue;
    private GameObject m_grabbedBall;
    private GridPositionProvider m_gridPositionProvider;
    private WinDetector m_winDetector;
    private bool m_isGameOver = false;
    
    public GridPositionProvider GetGridPositionProvider() {
        return m_gridPositionProvider;
    }

    private void Start() {
        var configScript = GetComponent<ConfigScript>();
        InitGridPositionProvider(configScript);
        InitWinDetector(configScript);
    }

    private void InitGridPositionProvider(ConfigScript configScript) {
        var listOfHoles = GameObject.FindGameObjectsWithTag("Hole");
        var snapRange = configScript.GetSnapingRange();
        m_gridPositionProvider = new GridPositionProvider(listOfHoles, snapRange);
    }

    private void InitWinDetector(ConfigScript configScript) {
        var boardSize = configScript.GetBoardSize();
        var board = new Board(boardSize);
        m_winDetector = new WinDetector(board);
    }

    private void Update() {
        if (m_isGameOver) {
            return;
        }
        
        if (Input.GetMouseButtonDown(0)) {
            TryToGrabBall();
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (m_grabbedBall != null) {
                    MoveBall();
            }
        }
    }

    private void TryToGrabBall() {
        var hitInfo = new RaycastHit();
        var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (hit) {
            var hitGameObject = hitInfo.transform.gameObject;

            if (hitGameObject.tag == m_colorToMove.ToString()) {
                BackupBallData(hitGameObject);
                SetupBallToFollowMouse();
            }
        }
    }

    private void SetupBallToFollowMouse() {
        m_grabbedBall.AddComponent<FollowMouseScript>();
        m_grabbedBall.AddComponent<SnapToGridScript>();
    }

    private void BackupBallData(GameObject hitGameObject) {
        m_grabbedBall = hitGameObject;
    }

    private void MoveBall() {
        var position = m_grabbedBall.transform.position;
        if (m_gridPositionProvider.IsPivotInRangeExist(position)) {
            var newCoordinates = GetCoordinatesByPosition(position);

            // TODO build move in a better way and use color in the move data
            var previousCoordinates = m_grabbedBall.GetComponent<CoordinatesOnGridScript>();
            var move = new MoveData();
            move.startCoordinate = previousCoordinates.GetBoardCoordinates();
            move.endCoordinate = new BoardCoordinate(true, newCoordinates);

            if (move.IsChangingLocation()) {
                m_winDetector.DoMove(move, m_colorToMove);
                if (m_winDetector.IsWinDetected(m_colorToMove)) {
                    m_isGameOver = true;
                    if (OnGameOver != null) {
                        OnGameOver();
                    }
                }
                m_grabbedBall.GetComponent<CoordinatesOnGridScript>().SetGridCoordinates(newCoordinates);
                m_colorToMove = m_colorToMove == BoardCellColor.Blue ? BoardCellColor.White : BoardCellColor.Blue;
            }
        }

        UngrabBall();
    }

    private GridCoordinate GetCoordinatesByPosition(Vector3 position) {
        var combinedPosition = m_gridPositionProvider.GetCombinedPosition(position);
        var gridCoordinates = combinedPosition.GetGridCoordinates();
        return gridCoordinates;
    }

    private void UngrabBall() {
        m_grabbedBall = null;
    }
}