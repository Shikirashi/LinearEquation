using System.IO;
using UnityEngine;

public class Parser : MonoBehaviour {
	// Start is called before the first frame update
	LinearEquationCalculator LEC;
	CramersRule cramers;
	GaussJordan gauss;
	void Start() {
		LEC = GetComponent<LinearEquationCalculator>();
		cramers = GetComponent<CramersRule>();
		gauss = GetComponent<GaussJordan>();
		ParseFile();
	}

	// Update is called once per frame
	void Update() {

	}
	void ParseFile() {
		string text = File.ReadAllText("equation.txt");
		Debug.Log(text);
		char[] separator = { ';' };
		char[] separator2 = { ' ' };
		string[] strValues = text.Split(separator, System.StringSplitOptions.RemoveEmptyEntries);
		int row = strValues.Length;
		int col = strValues[0].Split(separator2, System.StringSplitOptions.RemoveEmptyEntries).Length;
		/*Debug.Log("row is: " + row);
		Debug.Log("col is: " + col);*/
		float[,] multiArray = new float[row, col];

		for (int i = 0; i < row; i++) {
			//Debug.Log("current value: " + strValues[i]);
			var values = strValues[i].Split(separator2, System.StringSplitOptions.RemoveEmptyEntries);
			//Debug.Log("value length: " + values.Length);
			for (int j = 0; j < col && j < values.Length; j++) {
				float val = 0;
				if (float.TryParse(values[j], out val)) {
					multiArray[i, j] = val;
					//Debug.Log(multiArray[i, j]);
				}
				else {
					Debug.Log("error at row " + i + " col " + col);
				}
			}
		}
		//LEC.GetMatrix(multiArray);
		//cramers.GrabMatrix(multiArray);
		gauss.GrabMatrix(multiArray);
	}
}
