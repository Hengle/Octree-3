﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;


public class MainInstance : MonoBehaviour {

	//____public
	public GameObject ParticlePrefab;
	public GameObject ParticleSpawner;
	public Camera GLCamera;

	float Center = 0.0f;
	float MaxDimension = 17.0f;
	Vector3 CubeCenter;
	Vector3 PartitionerCenter;
	Vector3 CubeWidth;


	public Octree RootOcTree;

	//___private
	private List<GameObject> _Particles;
	private bool _Paint = false;

	enum CursorMode
	{
		NORMAL,
		DRAGGING
	}

	CursorMode _CursorMode = CursorMode.NORMAL;

	KeyboardCameraControl _KeyboardCameraControl;

	Ray myRay;      // initializing the ray
	RaycastHit hit; // initializing the raycasthit


	// Use this for initialization
	void Start () {
		_Particles = new List<GameObject>();
		GridOverlay gridOverlay = GLCamera.GetComponent<GridOverlay>();
		_KeyboardCameraControl = GLCamera.GetComponent<KeyboardCameraControl>();
		CubeCenter = gridOverlay.CubeCenter;
		CubeWidth = gridOverlay.CubeWidth;
		RootOcTree = new Octree(CubeCenter, CubeWidth, ParticlePrefab.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x);

	}
	
	// Update is called once per frame
	void Update () {
		if (_KeyboardCameraControl.LockCursor)
		{ 
			if (ParticleSpawner.activeSelf)
			SpawnParticleOnClick();
		}
	}



	void SpawnParticleOnClick()
	{
		//if (Input.GetKey(KeyCode.Mouse0))
		switch (_CursorMode)
		{

			case CursorMode.NORMAL:
				{
						if ((_Paint) ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0))
						{
							Debug.Log("SPAWNed IT!!!!");
							//instantiate and add in the list
							Vector3 positionToSpawn = ParticleSpawner.transform.position;
							GameObject ParticleObject;
							ParticleObject = Instantiate(ParticlePrefab, new Vector3(positionToSpawn.x, positionToSpawn.y, positionToSpawn.z), Quaternion.identity) as GameObject;
							Debug.Log("ParticleObject.transform.position: " + ParticleObject.transform.position);
							_Particles.Add(ParticleObject);

							RootOcTree.Insert(ParticleObject);
						
						}
						if (Input.GetMouseButtonUp(1))
						{
							//TODO: fix right click at appropriate time
							//ClearQuadtree();
							//_QuadTree.ParticleUnderCursor(hit.point);
							// 
							Debug.Log("CHANGEDS!!!!");
							_CursorMode = CursorMode.DRAGGING;
						}
					break;
				}

			case CursorMode.DRAGGING:
				{
					

					if (Input.GetMouseButtonUp(1))
					{
						// 
						_CursorMode = CursorMode.NORMAL;

					}

					break;
				}

		}//switch end
	}


}