using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour
{
    public VideoClip[] clip = new VideoClip[10];
    public VideoPlayer VP;
    // Start is called before the first frame update
    void Start()
    {
        VP = gameObject.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((ulong)VP.frame >= VP.frameCount-2 && (ulong)VP.frame<1000)
        {
            print("1");
            VideoClip temp = VP.clip;
            while(VP.clip == temp)
            {
                VP.clip = clip[Random.Range(0, clip.Length)];
                VP.frame = 1;
            }
            print(VP.clip);
        }
    }
}
