using UnityEngine;
using System.Collections;

public class CameraWork : MonoBehaviour {
	private float t; 

	private float[][] data; 
	public BarChart _BarChart = null; 
	public LineChart _LineChart = null; 


	// Use this for initialization
	void Start () {
		int max = 5; 
		t = 0; 
		data = new float[3][]; 
		for(int i=0;i<data.Length;i++) {
			data[i] = new float[max]; 
			for(int j=0;j<max;j++) {
				data[i][j] = Mathf.Sin(i+(float)j / (float)max * 2.0f * Mathf.PI); 
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		t += 1.0f; 
//		this.transform.position = new Vector3(2.0f*Mathf.Sin(t * 0.016f * 0.02f), 0, 0); 

		for(int i=0;i<data.Length;i++) {
			int max = data[i].Length; 
			for(int j=0;j<max;j++) {
				data[i][j] = Mathf.Sin(t * 0.01f + i+(float)j / (float)max * 2.0f * Mathf.PI); 
			}
		}

		if(_BarChart != null) {
			_BarChart.UpdateData(data); 
		}
		if(_LineChart != null) {
			_LineChart.UpdateData(data); 
		}


		for(int i=0;i<data.Length;i++) {
			int max = data[i].Length; 
			for(int j=0;j<max;j++) {
				data[i][j] = Mathf.Abs(Mathf.Sin(t * 0.01f + i+(float)j / (float)max * 2.0f * Mathf.PI)); // value in Pie/Doughnut chart must be positive value.  
			}
		}

	}
}
