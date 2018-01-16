using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour {

	[SerializeField]
	public static DataManager instance;
	public HabitData habitData; 
	public CharacterData charaData;
	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
			habitData = new HabitData();
			charaData = new CharacterData();
			Load ();
		} else {
			Destroy (this.gameObject);
		}
	
	}

	void Start () {
		
		habitData.runningLog = csvManager.ReadCsvFile ("RunningLog.csv");
		habitData.PutRunningLog ();

	}


	public void Save(){
		//PlayerPrefs.SetString("キー（名前）,saveするもの")
		for (int i = 0; i < habitData.titles.Length; i++) {
			PlayerPrefs.SetString ("title[" + i.ToString() + "]", habitData.titles [i]);
		}

		for (int i = 0; i < habitData.contents.Length; i++) {
			PlayerPrefs.SetString("content[" + i.ToString() +"]" , habitData.contents[i]);
		}

		for (int i = 0; i < habitData.daysRunning.Length; i++) {
			PlayerPrefs.SetInt("daysRunning[" + i.ToString() +"]" , habitData.daysRunning[i]);
		}

		for (int i = 0; i < habitData.daysSum.Length; i++) {
			PlayerPrefs.SetInt("daysSum[" + i.ToString() +"]",habitData.daysSum[i] );
		}

		for (int i = 0; i < habitData.runningLogId.Length; i++) {
			PlayerPrefs.SetInt("runningLogId[" + i.ToString() +"]",habitData.runningLogId[i] );
		}


		//Mainのvoid Start の表示


	}





	void Load(){
		for (int i = 0; i < habitData.titles.Length; i++) {
			habitData.titles [i] = PlayerPrefs.GetString ("title[" + i.ToString () + "]");
		}

		for (int i = 0; i < habitData.contents.Length; i++) {
			habitData.contents [i] = PlayerPrefs.GetString ("content[" + i.ToString () + "]");
		}

		for (int i = 0; i < habitData.daysRunning.Length; i++) {
			habitData.daysRunning [i] = PlayerPrefs.GetInt ("daysRunning[" + i.ToString () + "]");
		}

		for (int i = 0; i < habitData.daysSum.Length; i++) {
			habitData.daysSum [i] = PlayerPrefs.GetInt ("daysSum[" + i.ToString () + "]");
		}

		for (int i = 0; i < habitData.runningLogId.Length; i++) {
			habitData.runningLogId [i] = PlayerPrefs.GetInt ("runningLogId[" + i.ToString () + "]");
		}







	}


	//ボタンを押したときの時間の保存
	public void SaveButtonClickedDateTime(int i){
		DateTime today = DateTime.Now;
		PlayerPrefs.SetInt ("lastButtonPressedYear" + i.ToString (), today.Year);
		PlayerPrefs.SetInt ("lastButtonPressedMonth" + i.ToString (), today.Month);
		PlayerPrefs.SetInt ("lastButtonPressedDate" + i.ToString (), today.Day);
	}

	public void ResetButtonClickedDateTime(int i){
		PlayerPrefs.SetInt ("lastButtonPressedYear" + i.ToString (), 2000);
		PlayerPrefs.SetInt ("lastButtonPressedMonth" + i.ToString (), 1);
		PlayerPrefs.SetInt ("lastButtonPressedDate" + i.ToString (), 1);
	}
}
