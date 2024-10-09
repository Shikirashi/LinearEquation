using System;
using UnityEngine;

public class GaussJordan : MonoBehaviour {
	// Start is called before the first frame update
	float[,] matrix;
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
	void DisplayMatrix(float[,] matrix) {
		for (int i = 0; i < matrix.GetLength(0); i++) {
			for (int j = 0; j < matrix.GetLength(1); j++) {
				Debug.Log(matrix[i, j]);
			}
		}
	}

	public void GrabMatrix(float[,] matrix) {
		this.matrix = matrix;
		if (Solve(this.matrix)) {
			Debug.Log("solved");
		}
		else {
			Debug.Log("no solution");
		}
		DisplayMatrix(this.matrix);
	}
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
