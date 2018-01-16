using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainSceneManager : MonoBehaviour
{
	public Image tabBarMainIcon; 

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

	[SerializeField]
	Image[] coverImage = new Image[3];

	//習慣少ない時用
	[SerializeField]
	GameObject startScreen;

	[SerializeField]
	GameObject[] habitGroup = new GameObject[3];

	[SerializeField]
	Image[] runningRange = new Image[3];

	//LineRenderer
	[SerializeField]
	Image[] line = new Image[5];

	[SerializeField]
	Text[] countText = new Text[3];

	[SerializeField]
	AudioClip jumpSound;
	[SerializeField]
	AudioClip GrowthSound;
	[SerializeField]
	AudioClip HibiSound;
	[SerializeField]
	AudioClip EggSound;

	AudioSource audioSource;

	[SerializeField]
	Image TurorialImage;




	//開発用ボタン
	public void testButton ()
	{
		for (int i = 0; i < 3; i++) {
			//	OnTitleTextClick (i);
			titleButton [i].interactable = true;
		}
		DataManager.instance.Save ();
	}





	//キャラクター
	[SerializeField] GameObject[] egg = new GameObject[3];
	[SerializeField] GameObject[] hibiEgg = new GameObject[3];
	[SerializeField] GameObject[] hiyoko = new GameObject[3];
	[SerializeField] GameObject[] bird = new GameObject[3];
	[SerializeField] GameObject[] character30 = new GameObject[3];
	[SerializeField] GameObject[] character60 = new GameObject[3];
	[SerializeField] GameObject[] character90 = new GameObject[3];

	public GameObject[] character = new GameObject[3];

	[SerializeField] GameObject gokouPrefab;
	GameObject gokou;
		
	public float jump = 5;

	public GameObject Canvas;


	//【時間】のコード
	DateTime[] lastButtonPressed = new DateTime[3];
	//←ここは配列の要素数を入れる必要あり

	public void GokouButton ()
	{
		gokou.gameObject.GetComponent<Rigidbody> ().useGravity = true;
	}




	void Start ()
	{	
		Debug.Log(csvManager.OpenTextFile(csvManager.GetPath() + "RunningLog.csv"));
		//Debug
		tabBarMainIcon.gameObject.SetActive (true);
		gokou = Instantiate (gokouPrefab, new Vector3 (-6.8f , 22f, 60f), Quaternion.identity) as GameObject;
		gokou.transform.Rotate (new Vector3 (0, 180, 0));



		//【キャラクター】画面遷移で生成
		//audiosource
		for (int i = 0; i < 3; i++) {
			firstCharacterSpawner (i);
			audioSource = titleButton[i].gameObject.GetComponent<AudioSource> ();
		}

	

		//daysRunnningのDebug
		for (int i = 0; i < 3; i++) {
			Debug.Log (DataManager.instance.habitData.daysRunning [i]);
		}


		Debug.Log ("lineのテスト");
		//line [0] 横上　line [1] 横下　line [2] 縦左　line [3] 縦右　line [4] 縦真ん中
		//全部入っている時 
		if (DataManager.instance.habitData.titles[0] != "" && DataManager.instance.habitData.titles[1] != "" && DataManager.instance.habitData.titles[2] != "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
			line [2].gameObject.SetActive (true);
			line [3].gameObject.SetActive (true);
		}//二つ入っている時 
		else if (DataManager.instance.habitData.titles[0] != "" && DataManager.instance.habitData.titles[1] != "" && DataManager.instance.habitData.titles[2] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
			line [4].gameObject.SetActive (true);
		} else if (DataManager.instance.habitData.titles[1] != "" && DataManager.instance.habitData.titles[2] != "" && DataManager.instance.habitData.titles[0] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
			line [4].gameObject.SetActive (true);
		} else if (DataManager.instance.habitData.titles[2] != "" && DataManager.instance.habitData.titles[0] != "" && DataManager.instance.habitData.titles[1] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
			line [4].gameObject.SetActive (true);
		}
		//一つ入っている時
		else if (DataManager.instance.habitData.titles[0] != "" && DataManager.instance.habitData.titles[1] == "" && DataManager.instance.habitData.titles[2] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
		}else if (DataManager.instance.habitData.titles[1] != "" && DataManager.instance.habitData.titles[0] == "" && DataManager.instance.habitData.titles[2] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
		}else if (DataManager.instance.habitData.titles[2] != "" && DataManager.instance.habitData.titles[1] == "" && DataManager.instance.habitData.titles[2] == "") {
			line [0].gameObject.SetActive (true);
			line [1].gameObject.SetActive (true);
		}
		//全部入ってない時
		else if (DataManager.instance.habitData.titles[0] == "" && DataManager.instance.habitData.titles[1] == "" && DataManager.instance.habitData.titles[2] == "") {
			Debug.Log ("hoge");
		}
		


		//int left = 0 - Canvas.GetComponent<CanvasScaler> ().referenceResolution.x / 2;





		for (int i = 0; i < 3; i++) {
			titleText [i].text = DataManager.instance.habitData.titles [i];
			tapText [i].text = "完了？";
			runningText [i].text = "連続" + DataManager.instance.habitData.daysRunning [i].ToString () + "日目！";
			sumsText [i].text = "現在" + DataManager.instance.habitData.daysSum [i].ToString () + "日です！";

			//タイトルに何も入ってない時は文字を変えて、ボタンを押せないようにする。
			Vector3 pos = transform.position;
			transform.position = new Vector3(pos.x, pos.y + 2, pos.z);

			//widthをGetComponent
			Vector2 half = runningRange [0].gameObject.GetComponent<RectTransform> ().sizeDelta;
			half.x = 621;

			Vector2 full = runningRange [0].gameObject.GetComponent<RectTransform> ().sizeDelta;
			full.x = 1242;

			if (titleText [0].text == "" && titleText [1].text == "" && titleText [2].text == "") {
				startScreen.gameObject.SetActive (true);
			} else if (titleText [0].text == "" && titleText [1].text == "") {
				habitGroup [0].gameObject.SetActive (false);
				habitGroup [1].gameObject.SetActive (false);
				habitGroup [2].gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -548.5f, 0);
				runningRange [2].gameObject.GetComponent<RectTransform> ().sizeDelta = full;
				Debug.Log ("hoge");
			} else if (titleText [0].text == "" && titleText [2].text == "") {
				habitGroup [0].gameObject.SetActive (false);
				habitGroup [2].gameObject.SetActive (false);
				habitGroup [1].gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -548.5f, 0);
				runningRange [1].gameObject.GetComponent<RectTransform> ().sizeDelta = full;
			} else if (titleText [1].text == "" && titleText [2].text == "") {
				habitGroup [1].gameObject.SetActive (false);
				habitGroup [2].gameObject.SetActive (false);
				habitGroup [0].gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -548.5f, 0);
				runningRange [0].gameObject.GetComponent<RectTransform> ().sizeDelta = full;
			}else if (titleText [0].text == "") {
				habitGroup [0].gameObject.SetActive (false);
				habitGroup [1].gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (-310.5f, -548.5f, 0);
				habitGroup [2].gameObject.GetComponent<RectTransform> ().localPosition= new Vector3 (310.5f, -548.5f, 0);
				runningRange [1].gameObject.GetComponent<RectTransform> ().sizeDelta = half ;
				runningRange [2].gameObject.GetComponent<RectTransform> ().sizeDelta = half ;
			}else if (titleText [1].text == "") {
				habitGroup [1].gameObject.SetActive (false);
				habitGroup [0].gameObject.GetComponent<RectTransform> ().localPosition= new Vector3 (-310.5f, -548.5f, 0);
				habitGroup [2].gameObject.GetComponent<RectTransform> ().localPosition= new Vector3 (310.5f, -548.5f, 0);
				runningRange [0].gameObject.GetComponent<RectTransform> ().sizeDelta = half;
				runningRange [2].gameObject.GetComponent<RectTransform> ().sizeDelta = half;
			}else if (titleText [2].text == "") {
				habitGroup [2].gameObject.SetActive (false);
				habitGroup [0].gameObject.GetComponent<RectTransform> ().localPosition= new Vector3 (-310.5f, -548.5f, 0);
				habitGroup [1].gameObject.GetComponent<RectTransform> ().localPosition= new Vector3 (310.5f, -548.5f, 0);
				runningRange [0].gameObject.GetComponent<RectTransform> ().sizeDelta = half;
				runningRange [1].gameObject.GetComponent<RectTransform> ().sizeDelta = half;
			}
		}
			


		//【時間】前回ボタンを押した時間を取得 ←PlayerPrefsから読み込む必要あり、DateTime型はPlayerPrefsで保存できないので、年、月、日に分けてint型で保存しておく。
		for (int i = 0; i < lastButtonPressed.Length; i++) {
			int year;
			if(PlayerPrefs.HasKey("lastButtonPressedYear" + i.ToString())){
				year = PlayerPrefs.GetInt ("lastButtonPressedYear" + i.ToString());
			}else{
				year = 2000;
			}
			int month;
			if(PlayerPrefs.HasKey("lastButtonPressedMonth" + i.ToString())){
				month = PlayerPrefs.GetInt ("lastButtonPressedMonth" + i.ToString());
			}else{
				month = 1;
			}
			int date;
			if(PlayerPrefs.HasKey("lastButtonPressedDate" + i.ToString())){
				date = PlayerPrefs.GetInt ("lastButtonPressedDate" + i.ToString());
			}else{
				date = 1;
			}
			DateTime newDateTime = new DateTime (year, month, date, 0, 0, 0);
			Debug.Log ("Button[" + i + "]=" + newDateTime);
			lastButtonPressed [i] = newDateTime;
		}
			


		//【時間】 24時を過ぎた時に、ボタンを押せるようにする処理
		DateTime today = DateTime.Now;
		for (int i = 0; i < lastButtonPressed.Length; i++) {
			//if (lastButtonPressed [i].Date - today.Date != new TimeSpan (0, 0, 0, 0, 0)) {//←今日以外の日にボタンを押してた場合
				//10秒経過したときに、ボタンを押せるようにする処理
				//if ( today.Date - lastButtonPressed [i].Date > new TimeSpan (0, 0, 0, 10, 0)) {
			//titleButton [i].interactable = true;
			if (today.Date - lastButtonPressed [i].Date == new TimeSpan (0, 0, 0, 0, 0)) {
				titleButton [i].interactable = false;
			} else {
				titleButton [i].interactable = true;
			}
		}
	
		//【時間】 もし1日以上たっていたら、連続記録を0にする処理 
		for (int i = 0; i < lastButtonPressed.Length; i++) {
			if (isButtonPressedWithinLastDay (lastButtonPressed [i]) == false) {
				DataManager.instance.habitData.daysRunning [i] = 0;
			}
		}
	}

	//【時間】 もし今日の日付 - 昨日のボタンを押した日付が1日以下だった場合、trueで稼す。
	bool isButtonPressedWithinLastDay (DateTime lastButtonPressed)
	{
		DateTime today = DateTime.Now;
		if (today.Date - lastButtonPressed.Date <= new TimeSpan (1, 0, 0, 0, 0)) {
			return true;
		}
		return false;
	}


	void Update ()
	{
		//開発者用仕様
		if (Input.GetKeyDown ("space") == true) {
			for (int i = 0; i < 3; i++) {
			//	OnTitleTextClick (i);
				titleButton [i].interactable = true;
			}
			DataManager.instance.Save ();
		}

		//counttext
		for (int i = 0; i < 3; i++) {
			int[] countNumber = new int[3];

			if (DataManager.instance.habitData.daysRunning [i] >= 90) {
				countNumber [i] = 120;
			} else if (DataManager.instance.habitData.daysRunning [i] >= 60) {
				countNumber [i] = 90;
			} else if (DataManager.instance.habitData.daysRunning [i] >= 30) {
				countNumber [i] = 60;
			} else if (DataManager.instance.habitData.daysRunning [i] >= 14) {
				countNumber [i] = 30;
			} else if (DataManager.instance.habitData.daysRunning [i] >= 7) {
				countNumber [i] = 14;
			} else {
				countNumber [i] = 7;
			}
		

			int[] countSum  = new int[3];
			countSum[i] = countNumber [i] - DataManager.instance.habitData.daysRunning [i];

			countText[i].text = "あと" + countSum[i] + "日で・・";
		}

		//テスト用
		if (Input.GetKeyDown ("left") == true) {
				tapButton (0);
				OnTitleTextClick(0);
			DataManager.instance.Save ();
		}
		if (Input.GetKeyDown ("up") == true) {
			tapButton (1);
			OnTitleTextClick(1);
			DataManager.instance.Save ();
		}
		if (Input.GetKeyDown ("right") == true) {
			tapButton (2);
			OnTitleTextClick(2);

			DataManager.instance.Save ();
		}

		//goku
		gokou.gameObject.transform.position += new Vector3(3,0,0) * Time.deltaTime;




		for (int i = 0; i < 3; i++) {
			if (titleButton [i].interactable == false) {
				tapText [i].text = "おつかれ！";
			} 
			if (titleText[i].text == "") {
				tapText[i].text = "";
			}
		}
	}

	public void TutorialButton(){
		bool tutorialScreen = true;
		if (tutorialScreen = true) {
			TurorialImage.gameObject.SetActive (true);
		}
	}

	public void TutorialFinishButton(){
		bool tutorialFinishScreen = true;
		if (tutorialFinishScreen = true) {
			TurorialImage.gameObject.SetActive (false);
	}
	}
		

	public void SettingButton ()
	{
		SceneManager.LoadScene ("Setting");
	}


	public void RecordButton ()
	{
		SceneManager.LoadScene ("Record");
	}


	void ChangeRunning (int index, int num)
	{
		DataManager.instance.habitData.daysRunning [index] = num;
		runningText [index].text = "連続" + num.ToString () + "日です！";
	}


	void ChangeSum (int index, int num)
	{
		DataManager.instance.habitData.daysSum [index] = num;
		sumsText [index].text = "現在" + num.ToString () + "日です！";
	}


	//ボタンを押したとき（データ管理用）
	public void OnTitleTextClick (int i)
	{
		titleButton [i].interactable = false;
		DataManager.instance.SaveButtonClickedDateTime (i);
		int currentDaysRunning = DataManager.instance.habitData.daysRunning [i];
		ChangeRunning (i, currentDaysRunning + 1);
		int currentDaysSum = DataManager.instance.habitData.daysSum [i];
		ChangeSum (i, currentDaysSum + 1);
		if (DataManager.instance.habitData.daysRunning [i] == 1) {
			DataManager.instance.habitData.AddLog (i);
		} else {
			DataManager.instance.habitData.UpdateLog (i);
		}
		DataManager.instance.Save();
	}



	//【キャラクター】画面遷移のキャラクター生成の関数
	public void firstCharacterSpawner(int i){
		if (DataManager.instance.habitData.daysRunning [i] >= 90) {
			GameObject character90Clone = Instantiate (character90 [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			character90Clone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = character90Clone; 
		} else if (DataManager.instance.habitData.daysRunning [i] >= 60) {
			GameObject character60Clone = Instantiate (character60 [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			character60Clone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = character60Clone; 
		} else if (DataManager.instance.habitData.daysRunning [i] >= 30) {
			GameObject character30Clone = Instantiate (character30 [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			character30Clone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = character30Clone; 
		}else if (DataManager.instance.habitData.daysRunning [i] >= 14) {
			GameObject birdClone = Instantiate (bird [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			birdClone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = birdClone; 
		}else if (DataManager.instance.habitData.daysRunning [i] >= 7) {
			GameObject hiyokoClone = Instantiate (hiyoko [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			hiyokoClone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = hiyokoClone; 
		} else if (DataManager.instance.habitData.daysRunning [i] >= 4) {
			GameObject hibiEggClone = Instantiate (hibiEgg [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			hibiEggClone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = hibiEggClone; 
		} else if (DataManager.instance.habitData.daysRunning [i] >= 1) {
			GameObject eggClone = Instantiate (egg [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			eggClone.transform.Rotate (new Vector3 (0, 180, 0));
			character [i] = eggClone;
			Debug.Log ("キャラめい" + character [i].name);
		}
	}




	//【キャラクター】ボタンを押したときの変化（キャラクター関係用）
	public void tapButton(int i){
		//IEnumeratorを始める
		Debug.Log("ボタン反応してる？");
		StartCoroutine (StopForOneSend (i));
		if (DataManager.instance.habitData.daysRunning [i] == 1) {
			GameObject eggClone = Instantiate (egg [i], new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f), Quaternion.identity) as GameObject;
			eggClone.transform.Rotate (new Vector3 (0, 180, 0));
			Debug.Log ("たまごでてる？");
			character [i] = eggClone;
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = EggSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] > 90) {
			//character90ジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 90) {
			//character90をDestroyして、character60をinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (character90 [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = GrowthSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] > 60) {
			//character60ジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 60) {
			//character30をDestroyして、character60をinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (character60 [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = GrowthSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] > 30) {
			//character30ジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 30) {
			//birdをDestroyして、character30をinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (character30 [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = GrowthSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] > 14) {
			//birdジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 14) {
			//hiyokoをDestroyして、birdをinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (bird [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = GrowthSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] > 7) {
			//hiyokoジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 7) {
			//hibiEggをDestroyして、hiyokoをinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (hiyoko [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = GrowthSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 6) {
			//hibiEggジャンプ
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (4, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		}else if(DataManager.instance.habitData.daysRunning [i] == 5){
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 10, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		} else if (DataManager.instance.habitData.daysRunning [i] == 4) {
			//eggCloneをDestroyして、hibiEggをinstantiateする
			Destroy (character [i]);
			character [i] = Instantiate (hibiEgg [i]) as GameObject;
			//場所を戻す
			character [i].transform.position = new Vector3 (-3.76f + (3.2f * i), 17.2f, 58.04f);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = HibiSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		}else if(DataManager.instance.habitData.daysRunning [i] == 3){
			character [i].gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (-4f, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		}else if(DataManager.instance.habitData.daysRunning [i] > 1){
			//eggジャンプ
			character [i].gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, jump, 0);
			titleButton[i].gameObject.GetComponent<AudioSource> ().clip = jumpSound;
			titleButton[i].gameObject.GetComponent<AudioSource> ().Play ();
		}
	}

	IEnumerator StopForOneSend(int index){
		this.gameObject.GetComponent<CharacterControllerManager> ().movable [index] = false;
		yield return new WaitForSeconds (1.5f);
		if (DataManager.instance.habitData.daysRunning [index] >= 7) {
			this.gameObject.GetComponent<CharacterControllerManager> ().movable [index] = true;
		}
	}

}




