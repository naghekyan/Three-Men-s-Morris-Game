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
                ProcessBallDrop();
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

    private void ProcessBallDrop() {
        var position = m_grabbedBall.transform.position;
        if (IsBallPutOnGrid(position)) {
            var move = CreateMove(position);

            if (move.IsChangingLocation()) {
                MoveBall(move);
                SwitchPlayer();
            }
        }

        UngrabBall();
    }

    private MoveData CreateMove(Vector3 position) {
        GridCoordinate newCoordinates = GetCoordinatesByPosition(position);
        var previousCoordinates = m_grabbedBall.GetComponent<CoordinatesOnGridScript>().GetBoardCoordinates();;
        var endCoordinate = new BoardCoordinate(true, newCoordinates);
        
        var move = new MoveData(m_colorToMove, previousCoordinates, endCoordinate);
        return move;
    }

    private bool IsBallPutOnGrid(Vector3 position) {
        return m_gridPositionProvider.IsPivotInRangeExist(position);
    }

    private void MoveBall(MoveData move) {
        m_winDetector.DoMove(move);
        if (m_winDetector.IsWinDetected(m_colorToMove)) {
            m_isGameOver = true;
            if (OnGameOver != null) {
                OnGameOver();
            }
        }

        var finalGridCoordinates = move.m_endCoordinate.m_gridCoordinate;
        m_grabbedBall.GetComponent<CoordinatesOnGridScript>().SetGridCoordinates(finalGridCoordinates);
    }

    private void SwitchPlayer() {
        m_colorToMove = m_colorToMove == BoardCellColor.Blue ? BoardCellColor.White : BoardCellColor.Blue;
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