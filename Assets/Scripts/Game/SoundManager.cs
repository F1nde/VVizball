using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	public AudioSource audioSource;
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.
	public AudioClip[] audioEffects;

	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this;
			//If instance already exists:
		} else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}
		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}
	
	
	//Used to play single sound clips.
	public void PlaySingle(int clip)
	{
		if (clip >= 0 && clip < audioEffects.Length) {
			audioSource.PlayOneShot (audioEffects [clip]);
		}
	}
}

