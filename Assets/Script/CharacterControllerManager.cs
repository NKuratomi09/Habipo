using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControllerManager : MonoBehaviour
{

	public int characterStatus;
	public int characterId;
	float timer;
	public bool[] movable = new bool[3];

	Vector3[] targetPosition = new Vector3[3];

	MainSceneManager mainSceneManager;

	/*他のスクリプトのobjectを取得する方法
	MainSceneManager mainSceneManager;
	mainSceneManager = this.gameObject.GetComponent<MainSceneManager> ();
	mainSceneManager.character[i].*/


	//はじめの目的地
	//characterId 0:左 1:真ん中 2:右
	void Start ()
	{
		float[] x = new float[3];
		x [0] = Random.Range (-4.5f, -2.3f);
		x [1] = Random.Range (-1.3f, 0.3f);
		x [2] = Random.Range (1.2f, 3.3f);
		float[] z = new float[3];
		z [0] = Random.Range (54.0f, 75.8f);
		z [1] = Random.Range (54.0f, 75.8f);
		z [2] = Random.Range (54.0f, 75.8f);
		float y = 17.5f;

		for (int i = 0; i < 3; i++) {
			targetPosition [i] = new Vector3 (x [i], y, z [i]);
			Debug.Log (targetPosition [i]);
			if (DataManager.instance.habitData.daysRunning [i] < 7) {
				movable [i] = false;
			} else if (DataManager.instance.habitData.daysRunning [i] >= 7) {
				movable [i] = true;
			}
		}
	}



	//character目的地へ移動
	void Update ()
	{
		mainSceneManager = this.gameObject.GetComponent<MainSceneManager> ();
			
		//characterStatus 0:タマゴ　1:キャラ
		for (int i = 0; i < 3; i++) {
			if (movable [i] == true) {
				Vector3 currentPos = mainSceneManager.character [i].transform.position;
				Vector3 direction = new Vector3 (targetPosition [i].x - currentPos.x, targetPosition [i].y - currentPos.y, targetPosition [i].z - currentPos.z);
				Vector3 forward = Vector3.Normalize (direction);
				mainSceneManager.character [i].transform.position += forward * 4f * Time.deltaTime;

				if (Vector3.Distance (currentPos, targetPosition [i]) < 0.3f) {
					SetNewTarget (i);
				}
			}
		}


	}

	void SetNewTarget (int index)
	{
		float[] x = new float[3];
		x [0] = Random.Range (-4.5f, -2.3f);
		x [1] = Random.Range (-1.3f, 0.3f);
		x [2] = Random.Range (1.2f, 3.3f);
		float[] z = new float[3];
		z [0] = Random.Range (54.0f, 75.8f);
		z [1] = Random.Range (54.0f, 75.8f);
		z [2] = Random.Range (54.0f, 75.8f);
		float y = 17.5f;

		targetPosition [index] = new Vector3 (x [index], y, z [index]);
	}
}

