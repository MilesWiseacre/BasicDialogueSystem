using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DWindow : MonoBehaviour
{
    #region references
    // References to the text on the UI
    [SerializeField]
    TMP_Text actorText, linesText;
    // Reference to the cast
    Cast cast;
    // Reference to the current scene
    [HideInInspector]
    public Scene currScene;
    // Reference to the current dialogue
    Scene.Dialogue currDialogue;
    // Reference to the current actor
    Cast.Actor currActor;
    // Reference to the Animator
    Animator anim;
    // Reference to the current string of text
    string dialogue;
    // Reference to the Button on the Panel
    [SerializeField]
    GameObject continueButton;
    // References to the audio system, with an int for the randomizer
    AudioSource speaker;
    AudioClip[] sounds;
    int lastSound = -1;
    // Keeps track of what line of dialogue is being said in the scene
    int index, sentIndex;
    // Stores default font so it can be used anytime
    [SerializeField]
    TMP_FontAsset defaultFont;
    // Stores typing speed so it can be returned after dialogue is skipped
    float prefSpeed;
    #endregion
    // Sets up some of the references so they can be used
    void Start()
    {
        anim = GetComponent<Animator>();
        speaker = GetComponent<AudioSource>();
        cast = GetComponent<Cast>();
    }
    // Activates the continue button if the dialogue is done being typed, and skips ahead if a button is pressed
    void Update()
    {
        if (linesText.text == dialogue)
        {
            continueButton.SetActive(true);
        }
        if (linesText.text != dialogue && Input.anyKey)
        {
            prefSpeed = 0;
        }
    }
    // I like to have my public functions to have an underscore so when I use a function
    // in the inspector, they are all grouped up for me.
    // This function opens up the text window.
    public void _OpenWindow()
    {
        anim.SetBool("isOpen", true);
        sentIndex = currScene.scene.Length;
        _NextLine();
    }
    // Checks to see if the dialogue is going to continue or end
    public void _NextLine()
    {
        if (index < sentIndex)
        {
            // Resets typing speed after dialogue has been skipped
            prefSpeed = DOptionsRetrieval.GetTypeSpeed();
            NextSentence(currScene.scene[index].speaker.ToString(), currScene.scene[index].lines);
        } else
        {
            ResetText();
        }
    }
    // Performs all the tasks necessary to start bringing the text to the screen
    private void NextSentence(string speaker, string incoming)
    {
        // Prevents the continue button from being used, otherwise the player
        // can just mash it and mess up the dialogue printed on screen
        continueButton.SetActive(false);
        // Copies the dialogue for the current line
        currDialogue = currScene.scene[index];
        // Moves the conversation forward. This is done after the index is used
        // in the previous line so that the dialogue in place 0 is used
        index++;
        // Queues up the next sentence
        dialogue = incoming;
        // Allows for line breaks and speakers with full names
        dialogue = dialogue.Replace('@', '\n');
        speaker = speaker.Replace('_', ' ');
        // Puts the speaker on screen and clears the text field
        if (speaker == "Null")
        {
            actorText.text = "";
        } else
        {
            actorText.text = speaker;
        }
        linesText.text = "";
        // Loads up the sound effects for the current speaker
        FindActor();
        // Begins adding text to the field
        StartCoroutine(Type());
    }
    // Blanks the window and sends it away
    private void ResetText()
    {
        actorText.text = "";
        linesText.text = "";
        anim.SetBool("isOpen", false);
        index = 0;
    }
    // Finds which sound effects to load into the array depending on options
    private void FindActor()
    {
        if (currDialogue.speaker.ToString() == "Null")
        {
            LoadFont(defaultFont);
            LoadSound(cast.rpgSound);
        }
        else
        {
            foreach (Cast.Actor act in cast.cast)
            {
                if (act.actor == currDialogue.speaker.ToString())
                {
                    currActor = act;
                    if (DOptionsRetrieval.GetSFX() == "sfxDefault")
                    {
                        LoadSound(currActor.vox);
                    }
                    LoadFont(currActor.font);
                }
            }
        }
        if (DOptionsRetrieval.GetSFX() == "sfxRPG")
        {
            LoadSound(cast.rpgSound);
        } else if (DOptionsRetrieval.GetSFX() == "sfxSilent")
        {
            sounds = null;
        }
        
    }
    // Loads the font into the dialogu emanager
    private void LoadFont(TMP_FontAsset incFont)
    {
        if (incFont != null)
        {
            linesText.font = incFont;
        } else
        {
            linesText.font = defaultFont;
        }
    }
    // Loads the sound effects into the dialogue manager
    private void LoadSound(AudioClip[] clips)
    {
        sounds = clips;
    }
    // Provides the typing effect
    IEnumerator Type()
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            linesText.text += letter;
            if (sounds != null)
            {
                TypeSound();
            }
            yield return new WaitForSeconds(prefSpeed);
        }
    }
    // Provides the sound when typing, provided nothing is playing at the time
    private void TypeSound()
    {
        if (!speaker.isPlaying)
        {
            // Randomizes which sound should be played
            if (sounds.Length > 1)
            {
                int sound = Random.Range(0, sounds.Length);
                if (sound == lastSound)
                {
                    sound = Random.Range(0, sounds.Length);
                }
                speaker.PlayOneShot(sounds[sound]);
                lastSound = sound;
            } else if (sounds.Length == 1)
            {
                speaker.PlayOneShot(sounds[0]);
            }
        }
    }
}
