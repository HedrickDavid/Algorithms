using UnityEngine;
using System.Collections;

public class MainApplication : MonoBehaviour {

	[SerializeField]
	private AlgorithmType algorithmType;
	private AlgorithmController algorithmController;

	// Use this for initialization
	void Start () {
		StartApplication ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void StartApplication(){
		algorithmController = new AlgorithmController (algorithmType);
		algorithmController.StartController ();
	}
}
