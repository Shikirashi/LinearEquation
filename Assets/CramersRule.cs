using System;
using UnityEngine;
[System.Serializable]
public class CramersRule : MonoBehaviour {
	// Start is called before the first frame update
	[SerializeField]
	MatrixDisplayer matrixDisplay;
	static double[,] coefficients = new double[1, 1];
	static double[] constants = new double[1];
	void Start() {
		matrixDisplay = GetComponent<MatrixDisplayer>();
		constants = new double[2];
	}

	// Update is called once per frame
	void Update() {

	}

	public void GrabMatrix(float[,] matrix) {
		//DisplayMatrix(matrix);
		Debug.Log("Cramer's Rule");
		DisplayMatrix(matrix);
		Solve(matrix, matrix.GetLength(0));
	}

	public void Solve(float[,] matrix, int n) {
		float[,] coefficients = new float[n, n];
		float[] constants = new float[n];
		int k = 0;
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n + 1; j++) {
				if (j < n) {
					coefficients[i, j] = matrix[i, j];
				}
				else {
					constants[k] = matrix[i, j];
					k++;
				}
			}
		}
		float[] solutions = CalculateSolutions(coefficients, constants);
		DisplayConstants(solutions);
	}

	private float[] CalculateSolutions(float[,] coefficients, float[] constants) {
		int n = constants.Length;
		float det = CalculateDeterminant(coefficients);
		if (det == 0) {
			throw new InvalidOperationException("The determinant is zero, the system has no unique solution.");
		}

		float[] solutions = new float[n];
		for (int i = 0; i < n; i++) {
			float[,] tempMatrix = (float[,])coefficients.Clone();
			for (int j = 0; j < n; j++) {
				tempMatrix[j, i] = constants[j];
			}
			solutions[i] = CalculateDeterminant(tempMatrix) / det;
		}

		return solutions;
	}

	private float CalculateDeterminant(float[,] matrix) {
		int n = matrix.GetLength(0);
		if (n == 1)
			return matrix[0, 0];

		if (n == 2)
			return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

		float det = 0;
		for (int col = 0; col < n; col++) {
			det += (col % 2 == 0 ? 1 : -1) * matrix[0, col] * CalculateDeterminant(GetMinor(matrix, 0, col));
		}

		return det;
	}

	private float[,] GetMinor(float[,] matrix, int row, int col) {
		int n = matrix.GetLength(0);
		float[,] minor = new float[n - 1, n - 1];

		for (int i = 0, minorRow = 0; i < n; i++) {
			for (int j = 0, minorCol = 0; j < n; j++) {
				if (i != row && j != col) {
					minor[minorRow, minorCol] = matrix[i, j];
					minorCol++;
					if (minorCol == n - 1) {
						minorCol = 0;
						minorRow++;
					}
				}
			}
		}
		return minor;
	}

	void DisplayConstants(float[] m) {
		Debug.Log("Displaying constants");
		int alphabet = 65;
		for (int i = 0; i < m.Length; i++) {
			Debug.Log((char)alphabet + " = " + m[i]);
			alphabet++;
		}
	}

	void DisplayMatrix(float[,] m) {
		Debug.Log("Displaying matrices");
		for (int i = 0; i < m.GetLength(0); i++) {
			int alphabet = 65;
			string output = "";
			for (int j = 0; j < m.GetLength(1); j++) {
				char character = (char)alphabet;
				if (j == m.GetUpperBound(1)) {
					output += "=" + m[i, j];
				}
				else if (j != 0) {
					if (m[i, j] == 1 || m[i, j] == -1) {
						output += "+" + character.ToString();
					}
					else if (m[i, j] > 0) {
						output += "+" + m[i, j] + character.ToString();
					}
					else {
						output += m[i, j] + character.ToString();
					}
				}
				else {
					if (m[i, j] == 1 || m[i, j] == -1) {
						output += character.ToString();
					}
					else {
						output += m[i, j] + character.ToString();
					}
				}
				alphabet++;
			}
			Debug.Log(output);
		}
	}

}
