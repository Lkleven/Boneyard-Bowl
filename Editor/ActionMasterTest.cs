using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest{
	private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
	private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
	private ActionMaster.Action reset = ActionMaster.Action.Reset;
	private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
	private List<int> pinFalls;

	[SetUp]
	public void Setup(){
		pinFalls = new List<int>();
	}

	[Test]
	public void T00PassingTest(){
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01FirstBallStrikeReturnsEndTurn(){
		pinFalls.Add (10);
		Assert.AreEqual (endTurn, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T02Bowl8ReturnsTidy(){
		pinFalls.Add (8);
		Assert.AreEqual (tidy, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T03BowlTwoEightReturnsEndTurn(){
		pinFalls.AddRange(new int[]{ 2,8 });
		Assert.AreEqual (endTurn, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T04Ball21ReturnsEndGame(){
		pinFalls.AddRange(new int[]{ 10,10,10,10,10,10,10,10,10,10,10,10,5 });
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T05DontQualifyForBall21ReturnsEndGameAtBall20(){
		pinFalls.AddRange(new int[]{ 10,10,10,10,10,10,10,10,10,10,4,5 });
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T06BowlingSpareInBowl20ReturnsReset(){
		pinFalls.AddRange(new int[]{ 10,10,10,10,10,10,10,10,10,9,1 });
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T08CheckResetAtStrikeInLastFrame(){
		pinFalls.AddRange(new int[]{1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10});
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T09ResetWhenSpareInBowl20(){
		pinFalls.AddRange(new int[]{1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,9});
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T10StrikeWithBall19And6OnBall20ReturnsTidy(){
		pinFalls.AddRange(new int[]{1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,6});
			Assert.AreEqual (tidy, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T11BensBowl20Test(){
		pinFalls.AddRange(new int[]{1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,0});
		Assert.AreEqual (tidy, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T12NathanBowlIndexTest(){	//[0][10] is a spare, not a strike
		pinFalls.AddRange(new int[]{0,10, 5,1});
		Assert.AreEqual (endTurn, ActionMaster.NextAction (pinFalls));
	}

	[Test]
	public void T13Dondi10thFrameTurkey(){
		pinFalls.AddRange(new int[]{1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10});

		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
		pinFalls.Add (10);
		Assert.AreEqual (reset, ActionMaster.NextAction (pinFalls));
		pinFalls.Add (10);
		Assert.AreEqual (endGame, ActionMaster.NextAction (pinFalls));
	}
}