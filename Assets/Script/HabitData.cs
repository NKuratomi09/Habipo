using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HabitData{
	public string[,] runningLog; 
	public string[] titles = new string[3];
	public string[] contents = new string[3];
	public int[] daysRunning = new int[3];
	public int[] daysSum = new int[3];
	public int[] runningLogId = new int[3];

	public void PutRunningLog(){
		string str = "";
		for (int i = 0; i < runningLog.GetLength (0); i++) {
			for (int j = 0; j < runningLog.GetLength (1); j++) {
				str += runningLog [i, j] + ",";
			}
			str += "\n";
		}
		Debug.Log (str);
	}

	public void AddLog(int buttonIndex){
		//新しい2次元配列お宣言。要素数は今までのrunningLogの縦ながさ＋１
		string[,] newLog = new string[runningLog.GetLength(0)+1,runningLog.GetLength(1)];
		//今までの分をコピー
		for (int i = 0; i < runningLog.GetLength (0); i++) {
			for (int j = 0; j < runningLog.GetLength (1); j++) {
				newLog[i,j] = runningLog [i, j];
			}
		}
		//あたしいデータをいれる
		int index = newLog.GetLength(0)-1;
		newLog [index, 0] = index.ToString();
		newLog [index, 1] = DataManager.instance.habitData.titles [buttonIndex];
		newLog [index, 2] = DateTime.Now.Year.ToString();
		newLog [index, 3] = DateTime.Now.Month.ToString();
		newLog [index, 4] = DateTime.Now.Day.ToString();
		newLog [index, 5] = "1";
		runningLog = newLog;
		csvManager.WriteData("RunningLog.csv", runningLog);
		runningLogId[buttonIndex] = index;
	}

	public void UpdateLog(int buttonIndex){
		Debug.Log(runningLogId [buttonIndex]);
		runningLog [runningLogId [buttonIndex], 5] = DataManager.instance.habitData.daysRunning[buttonIndex].ToString();
		csvManager.WriteData("RunningLog.csv", runningLog);
	}

}