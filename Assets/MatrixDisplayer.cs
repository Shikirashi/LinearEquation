using TMPro;
using UnityEngine;
[System.Serializable]
public class MatrixDisplayer : MonoBehaviour {
	float[,] matrix;
	string matrixLabel;
	TextMeshProUGUI matrixLabelText;
	[SerializeField]
	GameObject cellContainer;
	[SerializeField]
	GameObject cellPrefab;
	void Start() {
	}

	void Update() {

	}

	public void GrabMatrix(float[,] matrix, string label) {
		this.matrix = matrix;
		this.matrixLabel = label;
		Purge();
		Display(this.matrix);
	}

	void Display(float[,] matrix) {
		Debug.Log("Matrix length is: " + matrix.GetLength(0) + "x" + matrix.GetLength(1));
		for (int i = 0; i < matrix.GetLength(0); i++) {
			for (int j = 0; j < matrix.GetLength(1); j++) {
				GameObject g = Instantiate(cellPrefab, cellContainer.transform);
				g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = matrix[i, j].ToString();
			}
		}
	}

	void Purge() {
		Debug.Log("Child count: " + cellContainer.transform.childCount);
		for (int i = cellContainer.transform.childCount; i > 0; i--) {
			Destroy(cellContainer.transform.GetChild(i - 1).gameObject);
		}
	}
}
