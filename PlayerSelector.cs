using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Player selector.
/// Highlights and un-highligths graphics in the player selection menu
/// </summary>
public class PlayerSelector : MonoBehaviour {
	public Image[] playerImages;
	public Text playerText;

	private int playerCount = 1;
	private PlayerCountGO pcGO;
	private Color[] playerColors = new Color[4];

	// Use this for initialization
	void Start () {
		pcGO = GameObject.FindObjectOfType<PlayerCountGO> ();
		SelectNumberOfPlayers (playerCount);
		SetPlayerColorsArray ();

		//un-highlights all images except the first one (single player standard)
		for (int i = 1; i < playerImages.Length; i++) {
			Color tmp = playerImages [i].color;
			tmp.a = 0.2f;
			playerImages [i].color = tmp;
		}
	}

	// Highlights images equal to the current amount of players based on mouse-hover
	public void HighLightOnMouseOver(int playerNumber){
		for (int i = 0; i < playerImages.Length; i++) {
			if (i < playerNumber) {
				Color tmp = playerImages [i].color;
				tmp.a = 1f;
				playerImages [i].color = tmp;
			} else {
				Color tmp2 = playerImages [i].color;
				tmp2.a = 0.2f;
				playerImages [i].color = tmp2;
			}
		}
	}

	// Un-highlights images when mouse exits mouse-hover on an image without clicking 
	public void MouseExitDarken(){
		for (int i = 0; i < playerImages.Length; i++) {
			if (i < playerCount) {
				Color tmp = playerImages [i].color;
				tmp.a = 1f;
				playerImages [i].color = tmp;
			} else {
				Color tmp2 = playerImages [i].color;
				tmp2.a = 0.2f;
				playerImages [i].color = tmp2;
			}
		}
	}

	// Stores the selected number of players in PlayerCountGO.cs
	public void SelectNumberOfPlayers(int players){
		playerCount = players;
		playerText.text = players + " Player";
		pcGO.SetPlayerCount (players);
		pcGO.SetPlayerColorsArray (playerColors);
	}

	// Initialises player colors (set in unity inspector/player selector menu) for use by PlayerCountGO
	private void SetPlayerColorsArray(){
		for (int i = 0; i < playerImages.Length; i++) {
			Color tmp = playerImages [i].color;
			playerColors [i] = tmp;
		}
	}
}
