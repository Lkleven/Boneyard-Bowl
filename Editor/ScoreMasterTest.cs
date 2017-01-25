using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


[TestFixture]
public class PointsMasterTest{
	private ScoreMaster scoreMaster;

	[SetUp]
	public void Setup(){
		scoreMaster = new ScoreMaster ();
	}

	[Test]
	public void T00ThreeStrikesReturn30(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 10, 10, 10 });
		Assert.AreEqual (scoreMaster.ScoreFrames (bowlList)[0], 30);
	}

	[Test]
	public void T01SpareInFirstFrameReturns15(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 5, 5, 5 });
		Assert.AreEqual (scoreMaster.ScoreFrames (bowlList)[0], 15);
	}

	[Test]
	public void T02SpareInSecondFrameReturns15(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 5,5, 5,5, 5});
		Assert.AreEqual (scoreMaster.ScoreFrames (bowlList)[1], 15);
	}

	[Test]
	public void T03StrikeAndSpare(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 5,5, 5});
		Assert.AreEqual (scoreMaster.ScoreFrames (bowlList)[0], 20);
		Assert.AreEqual (scoreMaster.ScoreFrames (bowlList)[1], 15);
	}

	[Test]
	public void T04PerfectScoreIs300(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10});
		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 300);
	}

	[Test]
	public void T05StrikeOneNineFiveIs35(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 1,9, 5});
		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 35);
	}

	[Test]
	public void T06StrikeFiveFiveFiveIs35(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 5,5, 5});
		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 35);
	}

	[Test]
	public void T07GutterBallTest(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 0,5, 5,0, 5});
		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 10);
	}

	[Test]
	public void T101FullGameShouldBe101(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 7,3, 5,1, 0,7, 10, 1,0, 7,3, 4,1, 5,3, 7,3,4});
		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 101);
	}
}