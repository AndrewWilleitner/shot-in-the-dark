using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour {

    private Transform playerTransform;
    private Transform cameraTransform;
    private GameObject directionalLightObject;
    private Light mainLight;

    public Transform cameraZoomOutPoint;
    public string nextSceneName;

	private AudioSource source;
	public AudioClip goalSound;


    // Use this for initialization
    void Start ()
    {
        playerTransform = GameObject.Find("Player").transform;
        cameraTransform = playerTransform.Find("Main Camera");

        directionalLightObject = GameObject.Find("Directional Light");
        mainLight = directionalLightObject.GetComponent<Light>();

        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
        mainLight.intensity = 0;

		source = GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}


    private IEnumerator ZoomOutCamera()
    {
        
        Vector3 startPos = cameraTransform.position;
        Quaternion startRot = cameraTransform.rotation;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 5f;
            cameraTransform.position = Vector3.Lerp(startPos, cameraZoomOutPoint.position, t);
            cameraTransform.rotation = Quaternion.Lerp(startRot, cameraZoomOutPoint.rotation, t);
            RenderSettings.ambientIntensity = t;
            RenderSettings.reflectionIntensity = t;
            mainLight.intensity = t * 0.5f;

            yield return new WaitForEndOfFrame();
        }
        cameraTransform.position = cameraZoomOutPoint.position;
        cameraTransform.rotation = cameraZoomOutPoint.rotation;
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
        mainLight.intensity = 0.5f;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.tag == "Player")
        {
			cameraTransform.parent = null;
            Destroy(playerTransform.gameObject);
            StartCoroutine("ZoomOutCamera");
        }
    }
}
