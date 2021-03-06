﻿/*


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Practice : MonoBehaviour {
	DateTime[] lastButtonPressed = new DateTime[i];

	void Start(){
		//前回ボタンを押した時間をデバッグ用に設定
		lastButtonPressed[i] = new DateTime "各配列のボタンを押した時間";
		//前回ボタンを押したのが昨日あればtrue, でなければfalse
		Debug.Log(lastButtonPressed[i]);
		}

	bool isButtonPressedWithinLastDay(DateTime lastButtonPressed){
		DateTime today = DateTime.Now;
		if	(today.Date - lastButtonPressed.Date == new TimeSpan(1,0,0,0,0)) {
		return true;
	}
	return false;
}
} 
	















public class Practice : MonoBehaviour {

	public GameObject[] enemy = new GameObject[100];
	public GameObject enemyPrefab;
	private string datetimeStr;



	void Start () {

		for (int i = 0; i < 100; i++) {
			enemy [i] = Instantiate (enemyPrefab, new Vector3 (2 * i, 0, 0), Quaternion.identity);
		}
	}


	void Update () {

		datetimeStr = System.DateTime.Now.ToString();
		Debug.Log(datetimeStr);

		if (Input.GetMouseButtonDown (0) == true) {
			for (int i = 0; i < 100; i++) {
				if (Random.Range (0, 2) == 0) {
					enemy [i].GetComponent<MeshRenderer> ().material.color = new Color (i/100f, 0, 0, 1);
				}
		}
	}

}

}



元MainSceneManagerスクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainSceneManager : MonoBehaviour {

	[SerializeField]
	Text[] titleText = new Text[3];

	[SerializeField]
	Text[] tapText = new Text[3];

	[SerializeField]
	Button[] titleButton = new Button[3];

	[SerializeField]
	Text[] runningText = new Text[3];
		
	[SerializeField]
	Text[] sumsText = new Text[3];

	//時間のコード
	DateTime[] lastButtonPressed = new DateTime[i];





	void Start () {

		//前回ボタンを押した時間を取得
		lastButtonPressed[i] = new DateTime "各配列のボタンを押した時間";

		for (int i = 0; i < 3; i++) {
			titleText [i].text = DataManager.instance.habitData.titles [i];
			tapText[i].text = "完了？";
			runningText[i].text = "連続" + DataManager.instance.habitData.daysRunning [i].ToString() +"日です！";
			sumsText[i].text = "現在" + DataManager.instance.habitData.daysSum [i].ToString() + "日です！" ;

			//【時間】 24時を過ぎた時に、ボタンを押せるようにする処理
			if (isButtonPressedWithinLastDay == true) {
				titleButton[0,1,2].interactable == true;
			} 

			//【時間】 もし1日以上たっていたら、連続記録を0にする処理 
			//(0日経過の時はボタン押せない、1日経過の時は記録継続、2日以上でリセットだから >= new TimeSpan(2,0,0,0,0)でよい？ ）
			for (int i = 0; i < 3; i++) {
				if(today.Date - lastButtonPressed[i].Date >= new TimeSpan(2,0,0,0,0)) {
					DataManager.instance.habitData.daysRunning [i] = 0;
			}
				

		
	}

	//【時間】 もし今日の日付 - 昨日のボタンを押した日付が1日以上だった場合、trueで稼す。
		bool isButtonPressedWithinLastDay (DateTime lastButtonPressed){
		DateTime today = DateTime.Now;
			for (int i = 0; i < 3; i++) {
				if(today.Date - lastButtonPressed[i].Date >= new TimeSpan(1,0,0,0,0)) {
					return true;
				}
				return false;
			}


	void Update () {
		if (Input.GetKeyDown ("space") == true) {
			DataManager.instance.Save ();
		}
	}


	public void SettingButton(){
		SceneManager.LoadScene("Setting");
	}


	public void RecordButton(){
		SceneManager.LoadScene("Record");
	}


	void ChangeRunning(int index, int num){
		DataManager.instance.habitData.daysRunning [index] = num;
		runningText [index].text = "連続" + num.ToString() + "日です！";
	}


	void ChangeSum(int index, int num){
		DataManager.instance.habitData.daysSum [index] = num;
		sumsText [index].text = "現在" + num.ToString() + "日です！";
	}


	public void OnTitleTextClick(int i){
		titleButton [i].interactable = false;
			
		//ここにボタンを押したときに時間を取得するコードを書く？

		if (titleButton [i].interactable == false) {
			tapText [i].text = "完了！";
		} 

		int currentDaysRunning = DataManager.instance.habitData.daysRunning [i];
		ChangeRunning (i, currentDaysRunning+ 1 );

		int currentDaysSum = DataManager.instance.habitData.daysSum [i];
		ChangeSum (i, currentDaysSum + 1 );

	}
		
}


*/