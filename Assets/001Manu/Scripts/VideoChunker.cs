using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoChunker : MonoBehaviour
{
    [SerializeField] VideoPlayer m_VideoPlayer;
    [SerializeField] VideoChunk chunkClip;
    [SerializeField] bool m_Loop = true;
    [SerializeField] bool m_autoPlay = true;
    public bool playing;

    [SerializeField] Sprite m_PlaySprite;
    [SerializeField] Sprite m_PauseSprite;
    [SerializeField] Image m_PlayPauseBG;

    [SerializeField] Slider m_InteractiveSlider;


    [SerializeField] Slider m_VolumeSlider;

    [SerializeField] Image m_LoopImage;
    [SerializeField] Color32 m_Loop_Enabled;
    [SerializeField] Color32 m_Loop_Disabled;

    [SerializeField] Image m_SoundImage;
    [SerializeField] Sprite m_SoundOn;
    [SerializeField] Sprite m_SoundOff;

    [SerializeField] Text m_CurrentTime;
    [SerializeField] Text m_TotalTime;
    /*
    [SerializeField] GameObject buttons;
    [SerializeField] RawImage m_RawImage;
    [SerializeField] RenderTexture m_RenderTexture;
    void Start()
    {
        m_InteractiveSlider.value = 0;
        if(!chunkClip)
            print("por favor anada un chunkClip para ser reproducido");
            return;
            if(!chunkClip.mainClip)
            {
                print("el chunkClip no tiene un video maestro, por favor asegurese de que este chunk esta actualizado");
            }
        m_VideoPlayer.clip = chunkClip.mainClip;
        chunkClip.chunkDurationTime = chunkClip.chunkEndTime - chunkClip.chunkStartTime;
        if(m_autoPlay)
            OnClick_PlayPause();
    }
    */
    public void InitializeVideoPlayer(VideoChunk chunkClip)
    {
        this.chunkClip = chunkClip;
        m_InteractiveSlider.value = 0;
        if (!chunkClip || !chunkClip.mainClip)
            return;
        m_VideoPlayer.clip = chunkClip.mainClip;
        chunkClip.chunkDurationTime = chunkClip.chunkEndTime - chunkClip.chunkStartTime;
        if (m_autoPlay)
            OnClick_PlayPause();
    }
    void Update()
    {
        playing = m_VideoPlayer.isPlaying;
        if (playing)
        {
            LimitTheVideoToChunk();
            SetCurrentTimeUI();
            SetSliderValue();
        }
    }
    private void LimitTheVideoToChunk()
    {
        if(m_VideoPlayer.isPlaying && m_VideoPlayer.time > chunkClip.chunkEndTime)
        {
            m_VideoPlayer.Pause();
            m_VideoPlayer.time = chunkClip.chunkStartTime;
            if (!m_Loop)
            {
                print("NotLoop");
                m_VideoPlayer.Stop();
                SetIsPlayingSprite(false);
            }else
            {
                print("Loop");
                m_VideoPlayer.Play();
                SetIsPlayingSprite(true);
            }

        }
    }
    public void UpdateVolume()
    {
        m_VideoPlayer.SetDirectAudioVolume(0, m_VolumeSlider.value);
    }
    public void OnClick_Loop()
    {
        m_Loop = !m_Loop;

        switch (m_Loop)
        {
            case true:
                m_LoopImage.color = m_Loop_Enabled;
                break;
            case false:
                m_LoopImage.color = m_Loop_Disabled;
                break;
        }
    }
    public void OnClick_ToggleMute()
    {
        bool isMute = m_VideoPlayer.GetDirectAudioMute(0);

        isMute = !isMute;   // flip/toggle mute

        switch (isMute)
        {
            case true:
                m_SoundImage.sprite = m_SoundOff;
                break;
            case false:
                m_SoundImage.sprite = m_SoundOn;
                break;
        }
        m_VideoPlayer.SetDirectAudioMute(0, isMute);
    }
    public void OnClick_PlayPause()
    {

        if (m_VideoPlayer.isPlaying)
        {
            m_VideoPlayer.Pause();
            m_VideoPlayer.targetTexture.Release();
            SetIsPlayingSprite(false);
        }
        else
        {
            if(m_VideoPlayer.time < chunkClip.chunkStartTime || m_VideoPlayer.time > chunkClip.chunkEndTime)
            {
                m_VideoPlayer.time = chunkClip.chunkStartTime;
            }
            m_VideoPlayer.Play();

            SetIsPlayingSprite(true);

            SetTotalTimeUI();
        }
    }
    private void SetIsPlayingSprite(bool isActive)
    {
        m_PlayPauseBG.sprite = (isActive) ? m_PauseSprite : m_PlaySprite;
    }
    private void SetCurrentTimeUI()
    {
        string minutes = Mathf.Floor((int)(m_VideoPlayer.time - chunkClip.chunkStartTime) / 60).ToString("00");
        string seconds = ((int)(m_VideoPlayer.time - chunkClip.chunkStartTime) % 60).ToString("00");

        m_CurrentTime.text = minutes + " : " + seconds;
    }
    private void SetTotalTimeUI()
    {
        string minutes = Mathf.Floor((int)chunkClip.chunkDurationTime / 60).ToString("00");
        string seconds = ((int)chunkClip.chunkDurationTime % 60).ToString("00");

        m_TotalTime.text = minutes + " : " + seconds;
    }
    private void SetSliderValue()
    {
        m_InteractiveSlider.maxValue = chunkClip.chunkDurationTime;
    }
    public void ScrubBarHeadMove()
    {
        m_VideoPlayer.time = chunkClip.chunkStartTime + m_InteractiveSlider.value;


        if (!m_VideoPlayer.isPlaying)
        {

            m_VideoPlayer.Play();

            m_PlayPauseBG.sprite = m_PauseSprite;
        }
    }

}
