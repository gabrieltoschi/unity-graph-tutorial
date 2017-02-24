using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher1 : MonoBehaviour {

	public int resolution = 10;
	private int resolutionLowerLimit = 10;
	private int resolutionUpperLimit = 100;

	private ParticleSystem.Particle[] points;
	private ParticleSystem partSystem;

	void Start (){
		// getting particle system
		partSystem = gameObject.GetComponent<ParticleSystem>();

		// testing resolution limits
		if (resolution < resolutionLowerLimit || resolution > resolutionUpperLimit){
			Debug.LogWarning ("Grapher resolution out of bounds, resetting to minimum.", this);
			resolution = resolutionLowerLimit;
		}

		// creating points
		points = new ParticleSystem.Particle[resolution];

		// set points' position in X axis
		float increment = 1f / (resolution - 1);

		for (int i = 0; i < resolution; i++) {
			float x = i * increment;

			points[i].position = new Vector3(x, 0f, 0f);
			points[i].startColor = new Vector3(x, 0f, 0f);
			points[i].startSize = 0.1f;
		}
	}

	void Update(){
		partSystem.SetParticles(points, points.Length);
	}
}
