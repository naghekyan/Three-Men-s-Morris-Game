using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class WinDetectorNegativeTest {
    private readonly int m_boardSize = 3;

    [UnityTest]
    public IEnumerator NegativeDetectWinOnMainDiagonal() {
        var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;

        var move = new MoveData();

        var coordinate = new GridCoordinate();
        coordinate.m_row = 0;
        coordinate.m_column = 0;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.White);
        coordinate.m_row = 1;
        coordinate.m_column = 1;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);
        coordinate.m_row = 2;
        coordinate.m_column = 2;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);

        var expectedValue = false;
        Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
    }

    [UnityTest]
    public IEnumerator NegativeDetectWinOnSecondaryDiagonal() {
        var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;

        var move = new MoveData();
        var coordinate = new GridCoordinate();
        coordinate.m_row = 0;
        coordinate.m_column = 2;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.White);
        coordinate.m_row = 1;
        coordinate.m_column = 1;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.White);
        coordinate.m_row = 2;
        coordinate.m_column = 0;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);

        var expectedValue = false;
        Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
    }

    [UnityTest]
    public IEnumerator NegativeDetectWin1() {
        var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;
        var move = new MoveData();

        var coordinate = new GridCoordinate(0, 0);
        // coordinate.m_row = 0;
        // coordinate.m_column = 0;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);
        // coordinate.m_row = 1;
        // coordinate.m_column = 1;
        coordinate = new GridCoordinate(1, 1);
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);
        coordinate.m_column = 2;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);

        var expectedValue = false;
        Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
    }

    [UnityTest]
    public IEnumerator NegativeWinOnFirstRow() {
        var winDetectionScript = new WinDetector(m_boardSize);

        yield return null;

        var move = new MoveData();
        var coordinate = new GridCoordinate(0, 0);
        // coordinate.m_row = 0;
        // coordinate.m_column = 0;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);
        coordinate.m_column = 1;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.White);
        coordinate.m_column = 2;
        move.SetupFirstMoveToGrid(coordinate);
        winDetectionScript.DoMove(move, BoardCellColor.Blue);

        var expectedValue = false;
        Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
    }
}