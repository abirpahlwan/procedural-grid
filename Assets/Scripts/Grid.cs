using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

	public int xSize;
	public int ySize;

	private Vector3[] vertices;
	private Mesh mesh;

	void Awake() {
		StartCoroutine(Generate());
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		foreach (var vertex in vertices) {
			Gizmos.DrawSphere(vertex, 0.1f);
		}
	}

	private IEnumerator Generate() {
		WaitForSeconds wait = new WaitForSeconds(0.05f);

		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";
		
		vertices = new Vector3[(xSize + 1) * (ySize + 1)];
		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x, y);
				yield return wait;
			}
		}

		mesh.vertices = vertices;

		int[] triangles = new int[3];
		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = xSize + 1;
		mesh.triangles = triangles;
	}
}
