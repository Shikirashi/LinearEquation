using System;
using UnityEngine;

public class LinearEquationCalculator : MonoBehaviour {
	float x, y, z;
	float x1, x2, x3, y1, y2, y3, z1, z2, z3;
	float[,] originalMatrix = new float[3, 3];
	float[,] matrix = new float[3, 3];
	float[,] answerMatrix = new float[3, 1];
	/*void Start() {
		originalMatrix[0, 0] = 1; originalMatrix[0, 1] = 3; originalMatrix[0, 2] = -2;
		originalMatrix[1, 0] = 3; originalMatrix[1, 1] = 5; originalMatrix[1, 2] = 6;
		originalMatrix[2, 0] = 2; originalMatrix[2, 1] = 4; originalMatrix[2, 2] = 3;

		matrix = originalMatrix;

		answerMatrix = new float[3, 1] { { 5 }, { 7 }, { 8 } };
		DisplayMatrix();

		Eliminate();
	}*/

	float FPB(float x, float y) {
		if (y == 0) {
			return x;
		}
		else {
			return FPB(y, x % y);
		}
	}

	float KPK(float x, float y) {
		float fpb = FPB(x, y);
		float kpk = (x * y) / fpb;
		return kpk;
	}

	void Eliminater() {
		//1 & 2

		for (int i = 0; i < matrix.GetLength(0) - 1; i++) {
			float temporary = KPK(matrix[i, 0], matrix[i + 1, 0]);
			for (int j = 0; j < matrix.GetLength(0) - 1; i++) {
				if (matrix[j, 0] != temporary) {
					float temp1 = temporary / matrix[i, 0];
					for (int k = 0; k < matrix.GetLength(1); k++) {
						matrix[j, k] *= temp1;
					}
				}
			}
		}
		Debug.LogWarning("After KPK");
		//DisplayMatrix();

		float temp = KPK(matrix[0, 0], matrix[1, 0]);
		for (int i = 0; i < matrix.GetLength(0) - 1; i++) {
			if (matrix[i, 0] != temp) {
				float temp1 = temp / matrix[i, 0];
				for (int j = 0; j < matrix.GetLength(1); j++) {
					matrix[i, j] *= temp1;
				}
			}
		}
		Debug.Log("After KPK:");
		//DisplayMatrix();

		matrix = new float[3, 3];
		matrix = originalMatrix;
		//2&3
		temp = KPK(matrix[1, 0], matrix[2, 0]);
		Debug.Log(temp);
		for (int i = 1; i < matrix.GetLength(0); i++) {
			if (matrix[i, 0] != temp) {
				float temp1 = temp / matrix[i, 0];
				for (int j = 0; j < matrix.GetLength(1); j++) {
					matrix[i, j] *= temp1;
				}
			}
		}
		Debug.Log("After KPK:");
		//DisplayMatrix();
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

	void DisplayAnswerMatrix() {
		for (int i = 0; i < answerMatrix.GetLength(0); i++) {
			string output = "";
			for (int j = 0; j < answerMatrix.GetLength(1); j++) {
				output += answerMatrix[i, j] + " ";
			}
			Debug.Log(output);
		}
	}

	public void GetMatrix(float[,] m) {
		originalMatrix = m;
		matrix = originalMatrix.Clone() as float[,];
		DisplayMatrix(matrix);
		Eliminate(matrix);
		/*if (Solve(matrix)) {
			Debug.Log("Solved");
		}*/
		//DisplayMatrix(matrix);
	}

	void Eliminate(float[,] M) {
		int rowCount = M.GetUpperBound(0) + 1;
		int columnCount = M.GetUpperBound(1) + 1;
		int back = M.GetUpperBound(1) - 1;
		float[,] container = new float[rowCount - 1, columnCount - 1];
		if (rowCount == 1) {
			Debug.Log("Only 1 row left");
			DisplayMatrix(container);
			return;
		}
		//Debug.Log(rowCount + " " + columnCount);
		float[,] temp = M.Clone() as float[,];
		/*if (M == null || M.Length != rowCount * (rowCount + 1))
			throw new ArgumentException("The algorithm must be provided with a (n x n+1) matrix.");
		if (rowCount < 1)
			throw new ArgumentException("The matrix must at least have one row.");*/
		for (int i = 0; i < M.GetUpperBound(0); i++) {
			float kpk = KPK(temp[i, back], temp[i + 1, back]);
			//Debug.Log(kpk);
			float multiplier = 0;
			/*if (temp[i, back] == 0) {
				back++;
			}*/
			if (temp[i, back] != kpk) {
				//Debug.Log(M[i, 0] + " not equal to " + kpk);
				multiplier = kpk / temp[i, back];
				for (int j = 0; j < columnCount; j++) {
					temp[i, j] *= multiplier;
				}
			}
			if (temp[i + 1, back] != kpk) {
				//Debug.Log(M[i + 1, 0] + " not equal to " + kpk);
				multiplier = kpk / temp[i + 1, back];
				for (int j = 0; j < columnCount; j++) {
					temp[i + 1, j] *= multiplier;
				}
			}
			DisplayMatrix(temp);
			for (int k = 0; k < columnCount - 1; k++) {
				bool foundZero = false;
				string output = "";
				output += "Doing ";
				if (!foundZero) {
					if (temp[i, k] - temp[i + 1, k] != 0) {
						container[i, k] = temp[i, k] - temp[i + 1, k];
						output += temp[i, k] + "-" + temp[i + 1, k] + "=" + container[i, k];
					}
					else {
						foundZero = true;
						output += temp[i, k] + "-" + temp[i + 1, k] + "=" + (temp[i, k] - temp[i + 1, k]);
					}
				}
				if (foundZero) {
					if (temp[i, k] - temp[i + 1, k] != 0) {
						container[i, k - 1] = temp[i, k] - temp[i + 1, k];
						output += temp[i, k] + "-" + temp[i + 1, k] + "=" + container[i, k - 1];
					}
				}
				Debug.Log(output);
			}
			/*for (int j = i; j < rowCount - 1; j++) {
				//Debug.Log(j);
			}*/
			temp = M.Clone() as float[,];
			//container = container.Where(s => !string.IsNullOrWhiteSpace(s).ToArray());
		}
		Debug.Log("Container row" + container.GetLength(0));
		Debug.Log("Container col" + container.GetLength(1));
		DisplayMatrix(container);
		Eliminate(container);
	}

	/*void CleanArray(float[,] array) {

		for (int i = 0; i < array.GetLength(0); i++) {
			for (int j = array.GetUpperBound(1); j > 0; j--) {
				if (array[i, j] != null && array[i - 1, j] == null) {

					array[i - 1, j] = array[i, j];
					array[i, j] = 0;

				}
			}
		}
	}*/


	//gauss-jordan
	static bool Solve(float[,] M) {
		// input checks
		int rowCount = M.GetUpperBound(0) + 1;
		if (M == null || M.Length != rowCount * (rowCount + 1))
			throw new ArgumentException("The algorithm must be provided with a (n x n+1) matrix.");
		if (rowCount < 1)
			throw new ArgumentException("The matrix must at least have one row.");

		// pivoting
		for (int col = 0; col + 1 < rowCount; col++) {
			// check for zero coefficients
			if (M[col, col] == 0) {
				Debug.Log("Found zero");
				// find non-zero coefficient
				int swapRow = col + 1;
				for (; swapRow < rowCount; swapRow++) {
					if (M[swapRow, col] != 0) break;
				}

				if (M[swapRow, col] != 0) {
					// found a non-zero coefficient?
					// yes, then swap it with the above
					float[] tmp = new float[rowCount + 1];
					for (int i = 0; i < rowCount + 1; i++) {
						tmp[i] = M[swapRow, i];
						M[swapRow, i] = M[col, i];
						M[col, i] = tmp[i];
					}
				}
				else return false; // no, then the matrix has no unique solution
			}
		}
		// elimination
		for (int sourceRow = 0; sourceRow + 1 < rowCount; sourceRow++) {
			for (int destRow = sourceRow + 1; destRow < rowCount; destRow++) {
				float df = M[sourceRow, sourceRow];
				float sf = M[destRow, sourceRow];
				for (int i = 0; i < rowCount + 1; i++)
					M[destRow, i] = M[destRow, i] * df - M[sourceRow, i] * sf;
			}
		}

		// back-insertion
		for (int row = rowCount - 1; row >= 0; row--) {
			float f = M[row, row];
			if (f == 0) return false;

			for (int i = 0; i < rowCount + 1; i++) M[row, i] /= f;
			for (int destRow = 0; destRow < row; destRow++) { M[destRow, rowCount] -= M[destRow, row] * M[row, rowCount]; M[destRow, row] = 0; }
		}
		return true;
	}
}
