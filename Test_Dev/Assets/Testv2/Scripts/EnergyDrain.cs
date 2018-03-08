using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnergyDrain : MonoBehaviour {
	public Color colorChange;
	public MeshRenderer currentShader;
	float colorEnergy = 1f;
	public float energy;
	public float speed;
	float maxEnergy;
	float support;

	// Use this for initialization
	void OnValidate ()
	{
		currentShader.sharedMaterial.color = colorChange;
	}

	void Start()
	{
		maxEnergy = energy;
	}

	// Update is called once per frame
	void Update () {

		energy -= 1 * Time.deltaTime * speed;
		support = (colorEnergy * energy) / maxEnergy;
		colorChange.r = support;
		colorChange.g = support;
		colorChange.b = 0f;
		currentShader.material.color = colorChange;

	}
}
