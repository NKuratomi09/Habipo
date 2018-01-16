using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SettingSceneManager : MonoBehaviour {

	[SerializeField]
	InputField[] titleInputField = new InputField[3];

	[SerializeField]
	InputField[] contentInputField = new InputField[3];

	[SerializeField]
	Button[] deleteButton = new Button[3];

	[SerializeField]
	Canvas[] deleteCanvas = new Canvas[3];

	[SerializeField]
	Button[] yesButton = new Button[3];

	[SerializeField]
	Button[] noButton = new Button[3];

	[SerializeField]
	Canvas[] checkCanvas = new Canvas[2];

	[SerializeField]
	Button[] checkYesButton = new Button[2];

	[SerializeField]
	Button[] checkNoButton = new Button[2];


	public Image tabBarSettingIcon; 


	void Start () {
		
		tabBarSettingIcon.gameObject.SetActive (true);

		for (int i = 0; i < 3; i++) {
			titleInputField[i].text = DataManager.instance.habitData.titles [i];
			contentInputField[i].text = DataManager.instance.habitData.contents [i];

			deleteCanvas[i].gameObject.SetActive(false);
		}
	}

	void Update () {


	}

	//保存ボタン
	public void SaveButton(){
		for (int i = 0; i < 3; i++) {
			if (DataManager.instance.habitData.titles [i] != titleInputField [i].text) {
				DataManager.instance.habitData.titles [i] = titleInputField [i].text;
				DataManager.instance.ResetButtonClickedDateTime (i);
				DataManager.instance.habitData.daysRunning [i] = 0;
				DataManager.instance.habitData.daysSum [i] = 0;
			}

			DataManager.instance.habitData.contents[i] = contentInputField [i].text;

			//saveの呼び出し
			DataManager.instance.Save();

		}
	}


	//Page遷移するとき
	public void MainButton(){
		//SceneManager.LoadScene("Main");
		bool isChanged = false;
		//内容変更確認
		for (int i = 0; i < 3; i++) {
			if (DataManager.instance.habitData.titles [i] != titleInputField [i].text
			    || DataManager.instance.habitData.contents [i] != contentInputField [i].text) {
				isChanged = true;
			} 
		}
		if (isChanged == true) {
			checkCanvas [0].gameObject.SetActive (true);
		} else {
			SceneManager.LoadScene("Main");
		}
	}

	//Page遷移するとき
	public void RecordButton(){
		//SceneManager.LoadScene ("Record");
		bool isChanged = false;
		//内容変更確認
		for (int i = 0; i < 3; i++) {
			if (DataManager.instance.habitData.titles [i] != titleInputField [i].text
				|| DataManager.instance.habitData.contents [i] != contentInputField [i].text) {
				isChanged = true;
			} 
		}
		if (isChanged == true) {
			checkCanvas [1].gameObject.SetActive (true);
		} else {
			SceneManager.LoadScene("Record");
		}
	}


	public void CheckYesButton(){
		Debug.Log ("good");

		SceneManager.LoadScene("Main");
	
	}

	public void CheckNoButton(){
		Debug.Log ("good");

		checkCanvas[0].gameObject.SetActive(false);
	}


	public void CheckYesButton1(){
		SceneManager.LoadScene("Record");
	}



	public void CheckNoButton1(){
		checkCanvas[1].gameObject.SetActive(false);
	}

		

	//DeleteButton配列
	public void DeleteButton(int i){
			deleteCanvas[i].gameObject.SetActive(true);
	}


	//YesButton配列
	public void YesButton(){
		deleteCanvas[0].gameObject.SetActive(false);
		titleInputField [0].text = "";
		contentInputField [0].text = "";
		DataManager.instance.habitData.daysRunning [0] = 0;
		DataManager.instance.habitData.daysSum [0] = 0;
		//ボタンを押せるように時間をリセット
		DataManager.instance.ResetButtonClickedDateTime (0);
	}
		
	public void YesButton1(){
		deleteCanvas[1].gameObject.SetActive(false);
		titleInputField [1].text = "";
		contentInputField [1].text = "";
		DataManager.instance.habitData.daysRunning [1] = 0;
		DataManager.instance.habitData.daysSum [1] = 0;

		DataManager.instance.ResetButtonClickedDateTime (1);
	}

	public void YesButton2(){
		deleteCanvas[2].gameObject.SetActive(false);
		titleInputField [2].text = "";
		contentInputField [2].text = "";
		DataManager.instance.habitData.daysRunning [2] = 0;
		DataManager.instance.habitData.daysSum [2] = 0;
		DataManager.instance.ResetButtonClickedDateTime (2);

	}

	//NoButton配列
	public void NoButton(int i){
		deleteCanvas[i].gameObject.SetActive(false);

	}

	//public void NoButton1(){
		//deleteCanvas[1].gameObject.SetActive(false);

	//}

	//public void NoButton2(){
	//	deleteCanvas[2].gameObject.SetActive(false);

	//}
		

	}
