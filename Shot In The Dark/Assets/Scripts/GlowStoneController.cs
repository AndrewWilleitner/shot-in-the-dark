using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStoneController : MonoBehaviour {

    GameObject player;
    PlayerController playerController;

    Light glowStoneLight;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        glowStoneLight = gameObject.GetComponent<Light>();

        glowStoneLight.intensity = 0.5f;
        glowStoneLight.range = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        OutOfBoundsCheck();
    }


    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name != "Player")
        {
            glowStoneLight.intensity = 1f;
            glowStoneLight.range = 15f;
        }
    }


    private void OutOfBoundsCheck()
    {
        if (transform.position.y < -100f)
        {
            playerController.stoneCount++;
            SelfDestroy();
        }
    }

    // The OnTriggerExit doesn't get called in other scripts if a game object is just destroyed.
    // To counter this I made a custom destroy method for the stones.
    // This method will move the stone far away, wait, than destroy itself.
    public void SelfDestroy()
    {
        StartCoroutine("DestroyFade");
    }

    private IEnumerator DestroyFade()
    {
        transform.position = new Vector3(0f, 1000000f, 0f);
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
