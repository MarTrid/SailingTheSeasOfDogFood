using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Squeak : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (clips.Length == 0)
        {
            return;
        }
        
        if (audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}
