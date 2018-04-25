using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStoneController : MonoBehaviour {

    GameObject player;
    PlayerController playerController;

    Light glowStoneLight;

	private AudioSource source;
	public AudioClip orbLandingSound;
	public AudioClip orbPickupSound1;
	public AudioClip orbPickupSound2;
	public AudioClip orbPickupSound3;
	public AudioClip orbPickupSound4;
	private List<AudioClip> pickupSounds = new List<AudioClip> ();

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

        glowStoneLight = gameObject.GetComponent<Light>();

        glowStoneLight.intensity = 0.5f;
        glowStoneLight.range = 8f;

		source = GetComponent<AudioSource> ();
		pickupSounds.Add (orbPickupSound1);
		pickupSounds.Add (orbPickupSound2);
		pickupSounds.Add (orbPickupSound3);
		pickupSounds.Add (orbPickupSound4);
	}
	
	// Update is called once per frame
	void Update () {
        OutOfBoundsCheck();
    }


    void OnCollisionStay(Collision collision)
    {
		if (collision.transform.name != "Player") {
			glowStoneLight.intensity = 1f;
			glowStoneLight.range = 15f;
		} 
    }

	void OnCollisionEnter(Collision collision) 
	{
		if (collision.transform.name != "Player") {
			source.PlayOneShot (orbLandingSound, .8f);
		} 
		if(collision.transform.name == "Player") {
			source.PlayOneShot (pickupSounds [Random.Range (0, 3)], 1f);
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
