using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorPuzzle : MonoBehaviour {
	public GameObject northWall;
	public GameObject southWall;
	public GameObject parentOb;
	public GameObject baseObject;
	public int duplicateTo = 5;

	public GameObject dialogPanel;
	public GameObject counterPanel;

	//max # of choices player gets before they lose
	//scale difficulty by increasing/decreasing max
	public int maxChoices = 5;

	private GameObject[] mirrors;
	private GameObject laser;
	private int choices;
	private bool won, lost;

	void Start () {
		GameObject goal;
		Transformer transformComp;
		Vector3 pos, hitPoint = Vector3.zero;
		Button retry;
		int[] matrix;
		int i = 0, rand = Random.Range(0, duplicateTo-1);

		laser = GameObject.Find("Laser");
		mirrors = ObjectDuplicate.Duplicate(parentOb, baseObject, duplicateTo);

		foreach(GameObject ob in mirrors) {
			RandomizePosition(ob.transform); //set starting position

			transformComp = ob.GetComponent<Transformer>();
			pos = ob.transform.position;
			transformComp.SetPosition(ob.transform.position); //set displayed position

			matrix = new int[4]; //start transformation matrix value

			for(int j = 0; j < 4; j++) {
				matrix[j] = Random.Range(-5, 5);
			}
			transformComp.SetTransform(matrix); //set transformation value and text

			//select random transformer as the winner choice
			if(i == rand) {
				goal = GameObject.Find("Wizard");
				hitPoint = transformComp.GetHitPoint(0.4f);
				goal.GetComponent<WizardPosition>().SetPosition(hitPoint);
				goal.transform.LookAt(Vector3.zero); //so wizard isn't facing wrong direction
			}
			i++;
		}

		laser.GetComponentInChildren<LaserScript>().Setup();
		choices = 0;

		//update counter panel choices left
		UpdateCounter(maxChoices.ToString());

		//setup counter panels "retry" button action
		retry = counterPanel.transform.Find("RetryButton").gameObject.GetComponent<Button>();
		retry.onClick.RemoveAllListeners();
		retry.onClick.AddListener(() => {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});
	}

	//late update to allow win/lose race condition to concede to win
	void LateUpdate() {
		if(choices >= maxChoices && !won) { //too many choices
			Lose();
		}
		//else if(GameObject.Find("Player").GetComponent<PlayerHealth>().IsDead()) { //player died
		//	Lose();
		//}
	}

	//randomize transformer position relative to rooms relative center
	private void RandomizePosition(Transform child) {
        Vector3 center = GetRelativeCenter();

		child.position = GetRandomOffset(center, 7);
	}

	//get position of center of room, relative to north/south wall of puzzle room
	private Vector3 GetRelativeCenter() {
		Vector3 northPos, southPos;

		northPos = northWall.GetComponent<Collider>().bounds.center;
		southPos = southWall.GetComponent<Collider>().bounds.center;

		Debug.Log((northPos + southPos)/2);
		return (northPos + southPos)/2;
	}

	//get position randomly offset from relative position
	private Vector3 GetRandomOffset(Vector3 relPos, float seed) {
		Vector3 result = relPos + Random.insideUnitSphere * seed;

		result.y = 0.5F; //set to same plane

		return result;
	}

	//win/lose dialog setup and display
	private void SetDialog(TextAsset t) {
		Button restart = null, goBack = null;

		if(dialogPanel == null) {
			dialogPanel = GameObject.Find("Canvas").transform.Find("DialogBox").gameObject;
		}

		dialogPanel.GetComponentInChildren<Text>(true).text = t.text;
		
		foreach(Component comp in dialogPanel.GetComponentsInChildren<Button>()) {
			if(comp.gameObject.name == "RestartButton") {
				restart = (Button)comp;
			}
			else if(comp.gameObject.name == "GoBackButton") {
				goBack = (Button)comp;
			}
		}

		restart.onClick.RemoveAllListeners();
		restart.onClick.AddListener(() => {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});

		goBack.onClick.RemoveAllListeners();
		goBack.onClick.AddListener(() => {
			//Add mechanism for scene revert to world map or calling scene
			// if(won) {
			// 	//return to scene saying puzzle complete
			// }
			// else {
			// 	//return to scene as puzzle fail
			// }
			SceneManager.LoadScene("NewStart");
		});

		dialogPanel.gameObject.SetActive(true);
	}

	private void UpdateCounter(string newCount) {
		counterPanel.transform.Find("CounterText").gameObject.GetComponent<Text>().text = newCount;
	}

	//track # of times player selected a cube
	public void IncrementChoice() {
		if(choices < maxChoices) {
			choices++;
			UpdateCounter((maxChoices - choices).ToString());
		}
	}

	//player wins
	public void Win() {
		SetDialog(Resources.Load<TextAsset>("Text/winText"));
	}

	//player loses
	public void Lose() {
		SetDialog(Resources.Load<TextAsset>("Text/loseText"));
	}
}
