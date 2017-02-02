using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;			//arrays.ToList()


[TestFixture]
public class ScoreDisplayTest{
	private List<int> rolls;
	
	[SetUp]
	public void Setup(){
		rolls = new List<int>();
	}

	[Test]
	public void T00PassingTest(){
		Assert.AreEqual (1,1);
	}

	[Test]
	public void T01Bowl1(){
		rolls.AddRange (new int[]{ 1 });
		string rollString = "1";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	[Test]
	public void T02Bowl55(){
		rolls.AddRange (new int[]{ 10, 10, 10, 5 });
		string rollString = "X X X 5";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	[Test]
	public void T03Bowl19(){
		rolls.AddRange (new int[]{ 1, 9 });
		string rollString = "1/";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	[Test]
	public void T04BowlStrikeInLastFrame(){
		rolls.AddRange (new int[]{ 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,1,1 });
		string rollString = "111111111111111111X11";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	[Test]
	public void T05Bowl0(){
		rolls.AddRange (new int[]{ 0 });
		string rollString = "-";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	[Test]
	public void T06Bow010(){
		rolls.AddRange (new int[]{ 0, 10 });
		string rollString = "-/";
		Assert.AreEqual (rollString, ScoreDisplay.FormatRolls (rolls));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG01GoldenCopyB1of3 () {
		int[] rolls = { 10, 9,1, 9,1, 9,1, 9,1, 7,0, 9,0, 10, 8,2, 8,2,10};
		string rollsString = "X 9/9/9/9/7-9-X 8/8/X";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG02GoldenCopyB2of3 () {
		int[] rolls = { 8,2, 8,1, 9,1, 7,1, 8,2, 9,1, 9,1, 10, 10, 7,1};
		string rollsString = "8/819/718/9/9/X X 71";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB3of3 () {
		int[] rolls = { 10, 10, 9,0, 10, 7,3, 10, 8,1, 6,3, 6,2, 9,1,10};
		string rollsString = "X X 9-X 7/X 8163629/X";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG04GoldenCopyC1of3 () {
		int[] rolls = { 7,2, 10, 10, 10, 10, 7,3, 10, 10, 9,1, 10,10,9};
		string rollsString = "72X X X X 7/X X 9/XX9";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG05GoldenCopyC2of3 () {
		int[] rolls = { 10, 10, 10, 10, 9,0, 10, 10, 10, 10, 10,9,1};
		string rollsString = "X X X X 9-X X X X X9/";
		Assert.AreEqual (rollsString, ScoreDisplay.FormatRolls (rolls.ToList()));
	}
}
