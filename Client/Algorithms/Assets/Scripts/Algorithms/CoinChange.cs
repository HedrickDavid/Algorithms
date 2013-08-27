using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinChange : Complex, IAlgorithm {

	private List<int> _denominationList;
	private List<int> _coinUsedList;
	private int _amount = 0;

	public override void Show(){

	}

	public override void Hide(){

	}

	public override void Activate(){

	}

	public override void Deactivate(){

	}

	public void SetValueList(List<int> denominationList, int amount){
		_denominationList = denominationList;
		_amount = amount;
	}

	public void Execute(){
		StartCoinChange (_amount, _denominationList.Count);
	}

	private int StartCoinChange (int amount, List<int> len){

		if (amount <= 0){
			return 0;
		}

		int value = 0;
		for (int i = 0; i<len; i++){
			value = Mathf.Min (StartCoinChange(amount - _denominationList[i]) + 1, value);
		}
		return value;
	}
}
