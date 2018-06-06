public class WinDetectorPositiveTest {
    private int m_boardSize = 3;
/* 
	[UnityTest]
	public IEnumerator DetectWinOnFirstRow() {
		
		var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.m_row = 0;
		coordinate.m_column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.m_column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.m_column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
	}

	[UnityTest]
	public IEnumerator DetectWinOnMainDiagonal() {
		
		var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.m_row = 0;
		coordinate.m_column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.m_row = 1;
		coordinate.m_column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.m_row = 2;
		coordinate.m_column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
	}

	[UnityTest]
	public IEnumerator DetectWinOnSecondaryDiagonal() {
		
		var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;
        var coordinate = new GridCoordinate(m_boardSize);
		coordinate.m_row = 0;
		coordinate.m_column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.m_row = 1;
		coordinate.m_column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.m_row = 2;
		coordinate.m_column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.White));
	}

	[UnityTest]
	public IEnumerator DetectWinOnSecondColumn() {
		
		var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.m_row = 0;
		coordinate.m_column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.m_row = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.m_row = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.White));
	}*/
}