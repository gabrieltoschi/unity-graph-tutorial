using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher1 : MonoBehaviour {

	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola
	};

	private delegate float FunctionDelegate(float x);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola
	};

	public FunctionOption function;

	[Range(10, 150)]
	public int resolution = 10;

	private int currentResolution;

	private ParticleSystem.Particle[] points;
	private ParticleSystem partSystem;

	void Start (){
		// getting particle system
		partSystem = gameObject.GetComponent<ParticleSystem>();
	}

	private void createPoints(){
		// setting resolution
		currentResolution = resolution;

		// creating points
		points = new ParticleSystem.Particle[resolution];

		// set points position in X axis
		float increment = 1f / (resolution - 1);

		for (int i = 0; i < resolution; i++) {
			float x = i * increment;

			points[i].position = new Vector3(x, 0f, 0f);
			points[i].startColor = new Color(1f, 0f, 0f, 1f);
			points[i].startSize = 0.1f;
		}
	}

	void Update(){
		// recreate points if resolution changed
		if (currentResolution != resolution || points == null){
			createPoints();
		}

		// get mathematical function
		FunctionDelegate mathFunc = functionDelegates[(int)function];

		// set points position in Y axis
		for (int i = 0; i < resolution; i++){
			Vector3 p = points[i].position;
			p.y = mathFunc(p.x);
			points[i].position = p;
		}

		// update particle system
		partSystem.SetParticles(points, points.Length);
	}

	private static float Linear(float x){
		return x;
	}

	private static float Exponential(float x){
		return x * x;
	}

	private static float Parabola (float x){
		x = 2f * x - 1f;
		return x * x;
	}
}
