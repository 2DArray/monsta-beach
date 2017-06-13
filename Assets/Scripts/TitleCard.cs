using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour {
	public Animation titleCardAnim;
	public Text moveInstructions;
	public Text camInstructions;
	public Image fadeImage;
	public AnimationCurve moveExitCurve;
	public AnimationCurve camExitCurve;
	public float moveExitDist;
	public float camExitDist;

	bool hidTitle = false;

	float moveExitTimer = 0f;
	float camExitTimer = 0f;

	Vector2 moveStartPos;
	Vector2 camStartPos;

	bool firstFrame = true;

	float introTimer = 0f;

	void HideTitle() {
		hidTitle=true;
		titleCardAnim.Play();
	}

	void Start () {
		moveStartPos=moveInstructions.rectTransform.anchoredPosition;
		camStartPos=camInstructions.rectTransform.anchoredPosition;
	}
	
	void Update () {
		if (firstFrame) {
			firstFrame=false;
		} else {
			introTimer+=Time.deltaTime/3f;
			float t = Mathf.Clamp01(1f-introTimer);
			t=3f*t*t-2f*t*t*t;
			fadeImage.color=new Color(0f,0f,0f,t);
		}


		if (MonstaControl.moveTimer>2f) {
			if (hidTitle==false) {
				HideTitle();
			}
			moveExitTimer+=Time.deltaTime;

			moveInstructions.rectTransform.anchoredPosition=moveStartPos-Vector2.up*moveExitCurve.Evaluate(moveExitTimer)*moveExitDist;
		}
		if (CameraFollow.turnTimer>1f) {
			if (hidTitle==false) {
				HideTitle();
			}
			camExitTimer+=Time.deltaTime;

			camInstructions.rectTransform.anchoredPosition=camStartPos-Vector2.up*camExitCurve.Evaluate(camExitTimer)*camExitDist;
		}
	}
}
