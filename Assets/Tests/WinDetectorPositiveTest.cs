using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class WinDetectorPositiveTest {

	[UnityTest]
	public IEnumerator DetectWinOnFirstRow() {
		var go = new GameObject("MyGameObject");
		var winDetectionScript = go.AddComponent<WinDetectionScript>();

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.row = 0;
		coordinate.column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
	}

	[UnityTest]
	public IEnumerator DetectWinOnMainDiagonal() {
		var go = new GameObject("MyGameObject");
		var winDetectionScript = go.AddComponent<WinDetectionScript>();

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.row = 0;
		coordinate.column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.row = 1;
		coordinate.column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		coordinate.row = 2;
		coordinate.column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.Blue);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.Blue));
	}

	[UnityTest]
	public IEnumerator DetectWinOnSecondaryDiagonal() {
		var go = new GameObject("MyGameObject");
		var winDetectionScript = go.AddComponent<WinDetectionScript>();

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.row = 0;
		coordinate.column = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.row = 1;
		coordinate.column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.row = 2;
		coordinate.column = 0;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.White));
	}

	[UnityTest]
	public IEnumerator DetectWinOnSecondColumn() {
		var go = new GameObject("MyGameObject");
		var winDetectionScript = go.AddComponent<WinDetectionScript>();

        yield return null;
        var coordinate = new GridCoordinate();
		coordinate.row = 0;
		coordinate.column = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.row = 1;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		coordinate.row = 2;
		winDetectionScript.DoMove(coordinate, BoardCellColor.White);
		
		bool expectedValue = true;
		Assert.AreEqual(expectedValue, winDetectionScript.IsWinDetected(BoardCellColor.White));
	}
}
