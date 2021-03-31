using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{


    public AudioClip menuBackgroundMusic;
    public AudioClip breathingSound;
    public Button startButton;
    public float startingPitch;
    public float decreasePitchBy;
    private AudioSource audioSource;
    private bool detune = false;
    private bool isStartingGame = false;




    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(detuneMusic);
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.pitch = startingPitch;
        playMusic();
    }

    // Update is called once per frame
    void Update()
    {
        //While detune, decreasePitchBy
        if (detune)
        {
            if (audioSource.pitch > 0)
            {
                audioSource.pitch -= Time.deltaTime * decreasePitchBy;
            }
            else
            {
                audioSource.Stop();
                audioSource.pitch = startingPitch;
                StartCoroutine(playBreathSound());
                detune = false;
            }
        }


        if (isStartingGame)
            startGame();
    }


    ///<summary>Load the main scene.</summary>
    private void startGame()
    {
        SceneManager.LoadScene("Debug_Room");
    }


    ///<summary>Play the main menu background music.</summary>
    private void playMusic()
    {
        audioSource.clip = menuBackgroundMusic;
        audioSource.Play();
    }


    ///<summary>Play the breathing sound. Once it's finished, set isStartingGame true.</summary>
    private IEnumerator playBreathSound()
    {
        audioSource.loop = false;
        audioSource.clip = breathingSound;
        audioSource.Play();


        //Wait til audioSource finishes playing.
        yield return new WaitWhile(()=> audioSource.isPlaying);
        //start the next scene
        isStartingGame = true;
    }


    ///<summary>Set detune to true. Detunes music during update calls.</summary>
    private void detuneMusic()
    {
        detune = true;
    }
}
