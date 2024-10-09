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
		DisplayConstants(constants);

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

	/*static float determinantOfMatrix(float[,] mat) {
		float ans;
		ans = mat[0, 0] * (mat[1, 1] * mat[2, 2] - mat[2, 1] * mat[1, 2])
			- mat[0, 1] * (mat[1, 0] * mat[2, 2] - mat[1, 2] * mat[2, 0])
			+ mat[0, 2] * (mat[1, 0] * mat[2, 1] - mat[1, 1] * mat[2, 0]);
		return ans;
	}

	// This function finds the solution of system of
	// linear equations using cramer's rule
	void findSolution(float[,] coeff) {
		// Matrix d using coeff as given in cramer's rule
		float[,] d = {
		{ coeff[0,0], coeff[0,1], coeff[0,2] },
		{ coeff[1,0], coeff[1,1], coeff[1,2] },
		{ coeff[2,0], coeff[2,1], coeff[2,2] },
	};
		//matrixDisplay.GrabMatrix(d, "D");

		// Matrix d1 using coeff as given in cramer's rule
		float[,] d1 = {
		{ coeff[0,3], coeff[0,1], coeff[0,2] },
		{ coeff[1,3], coeff[1,1], coeff[1,2] },
		{ coeff[2,3], coeff[2,1], coeff[2,2] },
	};

		// Matrix d2 using coeff as given in cramer's rule
		float[,] d2 = {
		{ coeff[0,0], coeff[0,3], coeff[0,2] },
		{ coeff[1,0], coeff[1,3], coeff[1,2] },
		{ coeff[2,0], coeff[2,3], coeff[2,2] },
	};

		// Matrix d3 using coeff as given in cramer's rule
		float[,] d3 = {
		{ coeff[0,0], coeff[0,1], coeff[0,3] },
		{ coeff[1,0], coeff[1,1], coeff[1,3] },
		{ coeff[2,0], coeff[2,1], coeff[2,3] },
	};

		// Calculating Determinant of Matrices d, d1, d2, d3
		float D = determinantOfMatrix(d);
		float D1 = determinantOfMatrix(d1);
		float D2 = determinantOfMatrix(d2);
		float D3 = determinantOfMatrix(d3);
		Debug.Log("D is: " + D);
		Debug.Log("D1 is: " + D1);
		Debug.Log("D2 is: " + D2);
		Debug.Log("D3 is: " + D3);

		// Case 1
		if (D != 0) {
			// Coeff have a unique solution. Apply Cramer's Rule
			float x = D1 / D;
			float y = D2 / D;
			float z = D3 / D; // calculating z using cramer's rule
			Debug.Log("Value of x is: " + x);
			Debug.Log("Value of y is: " + y);
			Debug.Log("Value of z is: " + z);
		}

		// Case 2
		else {
			if (D1 == 0 && D2 == 0 && D3 == 0)
				Debug.LogWarning("Infinite solutions!");
			else if (D1 != 0 || D2 != 0 || D3 != 0)
				Debug.LogWarning("No solutions!");
		}
	}*/

	/*// Driver Code
	public static void Main() {
		// storing coefficients of linear
		// equations in coeff matrix
		float[,] coeff = {{ 2, -1, 3, 9 },
						{ 1, 1, 1, 6 },
						{ 1, -1, 1, 2 }};
		findSolution(coeff);
	}*/

	void DisplayConstants(float[] m) {
		Debug.Log("Displaying constants");
		int alphabet = 65;
		for (int i = 0; i < m.Length; i++) {
			Debug.Log((char)alphabet + " = " + m[i]);
			alphabet++;
		}
	}
	void DisplayMatrix(float[,] m) {
		Debug.Log("Displaying matrix");
		for (int i = 0; i < m.GetLength(0); i++) {
			string output = "";
			for (int j = 0; j < m.GetLength(1); j++) {
				if (j == m.GetUpperBound(1)) {
					output += "=" + m[i, j];
				}
				else {
					switch (j) {
						case 0:
							if (m[i, j] == 0) {
								output += "0";
							}
							else if (m[i, j] == 1) {
								output += "+" + "x";
							}
							else if (m[i, j] == -1) {
								output += "x";
							}
							else if (m[i, j] > 0) {
								output += "+" + m[i, j] + "x";
							}
							else if (m[i, j] < 0) {
								output += m[i, j] + "x";
							}
							break;
						case 1:
							if (m[i, j] == 0) {
								output += "0";
							}
							else if (m[i, j] == 1) {
								output += "+" + "y";
							}
							else if (m[i, j] == -1) {
								output += "y";
							}
							else if (m[i, j] > 0) {
								output += "+" + m[i, j] + "y";
							}
							else if (m[i, j] < 0) {
								output += m[i, j] + "y";
							}
							break;
						case 2:
							if (m[i, j] == 0) {
								output += "";
							}
							else if (m[i, j] == 1) {
								output += "+" + "z";
							}
							else if (m[i, j] == -1) {
								output += "z";
							}
							else if (m[i, j] > 0) {
								output += "+" + m[i, j] + "z";
							}
							else if (m[i, j] < 0) {
								output += m[i, j] + "z";
							}
							break;
						default:
							Debug.LogError("Something went wrong when displaying matrix");
							break;
					}
				}
			}
			if (output[0] == '+') {
				output = output.Substring(1);
			}
			//matrix
			Debug.Log(output);
		}
	}
}
