using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LIS : Complex, IAlgorithm {

	private List<int> _valueList;
	private List<int> _lisList;
	private int _lisCount = 0;

	public override void Show(){

	}

	public override void Hide(){

	}

	public override void Activate(){

	}

	public override void Deactivate(){

	}

	public void SetValueList(List<int> valueList){
		_valueList = valueList;
	}

	private int BinarySearch (List<int> lcsList, int s, int e, int value){
		Debug.Log ("Start: " + s);
		Debug.Log ("End: " + e);
		int mid = Mathf.CeilToInt(s + e / 2);

		// if leaf
		if (s == e){
			return s;
		}

		if (lcsList [mid] > value) {
			if (lcsList [mid-1] < value){
				return mid;
			}
			return BinarySearch (lcsList, s, mid -1, value);
		} else {
			return BinarySearch (lcsList, mid + 1, e, value);
		}
		return -1;
	}

	public void Execute(){
		StartAlgorithm ();
	}

	private void StartAlgorithm(){
		int lisCount = LongestIncreasingSubsequence ();
		Debug.Log ("Longest Subsequence Count: " + lisCount);

		foreach (int value in _lisList){
			print (value + ", ");
		}
	}

	private int LongestIncreasingSubsequence(){
		_lisList = new List<int> ();
		// if list is empty 
		if (_valueList.Count == 0){
			return 0;
		} 

		_lisList.Add (_valueList[0]);
		int len = _valueList.Count;
		for (int i = 1; i<len; i++){
			Debug.Log ("LIS Count: " + _lisList.Count);
			Debug.Log ("Current value" + _valueList [i]);
			foreach (int lisVal in _lisList){
				Debug.Log (lisVal + ", ");
			}
			int value = _valueList [i];

			if (value > _lisList [_lisList.Count - 1]) {
				_lisList.Add (i);
			} else {
				int index = BinarySearch (_lisList, 0, _lisList.Count-1, i);
				// should not have a result of -1
				Debug.Log (index);
				if (index <= -1){
					_lisList [index] = value;
				}
			}
		}
		return _lisList.Count;
	}
}
