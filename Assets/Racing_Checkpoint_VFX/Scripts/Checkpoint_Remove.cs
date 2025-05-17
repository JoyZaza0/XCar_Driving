using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Remove : MonoBehaviour
{

	public ParticleSystem arch;
	public ParticleSystem archGlow;
	public ParticleSystem archSparksA;
	public ParticleSystem archSparksB;
	public ParticleSystem shrinkingArches;
	public ParticleSystem shrinkingSparks;
	public ParticleSystem archLight;

	public GameObject VFXStop;
	public GameObject portalEndVFX;


	private void Start()
	{
		portalEndVFX.SetActive(false);
	}

	//private	void Update()
	//{
	//	if (Input.GetButtonDown("Fire1")) //check to see if the left mouse was pushed.
	//	{
		
	//		RemoveCheckpoint(); // comment out this line to stop checkpoints triggering on left mouse click
	//	}

	//}

	//void OnTriggerEnter(Collider other)
	//{

	//	RemoveCheckpoint();

	//}



	public void RemoveCheckpoint()
	{

		arch.Stop();
		archGlow.Stop();
		archSparksA.Stop();
		archSparksB.Stop();
		shrinkingArches.Stop();
		shrinkingArches.Stop();
		shrinkingSparks.Stop();
		archLight.Stop();

		VFXStop.SetActive(false);
		portalEndVFX.SetActive(true);

	}


}
