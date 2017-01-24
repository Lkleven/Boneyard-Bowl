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
	private ActionMaster actionMaster;

	[SetUp]
	public void Setup(){
		actionMaster = new ActionMaster ();
	}

	[Test]
	public void T00PassingTest(){
		Assert.AreEqual (1, 1);
	}

	[Test]
	public void T01FirstBallStrikeReturnsEndTurn(){
		Assert.AreEqual (endTurn, actionMaster.Bowl(10));
	}

	[Test]
	public void T02Bowl8ReturnsTidy(){
		Assert.AreEqual (tidy, actionMaster.Bowl (8));
	}

	[Test]
	public void T03BowlTwoEightReturnsEndTurn(){
		actionMaster.Bowl (2);
		Assert.AreEqual (endTurn, actionMaster.Bowl(8));
	}

	[Test]
	public void T04Ball21ReturnsEndGame(){
		actionMaster.Bowl (10);	//bowl 1
		actionMaster.Bowl (10);	//bowl 3
		actionMaster.Bowl (10); //bowl 5
		actionMaster.Bowl (10); //bowl 7
		actionMaster.Bowl (10); //bowl 9
		actionMaster.Bowl (10); //bowl 11
		actionMaster.Bowl (10); //bowl 13
		actionMaster.Bowl (10); //bowl 15
		actionMaster.Bowl (10); //bowl 17
		actionMaster.Bowl (10); //bowl 19
		actionMaster.Bowl (10); //bowl 20
		Assert.AreEqual (endGame, actionMaster.Bowl (5)); //Bowl 21
	}

	[Test]
	public void T05DontQualifyForBall21ReturnsEndGameAtBall20(){
		actionMaster.Bowl (10);	//bowl 1
		actionMaster.Bowl (10);	//bowl 3
		actionMaster.Bowl (10); //bowl 5
		actionMaster.Bowl (10); //bowl 7
		actionMaster.Bowl (10); //bowl 9
		actionMaster.Bowl (10); //bowl 11
		actionMaster.Bowl (10); //bowl 13
		actionMaster.Bowl (10); //bowl 15
		actionMaster.Bowl (10); //bowl 17
		actionMaster.Bowl (4); //bowl 19
		Assert.AreEqual (endGame, actionMaster.Bowl (5)); //Bowl 20
	}

	[Test]
	public void T06BowlingSpareInBowl20ReturnsReset(){
		actionMaster.Bowl (10);	//bowl 1
		actionMaster.Bowl (10);	//bowl 3
		actionMaster.Bowl (10); //bowl 5
		actionMaster.Bowl (10); //bowl 7
		actionMaster.Bowl (10); //bowl 9
		actionMaster.Bowl (10); //bowl 11
		actionMaster.Bowl (10); //bowl 13
		actionMaster.Bowl (10); //bowl 15
		actionMaster.Bowl (10); //bowl 17
		actionMaster.Bowl (9); //bowl 19
		Assert.AreEqual (reset, actionMaster.Bowl (1)); //Bowl 20
	}

	[Test]
	public void T08CheckResetAtStrikeInLastFrame(){
		int[] bowls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (reset, actionMaster.Bowl (10));
	}

	[Test]
	public void T09ResetWhenSpareInBowl20(){
		int[] bowls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (reset, actionMaster.Bowl (9));
	}

	[Test]
	public void T10StrikeWithBall19And6OnBall20ReturnsTidy(){
		int[] bowls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (tidy, actionMaster.Bowl (6));
	}

	[Test]
	public void T11BensBowl20Test(){
		int[] bowls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (tidy, actionMaster.Bowl (0));
	}

	[Test]
	public void T12NathanBowlIndexTest(){	//[0][10] is a spare, not a strike
		int[] bowls = {0,10, 5};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (endTurn, actionMaster.Bowl (1));
	}

	[Test]
	public void T13Dondi10thFrameTurkey(){
		int[] bowls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		foreach (int bowl in bowls) {
			actionMaster.Bowl (bowl);
		}
		Assert.AreEqual (reset, actionMaster.Bowl(10));
		Assert.AreEqual (reset, actionMaster.Bowl(10));
		Assert.AreEqual (endGame, actionMaster.Bowl(10));
	}
}