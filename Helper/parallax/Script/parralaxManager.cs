using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class CameraExtensions
{
	//******Orthographic Camera Only******//

	public static Vector2 BoundsMin(this Camera camera)
	{
		return (Vector2)camera.transform.position - camera.Extents();
	}

	public static Vector2 BoundsMax(this Camera camera)
	{
		return (Vector2)camera.transform.position + camera.Extents();
	}

	public static Vector2 Extents(this Camera camera)
	{
		if (camera.orthographic)
			return new Vector2(camera.orthographicSize * Screen.width / Screen.height, camera.orthographicSize);
		else
		{
			Debug.LogError("Camera is not orthographic!", camera);
			return new Vector2();
		}
	}
	//*****End of Orthographic Only*****//
}

[System.Serializable]
public class CameraThreshold {
	public Vector3 popLimitation;
	public Vector3 depopLimitation;
}

[System.Serializable]
public class BasicGeneratorParameters {
	public AssetEntry typeOfAsset;
	public GameObject prefab = null;
	public Sprite sprite = null;
	public bool authoriseRandomFlip = false;
}

[System.Serializable]
public class RandomGeneratorParameters {
	public randomSpawnAssetConfiguration[] AssetConfiguation;
	public bool authoriseRandomFlip = false;
	public bool removeDirectDuplicata = true;
	public bool removeFlipDuplicata = true;
}


[System.Serializable]
public enum ParallaxPlansType {
	BASIC,
	WITHSAVE
};
[System.Serializable]
public enum GeneratorType {
	BASIC,
	RANDOM
};

[System.Serializable]
public enum AssetEntry {
	PREFAB,
	SPRITE
};

[System.Serializable]
public class ParralaxPlanConfiguration : System.Object
{
	public string nameParalaxPlan;
	[Header("Parralax plan selection")]
	public ParallaxPlansType parallaxType = ParallaxPlansType.WITHSAVE;
	[Header("\"Deep\" of the parralax plan, this will define the factor of speed")]
	[Tooltip("0 for ground, > 0 for foreground and <0 for background")]
	public float distance;
	public float yOffset = 0f;
	public float lowSpaceBetweenAsset;
	public float hightSpaceBetweenAsset;
    public float relativeSpeed;
	//public Color colorTeinte = Color.clear; //override by the cooler.warmer color
	public float m_OrderInLayerOffset = 0f;


	public int seed=0;

	public GeneratorType generatorType;

	public BasicGeneratorParameters basicGeneratorParameters;

	public RandomGeneratorParameters randomGeneratorParameters;


}
[ExecuteInEditMode]
public class parralaxManager : MonoBehaviour {

	[Header("Tab of all parralax plan configutation")]
	[SerializeField]
	private List<ParralaxPlanConfiguration> configurationParralax;

	[Header("Configuration of parralax Manager")]
	[SerializeField]
	[Tooltip("Camera that the parralax will follow.event if the camera don't move, set one")]
	private Camera cameraToFollow = null;
	[SerializeField]
	[Tooltip("independante speed. This speed willaffect all the parralax plan ")]
	private float constantSpeed;

	[SerializeField]
	[Tooltip("Distance between the game plan and the camera, this will affect the parallax effect")]
	private float cameraDistance=3;

	[SerializeField]
	[Tooltip("YSpeed multiplicator on a parallax, by default the y parallax is disable")]
	private float YSpeedMultiplicator = 0;

	[SerializeField]
	[Tooltip("Distance of the last plan, a plan at this distance will not move at all")]
	private float horizonLine=-4000;
	[SerializeField]
	[Tooltip("seed for all plans if not set")]
	private int m_globalSeed = 123456789;

	[SerializeField]
	[Tooltip("OrderInLayer value for a sprite at the horizon")]
	private float m_OrderInLayerAtHorizon = -4000;

	[SerializeField]
	[Tooltip("OrderInLayer value for a sprite at the minimal distance")]
	private float m_OrderInAtFront = 0;

	[SerializeField]
	[Tooltip("Cooldest color at the horizon")]
	private Color m_CooldestColorHorizon = Color.blue;

	[SerializeField]
	[Tooltip("Warmest color at the camera")]
	private Color m_WarmestColorCamera = Color.cyan;

	private float speed;
	public CameraThreshold cameraThreshold = new CameraThreshold();
	private List<GameObject> parralaxPlans;



	public float CameraWidthSize = 0;

	public bool debugMode = false;

	public bool reset = false;

	private bool isPreviousPositionSet = false;
	private Vector3 previousCameraPosition = Vector3.zero;

	private bool m_refreshZoom = false;

	#if UNITY_EDITOR
	private EditorApplication.CallbackFunction s_backgroundUpdateCB;
	private void EditorCallback() {
		if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode) {
			clear ();
		}
		if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode) {
			clear ();
		}
	}

	void OnEnable() {
		s_backgroundUpdateCB = new EditorApplication.CallbackFunction(EditorCallback);
		EditorApplication.update += s_backgroundUpdateCB;
	}
	#endif	
	void Awake(){
		clear ();
		var children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => DestroyImmediate(child));

	}

	// Use this for initialization
	void Start () {
		reset = false;
		speed = constantSpeed;
		cameraThreshold.popLimitation = cameraToFollow.BoundsMax();
		cameraThreshold.depopLimitation = cameraToFollow.BoundsMin();
		parralaxPlans = new List<GameObject> ();
		float maxDistanceOver0 = 0;
		foreach (ParralaxPlanConfiguration config in configurationParralax) {
			GameObject tempParralaxPlan = new GameObject();
			tempParralaxPlan.transform.parent = this.transform;
			tempParralaxPlan.name = config.nameParalaxPlan;

			parallaxPlan tempScript;
			if (config.parallaxType == ParallaxPlansType.BASIC) {
				tempScript = tempParralaxPlan.AddComponent<parallaxPlanBasic> (); 
			} else {
				tempScript = tempParralaxPlan.AddComponent<parallaxPlanSave> ();
			}
			tempScript.cameraThreshold = cameraThreshold;
			tempScript.distance = config.distance;
			if(config.distance > maxDistanceOver0)
            {
				maxDistanceOver0 = config.distance;

			}

			tempScript.lowSpaceBetweenAsset = config.lowSpaceBetweenAsset;
			tempScript.hightSpaceBetweenAsset = config.hightSpaceBetweenAsset;
            tempScript.relativeSpeed = config.relativeSpeed;
			//tempScript.colorTeint = config.colorTeinte;
			tempScript.cameraDistancePlan0 = cameraDistance;
			tempScript.horizonLineDistance = horizonLine;
			tempScript.ySpeedMultiplicator = YSpeedMultiplicator;
			tempScript.yOffset = config.yOffset;
			tempScript.seed = (config.seed != 0) ? config.seed : m_globalSeed + (int)config.distance;

			if (config.generatorType == GeneratorType.BASIC) {
				assetGenerator generatorScript = tempParralaxPlan.AddComponent<assetGenerator> ();
				if (config.basicGeneratorParameters.typeOfAsset == AssetEntry.PREFAB) {
					generatorScript.prefab = config.basicGeneratorParameters.prefab;
				} else {
					generatorScript.spriteForPrefab = config.basicGeneratorParameters.sprite;
				}
				generatorScript.authoriseRandomFlip = config.basicGeneratorParameters.authoriseRandomFlip;
				tempScript.generator = generatorScript;
			} else {
				assetRandomGenerator generatorScript = tempParralaxPlan.AddComponent<assetRandomGenerator> ();
				generatorScript.AssetConfiguation = config.randomGeneratorParameters.AssetConfiguation;
				generatorScript.authoriseRandomFlip = config.randomGeneratorParameters.authoriseRandomFlip;
				generatorScript.removeDirectDuplicata = config.randomGeneratorParameters.removeDirectDuplicata;
				generatorScript.removeFlipDuplicata = config.randomGeneratorParameters.removeFlipDuplicata;
		
				tempScript.generator = generatorScript;
			}
				
			parralaxPlans.Add(tempParralaxPlan);
		}
		parralaxPlans.Sort(delegate(GameObject x, GameObject y)
		{
			parallaxPlan tempScriptX = x.GetComponent<parallaxPlan>();
			parallaxPlan tempScriptY = y.GetComponent<parallaxPlan>();
			if (tempScriptX.distance == tempScriptY.distance) {
				return 0;
			} else if (tempScriptX.distance < tempScriptY.distance) {
				return 1;
			} else return -1;
		});

		float totalDistance = maxDistanceOver0 - horizonLine;
		float totalZindex = m_OrderInAtFront - m_OrderInLayerAtHorizon;
		float factor = totalZindex / totalDistance;

		float m_CooldestColorHorizonH, m_CooldestColorHorizonS, m_CooldestColorHorizonV; 
		Color.RGBToHSV(m_CooldestColorHorizon, out m_CooldestColorHorizonH, out m_CooldestColorHorizonS, out m_CooldestColorHorizonV);
		float m_WarmestColorCameraH, m_WarmestColorCameraS, m_WarmestColorCameraV;
		Color.RGBToHSV(m_WarmestColorCamera, out m_WarmestColorCameraH, out m_WarmestColorCameraS, out m_WarmestColorCameraV);

		float HueFactor = (m_CooldestColorHorizonH - m_WarmestColorCameraH)/totalDistance;
		float SaturationFactor = (m_CooldestColorHorizonS - m_WarmestColorCameraS)/totalDistance;
		float VFactor = (m_CooldestColorHorizonV - m_WarmestColorCameraV)/totalDistance;

		float zinf = 2.0f;
		float zsupp = -2.0f;
		foreach (GameObject temp in parralaxPlans) {
			parallaxPlan tempPlan = temp.GetComponent<parallaxPlan>();
			tempPlan.Zindex = tempPlan.distance * factor;
			tempPlan.colorTeint = Color.HSVToRGB(m_CooldestColorHorizonH + HueFactor* tempPlan.distance, m_CooldestColorHorizonS + SaturationFactor * tempPlan.distance, m_CooldestColorHorizonV + VFactor * tempPlan.distance);
			/*if (temp.GetComponent<parallaxPlan>().distance < 0){
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z + zinf++);
			} else if(temp.GetComponent<parallaxPlan>().distance == 0) {
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z );
			} else {
				temp.transform.localPosition = new Vector3(temp.transform.localPosition.x,temp.transform.localPosition.y,temp.transform.localPosition.z+ zsupp--);
			}*/
		}
		UpdateCameraThreshold ();
	}

	void UpdateCameraThreshold() {
		//reset the Pop and depop position 
		m_refreshZoom = false;
		float cameraOrthographiqueSize = cameraToFollow.orthographicSize;

		float CameraW = 2 * Screen.width / Screen.height;// cameraToFollow.rect.width;
		//Debug.Log("W" + CameraWidthSize + "  size : " + cameraOrthographiqueSize);
		if (CameraWidthSize != cameraOrthographiqueSize*CameraW || CameraWidthSize ==0)
		{
			//zoom
			CameraWidthSize = cameraOrthographiqueSize * CameraW;
			m_refreshZoom = true;
		}
		if (cameraThreshold != null) {
			cameraThreshold.popLimitation = cameraToFollow.BoundsMax() + Vector2.one;// new Vector3(cameraToFollow.transform.position.x + CameraW * cameraOrthographiqueSize,cameraToFollow.transform.position.y , cameraToFollow.transform.position.z);
			cameraThreshold.depopLimitation = cameraToFollow.BoundsMin() - Vector2.one ; //new Vector3 (cameraToFollow.transform.position.x - CameraW * cameraOrthographiqueSize, cameraToFollow.transform.position.y, cameraToFollow.transform.position.z);
		}
	}

	void UpdateSpeedAndPosition(){
		float cameraSpeedX=0;
		float cameraSpeedY = 0;
		if (cameraToFollow != null){
			if (!isPreviousPositionSet) {
				isPreviousPositionSet = true;
				previousCameraPosition = cameraToFollow.transform.position;
			}
			cameraSpeedX = (cameraToFollow.transform.position.x - previousCameraPosition.x);
			cameraSpeedY = (cameraToFollow.transform.position.y - previousCameraPosition.y);
			previousCameraPosition = cameraToFollow.transform.position;
			this.transform.position = new Vector3(cameraToFollow.transform.position.x, this.transform.position.y, this.transform.position.z);
		}
		if (parralaxPlans != null) {
			foreach (GameObject plan in parralaxPlans) {
				plan.GetComponent<parallaxPlan> ().setSpeedOfPlan (speed + cameraSpeedX, cameraSpeedY);
				if (m_refreshZoom) {
					plan.GetComponent<parallaxPlan> ().refreshOnZoom ();
				}
			}
		}
	}


	// Update is called once per frame
	#if UNITY_EDITOR
	void Update () {
	#else
	void FixedUpdate () {
	#endif
		UpdateCameraThreshold ();

		UpdateSpeedAndPosition ();

		if (debugMode) {
			setPlanConstante ();
		}

		if (reset) {
			resetAllPlan ();
		}
	}

	public float getGroundSpeedf() {
		return speed;
	}

	public void isPaused(bool pause) {
		if (pause) {
			speed = 0;
		} else {
			speed = constantSpeed;
		}
	}

	private void setPlanConstante() {
		speed = constantSpeed;

		foreach (ParralaxPlanConfiguration config in configurationParralax) {
			GameObject tempParralaxPlan = parralaxPlans.Find (plan => plan?.name == config.nameParalaxPlan);

			if (tempParralaxPlan)
			{
				parallaxPlan parralaxScript = tempParralaxPlan.GetComponent<parallaxPlan>();
				parralaxScript.yOffset = config.yOffset;
				parralaxScript.distance = config.distance;
				parralaxScript.lowSpaceBetweenAsset = config.lowSpaceBetweenAsset;
				parralaxScript.hightSpaceBetweenAsset = config.hightSpaceBetweenAsset;
				parralaxScript.relativeSpeed = config.relativeSpeed;
				//parralaxScript.colorTeint = config.colorTeinte;
				parralaxScript.cameraDistancePlan0 = cameraDistance;
				parralaxScript.horizonLineDistance = horizonLine;
			}

		}
	}
	private void resetAllPlan(){
		reset = false;
		clear ();
		Start ();
	}

	private void clear(){
		if (parralaxPlans != null) {
			foreach (GameObject plan in parralaxPlans) {
				if (plan != null)
				{
					parallaxPlan current = plan.GetComponent<parallaxPlan>();
					if (current != null)
					{
						current.Clear();
					}
				}
			}
            foreach (GameObject plan in parralaxPlans)
            {
                DestroyImmediate(plan);
            }

            parralaxPlans.Clear ();
			parralaxPlans = null;
		}
	}
}
