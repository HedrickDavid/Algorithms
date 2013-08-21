using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Author: Hedrick David
/// Email: hedrick_david@ymail.com / hedrick_david@dlsu.ph
/// Description: This class will solve for the longest increasing subsequence given 
/// an array of int. It will give not only the length of LIS (O(n log n) because of the binary search) and 
/// the sequence by backtracking through the dp (O(n) because the length will always be that of the length of
/// the original value array). This algorithm uses only 2 arrays for storage of values for the main algorithm,
/// unlike the Patience sort which uses more space based on the number of piles.
/// </summary>
public class LIS : Complex, IAlgorithm {

	private List<int> _valueList; 
	private List<int> _lisSeqList; 
	private int[] _lPileArray; 
	private int[] _dpArray; 

	private int _lisCtr = 0; 

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

	/// <summary>
	/// This is a binary search recursive algorithm to find the lowest value in the array where the value is 
	/// less than the current index in the array but greater than the previous value in the array;
	/// </summary>
	/// <param name="arr"> The array containing the values to be searched </param>
	/// <param name="s"> Start index to traverse </param>
	/// <param name="e"> End index to traverse </param>
	/// <param name="value"> The value being compared </param>
	private int LISBS (int[] arr, int s, int e, int value){
		// if leaf
		if (s == e){
			return s;
		}

		int mid = Mathf.CeilToInt((s + e) / 2);

		if (arr [mid] > value) {
			if (arr [mid-1] < value){
				return mid;
			}
			Debug.Log ("Mid Value greater than key");
			Debug.Log ("Mid: " + mid);
			Debug.Log ("Start: " + s);
			Debug.Log ("End: " + e);
			return LISBS (arr, s, mid -1, value);
		} else {
			Debug.Log ("Mid Value less than or equal to key");
			Debug.Log ("Mid: " + mid);
			Debug.Log ("Start: " + s);
			Debug.Log ("End: " + e);
			return LISBS (arr, mid + 1, e, value);
		}
		return -1;
	}

	public void Execute(){
		StartAlgorithm ();
	}

	private void StartAlgorithm(){
		_lisCtr = LongestIncreasingSubsequence ();

		// Backtrack on the dpList to get the sequence
		_lisSeqList = GetLISSequence (_lisCtr, _valueList, _dpArray, _lPileArray);

		Debug.Log ("Longest Increasing Subsequence Count: " + _lisCtr);

		string str = "LIS Sequence: ";
		foreach(int val in _lisSeqList){
			str += val + ", ";
		}

		string strPile = "LIS Sequence Pile Value: ";
		foreach(int val in _lPileArray){
			strPile += val + ", ";
		}

		string strDP = "LIS Sequence DP: ";
		foreach(int val in _dpArray){
			strDP += val + ", ";
		}
		Debug.Log (str);
		Debug.Log (strPile);
		Debug.Log (strDP);
	}

	/// <summary>
	/// This function gets the LIS Sequence by backtraking through the dpArray.
	/// It looks for the value which is less than the previous pointer which is similar in
	/// the last phases of a patience sort.
	/// </summary>
	/// <returns>The LIS sequence.</returns>
	/// <param name="lisCount"> The number of piles </param>
	/// <param name="valueList"> The original values being compared </param>
	/// <param name="dpArray"> The array the contains the index of the values that where stored in the piles </param>
	/// <param name="pileArray"> The array that stores the last value for each pile represented by each element in the array</param>
	private List<int> GetLISSequence(int lisCount, List<int> valueList, int[] dpArray, int[] pileArray){
		List<int> lisSeqList = new List<int> ();
		int len = dpArray.Length;
		int pointer = lisCount;
		for (int i=len-1; i>=0; i--){
			if (pointer <= 0) {
				break;
			}
			Debug.Log ("Index: " + i);
			Debug.Log ("Pointer Value: " + pointer);
			Debug.Log ("Dynamic Value: " + dpArray[i]);
			if (dpArray[i] == pointer || lisCount == pointer){
				Debug.Log ("Inserted Value: " + valueList[i]);
				lisSeqList.Insert (0, valueList[i]);
				pointer--;
			}
		}
		return lisSeqList;
	}

	/// <summary>
	/// The main algorithm which solves for the longest increasing subsequence.
	/// If the value in the valueList is less than the first element of the pileArray then it replaces it.
	/// If the value is greater than the current lis length then it adds it to the array.
	/// If the value is less than the current length of lis value in the pileArray, then it finds 
	/// the element in the pileArray where the value is the least one.
	/// </summary>
	/// <returns>The length of the longest increasing subsequence.</returns>
	private int LongestIncreasingSubsequence(){

		// if list is empty 
		if (_valueList.Count == 0){
			return 0;
		} 
		int len = _valueList.Count;
		Debug.Log (len);
		// initialize arrays
		_lPileArray = new int[len+1]; // The array which will contain the pile values
		_dpArray = new int[len]; // 

		int lc = 1; // longest count or size

		_lPileArray[0] = 0; // empty value
		_lPileArray [1] = _valueList [0]; // get the first value in the value list 
		_dpArray[0] = lc; 

		for (int i = 1; i<len; i++){
			int value = _valueList [i];
			Debug.Log ("Value: " + value);
			Debug.Log ("Max Pile Value " + _lPileArray[lc]);
			if(value < _lPileArray[1]){
				Debug.Log ("Value " + value + " replaces the first");
				_lPileArray [1] = value;
				_dpArray [i] = 1;
			} else if (value > _lPileArray[lc]) {
				lc++;
				_lPileArray[lc] = _valueList[i];
				_dpArray[i] = lc;
			} else {
				// Basically finds the lowest index where the value is less than and 
				//greater than the previous index of that
				Debug.Log ("Pile length: " + _lPileArray.Length);
				int index = LISBS (_lPileArray, 1, lc, i);
				// should not have a result of -1
				Debug.Log (index);
				if (index != -1){
					_lPileArray [index] = value;
					_dpArray [i] = index;
				}
			}
		}
		return lc;
	}
}
