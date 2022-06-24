using UnityEngine;
using Vuforia;
public class CallVideoPlayer : DefaultObserverEventHandler
{
    [SerializeField] VideoChunk chunk;
    VideoChunker chunker;
    RectTransform chunkerRect;
    [SerializeField] bool bigMarker;
    public void SuStartVideo()
    {
        chunker = FindObjectOfType<VideoChunker>(true);
        chunker.transform.SetParent(transform);
        chunkerRect = chunker.GetComponent<RectTransform>();
        chunkerRect.localRotation = Quaternion.Euler(90, 0, 0);
        chunkerRect.anchoredPosition = new Vector2(0, 0.1f);
        chunkerRect.anchorMax = Vector2.zero;
        chunkerRect.anchorMin = Vector2.zero;
        chunkerRect.sizeDelta = new Vector2(1920f, 1228f);
        if (!bigMarker)
        {
            chunkerRect.localScale = new Vector3(0.002f, 0.002f, 0.002f);
            chunkerRect.localPosition = new Vector3(0, 0.1f, 0.5f);
        }
        else
        {
            chunkerRect.localScale = new Vector3(0.004f, 0.004f, 0.004f);
            chunkerRect.localPosition = new Vector3(0, 0.1f, 1f);
        }
        chunker.gameObject.SetActive(true);
        chunker.GetComponent<Canvas>().enabled = true;
        chunker.InitializeVideoPlayer(chunk);
    }
    public void SutAndVanishVideoPlayer()
    {
        if (chunker.playing)
            chunker.OnClick_PlayPause();
        chunker.transform.SetParent(null);
        chunker.gameObject.SetActive(false);
        chunker = null;
    }
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        SuStartVideo();
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        SutAndVanishVideoPlayer();
    }
}
