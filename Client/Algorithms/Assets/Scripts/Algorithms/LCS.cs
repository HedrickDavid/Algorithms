using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LCS : Complex, IAlgorithm {

	private List<int> _valueList;
	private List<int> _lcsList;

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

	public void Execute(){
		Debug.Log (_valueList.Count);
		_lcsList = new List<int> ();

		// Initialize first value of list
		//InitializeLCS ();
		Debug.Log (_lcsList[_lcsList.Count-1]);
		//int lcs = ExecuteLCS (_valueList, 0, _valueList.Count - 1, 1);
		int lcs = StartLCS ();
		Debug.Log ("Longest Common Subsequence: " + lcs);
		/*string str = "";
		foreach (int val in _lcsList){
			str += val + ", ";
		}*/

		//Debug.Log (str);
	}

	private int StartLCS(){
		int largest = 0;
		int len = _valueList.Count;
		if (len == 1){
			_lcsList.Add (1);
			largest++;
			return largest;
		} else if (len == 0){
			return largest;
		}

		for (int i=len - 2; i>=0; i--){
			int lcsCount = FindLCS(_valueList , i+1, len, i);
			_lcsList.Add (lcsCount);
			if (largest < lcsCount ){
				largest = lcsCount;
			}
		}
		return largest;
	}

	private int FindLCS(List<int> valueList, int start, int end, int index){

		// return if leaft node
		if (start <= end){
			return 1;
		}

		int mid = (int)Mathf.Floor((start + end) / 2);
		int left = FindLCS (valueList, start, mid, index);
		int right = FindLCS (valueList, mid + 1, end, index);

		if (left > right) {
			return left;
		} else {
			return right;
		}
	}

	private void InitializeLCS(){
		// Initialize memoization of the last 2 elements in the list
		if (_lcsList.Count == 0 && _valueList.Count >= 2) {
			int last = _valueList.Count - 1;
			int secLast = _valueList.Count - 2;
			if (_valueList [last] > _valueList [secLast]) {		
				_lcsList.Insert (0, _valueList [last]);
			} else {
				_lcsList.Insert (0, _valueList [secLast]);
			}
		} else if (_valueList.Count == 1){
			_lcsList.Insert (0, _valueList[0]);
		}
	}

	private int ExecuteLCS(List<int> valueList, int start, int end, int largest){
		int lcsCount = largest;

		// Determine if the element is the leaf node
		if (start == end){
			//Debug.Log ("Leaf: " + start + " - " + end);
			return lcsCount; 
		}

		int mid = (int)Mathf.Floor((start + end) / 2);

		lcsCount = ExecuteLCS (valueList, mid + 1, end, lcsCount);

		// Add to list if the value of the end index of the value list is less than that of the first 
		// index of the longest common subsequence and increment count
		if (valueList [end] < _lcsList [0] && end != _valueList.Count - 1) {		
			_lcsList.Insert (0, _valueList [end]);
			lcsCount++;
		}

		// Left side of the recursion
		lcsCount = ExecuteLCS (valueList, start, mid, lcsCount);

		return lcsCount;
	}
}
