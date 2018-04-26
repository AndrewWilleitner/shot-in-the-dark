using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

	Animator anim;
	private Transform modelTransform;
    private Rigidbody playerRigidbody;
    private Transform cameraTransform;

    public GameObject GlowStonePrefab;

    public int stoneCount;
    public float movementSpeed;
    public float maxThrowPower;
    public Vector3 movement;

    private GameObject mainCanvasObject;
	private Text stoneCountTxt;

    private Transform selectedRetrievalStone;
    private Rigidbody selectedRetrievalStoneRigidbody;

    private Quaternion desiredCameraRotation;
	private Quaternion desiredModelRotation;

	private float throwStoneDelay = .4f;



    // Use this for initialization
    void Start()
    {

		anim = transform.Find ("sitdT-Pose").GetComponent<Animator> ();
		modelTransform = transform.Find ("sitdT-Pose").transform;
        playerRigidbody = transform.GetComponent<Rigidbody>();
        cameraTransform = transform.Find("Main Camera");

        mainCanvasObject = GameObject.Find("Canvas");
        stoneCountTxt = mainCanvasObject.transform.Find("StoneCountText").GetComponent<Text>();

        stoneCountTxt.text = "Stones: " + stoneCount.ToString();
        desiredCameraRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        RotateCameraInput();
        ThrowStoneClick();
        RetrieveStoneClick();
        ResetLevel();
    }

    // Update is called once per physics frame
    void FixedUpdate()
    {
        MovementAndRotation();
        RetrieveStoneMovement();
		stoneCountTxt.text = "Stones: " + stoneCount.ToString();
    }

    private void MovementInput()
    {
        movement = Vector3.zero;
        if (Input.GetKey("w"))
            movement += cameraTransform.forward;
        if (Input.GetKey("s"))
            movement += -cameraTransform.forward;
        if (Input.GetKey("d"))
            movement += cameraTransform.right;
        if (Input.GetKey("a"))
            movement += -cameraTransform.right;
    }


    private void MovementAndRotation()
    {
        movement.y = 0;
        movement = movement.normalized * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
        playerRigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, desiredCameraRotation, 10f * Time.deltaTime));

		if (movement == Vector3.zero) {
			anim.SetBool ("walk", false);
		} else {
			desiredModelRotation = Quaternion.LookRotation (movement);
			anim.SetBool ("walk", true);
		}
		modelTransform.rotation = desiredModelRotation;
    }


	IEnumerator ThrowStone(Vector3 targetPos, float delayTime)
    {
		if (stoneCount <= 0)
			yield break;
		anim.SetTrigger("throw");
		yield return new WaitForSeconds (delayTime);

        GameObject stone = Instantiate(GlowStonePrefab, transform.position + new Vector3(0f, 2.5f, 0f), Quaternion.identity);
        Rigidbody stoneRigidbody = stone.transform.GetComponent<Rigidbody>();


        float gravity = Physics.gravity.magnitude;
        float angle = 30 * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(targetPos.x, 0, targetPos.z);
        Vector3 planarPostion = new Vector3(stone.transform.position.x, 0, stone.transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPostion);

        float yOffset = stone.transform.position.y - targetPos.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        initialVelocity = Mathf.Clamp(initialVelocity, 0, maxThrowPower);

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (targetPos.x > stone.transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        stoneRigidbody.velocity = finalVelocity;


        stoneCount--;
    }


    private void RetrieveStoneMovement()
    {
        if (Input.GetMouseButton(1))
        {
            if (selectedRetrievalStone)
            {
                selectedRetrievalStoneRigidbody.MovePosition(selectedRetrievalStone.position + (transform.position - selectedRetrievalStone.position).normalized * 20 * Time.deltaTime);
            }
        }
    }


    private void RetrieveStoneClick()
    {
        int stoneLayerMask = 1 << LayerMask.NameToLayer("GlowStone");
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, stoneLayerMask))
            {
                if (hit.rigidbody)
                {
                    selectedRetrievalStone = hit.transform;
                    selectedRetrievalStoneRigidbody = selectedRetrievalStone.GetComponent<Rigidbody>();
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            selectedRetrievalStone = null;
            selectedRetrievalStoneRigidbody = null;
        }
    }


    private void ThrowStoneClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
				StartCoroutine(ThrowStone(hit.point, throwStoneDelay));
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "GlowStone")
        {
            stoneCount++;
            collision.transform.gameObject.GetComponent<GlowStoneController>().SelfDestroy();
        }
    }


    private void RotateCameraInput()
    {
        if (Input.GetKeyDown("e"))
        {
            desiredCameraRotation = Quaternion.Euler(desiredCameraRotation.eulerAngles + Vector3.up * 45);
        }
        if (Input.GetKeyDown("q"))
        {
            desiredCameraRotation = Quaternion.Euler(desiredCameraRotation.eulerAngles + Vector3.up * -45);
        }
    }


    private void ResetLevel()
	{
		if (Input.GetKeyDown ("r") || transform.position.y < -50f) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
}