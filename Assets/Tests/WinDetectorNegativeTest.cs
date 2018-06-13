using System;
using System.Collections;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WinDetectorNegativeTest {

    WinDetector GetWinDetector() {
        const int boardSize = 3;
        var board = new Board(boardSize);
        var winDetector = new WinDetector(board);
        return winDetector;
    }
    
    [UnityTest]
    public IEnumerator NegativeDetectWinOnMainDiagonal() {

        yield return null;
        var winDetector = GetWinDetector();
        var move = new MoveData();

        var coordinate = new GridCoordinate(0, 0);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.White);
        winDetector.DoMove(move);
        coordinate = new GridCoordinate(1, 1);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);
        coordinate = new GridCoordinate(2, 2);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);

        const bool expectedValue = false;
        Assert.AreEqual(expectedValue, winDetector.IsWinDetected(BoardCellColor.Blue));
    }
    
    [UnityTest]
    public IEnumerator NegativeDetectWinOnSecondaryDiagonal() {

        yield return null;
        var winDetector = GetWinDetector();
        var move = new MoveData();
        var coordinate = new GridCoordinate(0, 2);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.White);
        winDetector.DoMove(move);
        coordinate = new GridCoordinate(1, 1);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.White);
        winDetector.DoMove(move);
        coordinate = new GridCoordinate(2, 0);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);

        const bool expectedValue = false;
        Assert.AreEqual(expectedValue, winDetector.IsWinDetected(BoardCellColor.Blue));
    }

    [UnityTest]
    public IEnumerator NegativeDetectWin1() {

        yield return null;
        var winDetector = GetWinDetector();
        var move = new MoveData();

        var coordinate = new GridCoordinate(0, 0);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);
        coordinate = new GridCoordinate(1, 1);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);
        coordinate.SetColumn(2);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);

        const bool expectedValue = false;
        Assert.AreEqual(expectedValue, winDetector.IsWinDetected(BoardCellColor.Blue));
    }

    [UnityTest]
    public IEnumerator NegativeWinOnFirstRow() {

        yield return null;
        var winDetector = GetWinDetector();
        var move = new MoveData();
        var coordinate = new GridCoordinate(0, 0);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);
        coordinate.SetColumn(1);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.White);
        winDetector.DoMove(move);
        coordinate.SetColumn(2);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);

        const bool expectedValue = false;
        Assert.AreEqual(expectedValue, winDetector.IsWinDetected(BoardCellColor.Blue));
    }
    
    [UnityTest]
    public IEnumerator NegativeOutOfGridRangeIndex() {

        yield return null;
        var winDetector = GetWinDetector();
        var move = new MoveData();
        var coordinate = new GridCoordinate(0, 0);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        winDetector.DoMove(move);
        coordinate.SetColumn(1);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.White);
        winDetector.DoMove(move);
        coordinate.SetColumn(7);
        move.SetupFirstMoveToGrid(coordinate, BoardCellColor.Blue);
        
        Assert.That(() => winDetector.DoMove(move), Throws.TypeOf<Exception>());
    }
}