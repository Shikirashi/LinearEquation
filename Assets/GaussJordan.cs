using UnityEngine;

public class GaussJordan : MonoBehaviour {
	// Start is called before the first frame update
	float[,] matrix;
	void Start() {

	}

	// Update is called once per frame
	void Update() {

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

	public void GrabMatrix(float[,] matrix) {
		this.matrix = matrix;
		Debug.Log("Gauss Jordan");
		DisplayMatrix(this.matrix);
		Solve(this.matrix);
	}
	void NormalizeRow(float[,] matrix, int row) {
		float divisor = matrix[row, row];
		for (int j = 0; j < matrix.GetLength(1); j++) {
			matrix[row, j] /= divisor;
		}
	}
	void SubtractRow(float[,] matrix, int targetRow, int sourceRow) {
		float factor = matrix[targetRow, sourceRow];
		for (int j = 0; j < matrix.GetLength(1); j++) {
			matrix[targetRow, j] -= factor * matrix[sourceRow, j];
		}
	}
	void Solve(float[,] matrix) {
		int n = matrix.GetLength(0);

		for (int i = 0; i < n; i++) {
			// Make the diagonal contain all 1s
			NormalizeRow(matrix, i);

			// Make the other rows contain 0s
			for (int j = 0; j < n; j++) {
				if (j != i) {
					SubtractRow(matrix, j, i);
				}
			}
		}
		Debug.Log("Matrix size 0 is: " + matrix.GetLength(0));
		Debug.Log("Matrix size 1 is: " + matrix.GetLength(1));
		// Display the result
		int alphabet = 65;
		Debug.Log("Displaying answer");
		for (int i = 0; i < n; i++) {
			Debug.Log((char)alphabet + " = " + matrix[i, n]);
			alphabet++;
		}
	}

	void DisplayConstants(float[] m) {
		Debug.Log("Displaying constants");
		int alphabet = 65;
		for (int i = 0; i < m.Length; i++) {
			Debug.Log((char)alphabet + " = " + m[i]);
			alphabet++;
		}
	}
}
