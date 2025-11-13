using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName="NewNPCDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject
{
   
        public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 1f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;

}

    // Update is called once per frame

