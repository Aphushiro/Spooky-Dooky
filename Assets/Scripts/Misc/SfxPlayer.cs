using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public GameObject sfxObject;

    public AudioClip[] clips;
    AudioSource source;

    public bool pitch;

    // Start is called before the first frame update
    public void PlaySfx()
    {
        GameObject sfx = Instantiate(sfxObject, transform.position, Quaternion.identity);

        source = sfx.GetComponent<AudioSource>();
        
        int num = Random.Range(0, clips.Length);
        source.clip = clips[num];

        if (pitch)
        {
            float vary = Random.Range(-0.1f, 0.1f);
            source.pitch += vary;
        }
        source.Play();
        Destroy(sfx, 1f);
    }
}
