using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MainApplication : MonoBehaviour {

	[SerializeField]
	private AlgorithmType algorithmType;

	public AlgorithmType AlgorithmType {
		get {
			return algorithmType;
		}
		set {
			algorithmType = value;

		}
	}

	[SerializeField]
	private GameObject parentContainer;

	private AlgorithmController algorithmController;

	// Use this for initialization
	void Start () {
		RegisterComplex ();
		ShowAlgorithm ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void ShowAlgorithm(){
		//Debug.Log(Type.GetType(algorithmType.ToString()));
		//MinMax min = AggregateShifter.Shift<MinMax>();
		//min.Execute ();
		Debug.Log ("Algorithm Displayed");
		if (parentContainer != null){
			//MinMax minmax = parentContainer.AddComponent<MinMax>();
			//minmax.Execute ();

			LIS lcs = parentContainer.AddComponent<LIS> ();
			//lcs.SetValueList(new List<int>(){11, 17, 5, 8, 6, 4, 7, 12, 3});
			lcs.SetValueList(new List<int>(){20, 4, 5, 44, 3, 10, 1, 12, 23});
			lcs.Execute ();
		}
	}

	private void RegisterComplex(){
		/*AggregateShifter.SetParent(parentContainer);
		AggregateShifter.Register<MinMax>(new MinMax());
		AggregateShifter.Register<HeapSort>(new HeapSort());
		AggregateShifter.Register<MergeSort>(new MergeSort());
		AggregateShifter.Register<HeapSort>(new HeapSort());
		AggregateShifter.Register<CountingSort>(new CountingSort());
		AggregateShifter.Register<BucketSort>(new BucketSort());
		AggregateShifter.Register<RadixSort>(new RadixSort());
		AggregateShifter.Register<BitonicSorting>(new BitonicSorting());
		AggregateShifter.Register<StoogeSort>(new StoogeSort());*/
	}
}
