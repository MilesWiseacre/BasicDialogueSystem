using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Put on the same object as DWindow so it can be used
public class Cast : MonoBehaviour
{
    // Allows me to build a library of actors and the sounds they make
    [System.Serializable]
    public class Actor
    {
        // Name of the actor
        public string actor;
        // Actor's voice sounds
        public AudioClip[] vox;
        // Actor's font
        public TMP_FontAsset font;
    }
    // Creates a list of Actors
    public List<Actor> cast = new List<Actor>();
    // Sounds to use for another sound setting
    public AudioClip[] rpgSound;
}
