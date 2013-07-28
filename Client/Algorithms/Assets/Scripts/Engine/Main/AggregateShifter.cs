using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AggregateShifter : MonoSingleton<AggregateShifter> {

	private GameObject _panel = null;

	private List<Complex> _complexList = new List<Complex>();
	private List<Complex> _complexStack = new List<Complex>();
	private int _currComplex = -1;

	public static void SetParent(GameObject panel){
		Instance._panel = panel;
	}

	public static void Register<T>(Complex complex){
		Instance.RegisterComplex<T> (complex);
	}

	public static T Shift<T>() where T : Complex{
		return Instance.ShiftComplex<T>();
	}

	public static void Push<T>(){
		Instance.PushComplex<T> ();
	}

	public static void Pop<T>(){
		Instance.PopComplex<T> ();
	}
	// TODO: Change the list of registered complex to string references
	public void RegisterComplex<T>(Complex complex){
		if (!ComplexExistsFromList<T>()) {
			Debug.Log ("Registered complex of type: " + complex.GetType());
			_complexList.Add (complex);
		} else {
			Debug.LogWarning ("Complex already added in the list");
		}
	}

	public T ShiftComplex<T>() where T : Complex{
		if(ComplexExistsFromList<T>()){
			if (_panel == null){
				_panel = new GameObject ();
			}
			Complex complex = GetComplex<Complex> ();
			_panel.AddComponent<T>();
			if (_currComplex != -1){
				_complexList [_currComplex].Deactivate ();
				_complexList [_currComplex].Hide ();
				Destroy(_complexList [_currComplex]);
			}
			complex.Show ();
			complex.Activate ();
		} else {
			Debug.Log("Cannot find complex in list");
		}
		return default(T);
	}

	public void PushComplex<T>(){
		if (ComplexExistsFromList<T>()){
			if (!ComplexExistsFromStack<T>()){
				// TODO: Implement Push
			}
		}
	}

	public void PopComplex<T>(){
		if (ComplexExistsFromStack<T>()) {
			// TODO: Implement Pop
		}
	}

	public T GetComplex<T>() where T : Complex{
		int len = _complexList.Count;
		for (int i=0; i<len; i++){
			if (_complexList[i].GetType() == typeof(T)){
				return _complexList [i] as T;
			}
		}
		return default(T);
	}

	public bool ComplexExistsFromList<T>(){
		return  _complexList.Select (i => i.GetType()).Contains(typeof(T));
	}

	public bool ComplexExistsFromStack<T>(){
		return  _complexStack.Select (i => i.GetType()).Contains(typeof(T));
	}
}
