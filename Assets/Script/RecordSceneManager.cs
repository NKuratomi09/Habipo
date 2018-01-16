using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class RecordSceneManager : MonoBehaviour
{

	[SerializeField] GameObject listItem;

	[SerializeField] GameObject group90;
	[SerializeField] GameObject group60;
	[SerializeField] GameObject group30;
	[SerializeField] GameObject group14;
	[SerializeField] GameObject group07;

	[SerializeField] GameObject recordStartScreen;




	public Image tabBarRecordIcon;

	public void SettingButton ()
	{
		SceneManager.LoadScene ("Setting");
		Debug.Log("SettingButton");
	}

	public void MainButton ()
	{
		SceneManager.LoadScene ("Main");
		Debug.Log("MainButton");
	}

	void Start ()
	{
		
			

		Debug.Log (DataManager.instance.habitData.runningLog.GetLength (0));
		if (DataManager.instance.habitData.runningLog.GetLength (0) > 1) {
			ShowRecord ();
		}

		/*
		for (int i = 1; i < DataManager.instance.habitData.runningLog.GetLength (0); i++) {

			GameObject listItemClone = Instantiate (listItem) as GameObject;

			if (int.Parse (DataManager.instance.habitData.runningLog [i, 5]) >= 90) {
				listItemClone.transform.SetParent (group90.transform);
			} else if (int.Parse (DataManager.instance.habitData.runningLog [i, 5]) >= 60) {
				listItemClone.transform.SetParent (group60.transform);
			} else if (int.Parse (DataManager.instance.habitData.runningLog [i, 5]) >= 30) {
				listItemClone.transform.SetParent (group30.transform);
			} else if (int.Parse (DataManager.instance.habitData.runningLog [i, 5]) >= 14) {
				listItemClone.transform.SetParent (group14.transform);
			} else if (int.Parse (DataManager.instance.habitData.runningLog [i, 5]) >= 7) {
				listItemClone.transform.SetParent (group07.transform);
			}

			//GameObject listItemClone = Instantiate (listItem) as GameObject;
			//listItemClone.transform.SetParent (group07.transform);

			listItemClone.transform.FindChild ("LeftText").GetComponent<Text> ().text = 
				DataManager.instance.habitData.runningLog [i, 3] + "/" + DataManager.instance.habitData.runningLog [i, 4] + "~" /+記録途切れた日（runningDaysを足す）;
			listItemClone.transform.FindChild ("CenterText").GetComponent<Text> ().text = DataManager.instance.habitData.runningLog [i, 1];
			listItemClone.transform.FindChild ("RightText").GetComponent<Text> ().text = DataManager.instance.habitData.runningLog [i, 5] + "日継続！";

		}
		*/

	}


	void ShowRecord ()
	{
		List <string[]> newList = new List<string[]> ();
		for (int i = 1; i < DataManager.instance.habitData.runningLog.GetLength (0); i++) {
			string[] strArray = new string[DataManager.instance.habitData.runningLog.GetLength (1)];
			for (int j = 0; j < DataManager.instance.habitData.runningLog.GetLength (1); j++) {
				strArray [j] = DataManager.instance.habitData.runningLog [i, j];
			}
			newList.Add (strArray);
		}
		newList.Sort ((a, b) => {
			Debug.Log (a [5]);
			return int.Parse (b [5]) - int.Parse (a [5]);
		});

		for (int i = 0; i < newList.Count; i++) {
			if (int.Parse (newList [i] [5]) >= 7) {
				Transform targetTransform;
				if (int.Parse (newList [i] [5]) >= 90) {
					targetTransform = group90.transform;
					group90.gameObject.SetActive (true);
				} else if (int.Parse (newList [i] [5]) >= 60) {
					targetTransform = group60.transform;
					group60.gameObject.SetActive (true);
				} else if (int.Parse (newList [i] [5]) >= 30) {
					targetTransform = group30.transform;
					group30.gameObject.SetActive (true);
				} else if (int.Parse (newList [i] [5]) >= 14) {
					targetTransform = group14.transform;
					group14.gameObject.SetActive (true);
				} else {
					targetTransform = group07.transform;
					group07.gameObject.SetActive (true);
				}
				GameObject listItemClone = Instantiate (listItem, targetTransform) as GameObject;
				int startedYear = int.Parse (newList [i] [2]);
				int startedMonth = int.Parse (newList [i] [3]);
				int startedDay = int.Parse (newList [i] [4]);		

				DateTime startedDate = new DateTime (startedYear, startedMonth, startedDay, 0, 0, 0);
				TimeSpan countDays = new TimeSpan (int.Parse (newList [i] [5]), 0, 0, 0);
				DateTime finishedDate = startedDate + countDays;

				//Debug.Log (finishedDate);

				listItemClone.transform.Find ("LeftText").GetComponent<Text> ().text = 
					newList [i] [3] + "/" + newList [i] [4] + " ~ " + finishedDate.Month + "/" + finishedDate.Day;
				listItemClone.transform.Find ("CenterText").GetComponent<Text> ().text = newList [i] [1];
				listItemClone.transform.Find ("RightText").GetComponent<Text> ().text = newList [i] [5] + "日";

				recordStartScreen.gameObject.SetActive (false);
			}
		}
	}
	
}
	
	
