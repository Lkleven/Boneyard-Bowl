using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;			//arrays.ToList()


[TestFixture]
public class ScoreMasterTest{

	[Test]
	public void T00ThreeStrikesReturn30(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 10, 10, 10 });
		Assert.AreEqual (ScoreMaster.ScoreFrames (bowlList)[0], 30);
	}

	[Test]
	public void T01SpareInFirstFrameReturns15(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 5, 5, 5 });
		Assert.AreEqual (ScoreMaster.ScoreFrames (bowlList)[0], 15);
	}

	[Test]
	public void T02SpareInSecondFrameReturns15(){
		List<int> bowlList = new List<int> ();
		bowlList.AddRange (new int[]{ 5,5, 5,5, 5});
		Assert.AreEqual (ScoreMaster.ScoreFrames (bowlList)[1], 15);
	}

	[Test]
	public void T03StrikeAndSpare(){
		List<int> bowlList= new List<int> ();
		bowlList.AddRange (new int[]{ 10, 5,5, 5});
		Assert.AreEqual (ScoreMaster.ScoreFrames (bowlList)[0], 20);
		Assert.AreEqual (ScoreMaster.ScoreFrames (bowlList)[1], 15);
	}


	//REMOVED METHOD SumScore()
//	[Test]
//	public void T04PerfectScoreIs300(){
//		List<int> bowlList= new List<int> ();
//		bowlList.AddRange (new int[]{ 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10});
//		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 300);
//	}
//
//	[Test]
//	public void T05StrikeOneNineFiveIs35(){
//		List<int> bowlList= new List<int> ();
//		bowlList.AddRange (new int[]{ 10, 1,9, 5});
//		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 35);
//	}
//
//	[Test]
//	public void T06StrikeFiveFiveFiveIs35(){
//		List<int> bowlList= new List<int> ();
//		bowlList.AddRange (new int[]{ 10, 5,5, 5});
//		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 35);
//	}
//
//	[Test]
//	public void T07GutterBallTest(){
//		List<int> bowlList= new List<int> ();
//		bowlList.AddRange (new int[]{ 0,5, 5,0, 5});
//		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 10);
//	}
//
//	[Test]
//	public void T101FullGameShouldBe101(){
//		List<int> bowlList= new List<int> ();
//		bowlList.AddRange (new int[]{ 10, 7,3, 5,1, 0,7, 10, 1,0, 7,3, 4,1, 5,3, 7,3,4});
//		Assert.AreEqual (scoreMaster.SumScore (scoreMaster.ScoreFrames (bowlList)), 101);
//	}


	[Test] 
	public void L01Bowl23(){
		List<int> bowlList= new List<int> ();
		List<int> framesList= new List<int> ();
		bowlList.AddRange (new int[]{2,3});
		framesList.Add(5);
		//Debug.Log (ScoreMaster.ScoreFrames (bowlList)[1]);
		Assert.AreEqual(framesList, ScoreMaster.ScoreFrames(bowlList));
	}

	[Test]
	public void L02Bowl234 () {
		int[] rolls = {2,3,4};
		int[] frames = { 5};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L03Bowl2345 () {
		int[] rolls = {2,3,4,5};
		int[] frames = { 5,  9};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L04Bowl23456 () {
		int[] rolls = {2,3,4,5,6};
		int[] frames = { 5,  9};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L05Bowl234561 () {
		int[] rolls = {2,3,4,5,6,1};
		int[] frames = { 5,  9,  7};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L06Bowl2345612 () {
		int[] rolls = {2,3, 4,5, 6,1, 2};
		int[] frames = { 5,  9,    7};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L07BowlX1 () {
		int[] rolls = {10, 1};
		int[] frames = {};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L08Bowl19 () {
		int[] rolls = {1, 9};
		int[] frames = {};
		//Debug.Log(ScoreMaster.ScoreFrames (rolls.ToList()).Count);
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L09Bowl123455 () {
		int[] rolls = {1,2, 3,4, 5,5};
		int[] frames = { 3,   7};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L10SpareBonus () {
		int[] rolls = {1,2, 3,5, 5,5, 3,3};
		int[] frames = { 3,   8,  13,   6};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L11SpareBonus2 () {
		int[] rolls = {1,2, 3,5, 5,5, 3,3, 7,1, 9,1, 6};
		int[] frames = { 3,   8,  13,   6,   8,  16};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L12StrikeBonus () {
		int[] rolls = { 10, 3,4};
		int[] frames = {17,   7};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L13StrikeBonus3 () {
		int[] rolls = { 1,2, 3,4, 5,4, 3,2, 10, 1,3, 3,4};
		int[] frames = {  3,   7,   9,   5, 14,   4,   7};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L14MultiStrikes () {
		int[] rolls = { 10, 10, 2,3};
		int[] frames = {22, 15,   5};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L15MultiStrikes3 () {
		int[] rolls = { 10, 10, 2,3, 10, 5,3};
		int[] frames = {22, 15,   5, 18,   8};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L16TestGutterGame () {
		int[] rolls = { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0};
		int[] totalS = {  0,   0,   0,   0,   0,   0,   0,   0,   0,   0};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	[Test]
	public void L17TestAllOnes () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		int[] totalS = {  2,   4,   6,   8,  10,  12,  14,  16,  18,  20};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	[Test]
	public void L18TestAllStrikes () {
		int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,10,10};
		int[] totalS = {30, 60, 90,120,150,180,210,240,270,      300};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	[Test]
	public void L19TestImmediateStrikeBonus() {
		int[] rolls = {5,5, 3};
		int[] frames = {13};
		Assert.AreEqual (frames.ToList(), ScoreMaster.ScoreFrames (rolls.ToList()));
	}

	[Test]
	public void L20SpareInLastFrame () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9,7};
		int[] totalS = {  2,   4,   6,   8,  10,  12,  14,  16,  18,    35};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	[Test]
	public void L21StrikeInLastFrame () {
		int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,2,3};
		int[] totalS = {  2,   4,   6,   8,  10,  12,  14,  16,  18,     33};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	// http://slocums.homestead.com/gamescore.html
	[Test]
	[Category ("Verification")]
	public void TG02GoldenCopyA () {
		int[] rolls = { 10, 7,3, 9,0, 10, 0,8, 8,2, 0,6, 10, 10, 10,8,1};
		int[] totalS = {20,  39,  48, 66,  74,  84,  90,120,148,    167};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB1of3 () {
		int[] rolls = { 10, 9,1, 9,1, 9,1, 9,1, 7,0, 9,0, 10, 8,2, 8,2,10};
		int[] totalS = {20,  39,  58,  77,  94, 101, 110,130, 148,    168};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB2of3 () {
		int[] rolls = { 8,2, 8,1, 9,1, 7,1, 8,2, 9,1, 9,1, 10, 10, 7,1};
		int[] totalS = { 18,  27,  44,  52,  71,  90, 110,137,155, 163};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	//http://guttertoglory.com/wp-content/uploads/2011/11/score-2011_11_28.png
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyB3of3 () {
		int[] rolls = { 10, 10, 9,0, 10, 7,3, 10, 8,1, 6,3, 6,2, 9,1,10};
		int[] totalS = {29, 48,  57, 77,  97,116, 125, 134, 142,    162};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyC1of3 () {
		int[] rolls = { 7,2, 10, 10, 10, 10, 7,3, 10, 10, 9,1, 10,10,9};
		int[] totalS = {  9, 39, 69, 96,116, 136,165,185, 205,     234};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}

	// http://brownswick.com/wp-content/uploads/2012/06/OpenBowlingScores-6-12-12.jpg
	[Category ("Verification")]
	[Test]
	public void TG03GoldenCopyC2of3 () {
		int[] rolls = { 10, 10, 10, 10, 9,0, 10, 10, 10, 10, 10,9,1};
		int[] totalS = {30, 60, 89,108, 117,147,177,207,236,    256};
		Assert.AreEqual (totalS.ToList(), ScoreMaster.ScoreCumulative (rolls.ToList()));
	}
}