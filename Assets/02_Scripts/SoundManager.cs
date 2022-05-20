using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	public static SoundManager Instance;
	public AudioSource BgSource,ClickSource;
	public AudioClip MMSound,LevelSound,GPSound,LevelFail,LevelComplete,BtnClick,CpSound,GemSound,ApplauseSound;
    public AudioClip[] SignalSounds;
 
    // Use this for initialization


    void Awake()
	{
		if (Instance==null) {

			Instance = this;

		}
	}



	void Start () {

	

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PlayBG(AudioClip clip,float vol)
	{
		if (PlayerPrefs.GetInt("Music")==1)
		{
			BgSource.Stop();

			BgSource.clip = clip;
			BgSource.volume = vol;
		//	Debug.Log ("playing BG");
			BgSource.Play();
		}
		
		
	}

	public void StopBG()
	{
		BgSource.Stop();

	}

	public void PlayBtnClick()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{

			ClickSource.Stop();
			ClickSource.PlayOneShot(BtnClick);
		}
	}

	public void PlayGem()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{
            ClickSource.Stop();
            ClickSource.PlayOneShot(GemSound);

        }
	}

    public void PlayCp()
	{
		if (PlayerPrefs.GetInt("Sound", 1) == 1)
		{
            ClickSource.Stop();
            ClickSource.PlayOneShot(CpSound);

        }
	}

    public void PlaySignal (int Num)
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            ClickSource.Stop();
            ClickSource.PlayOneShot(SignalSounds[Num]);

        }
    }



    public void PlaySingle(AudioClip clip)
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            ClickSource.Stop();
            ClickSource.PlayOneShot(clip);
        }
    }









}
