using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "Chunk", menuName = "ScriptableObjects/VideoChunk", order = 1)]
public class VideoChunk : ScriptableObject
{
    public VideoClip mainClip;
    public float chunkStartTime, chunkEndTime, chunkDurationTime;
}
