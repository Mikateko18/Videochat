using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using agora_gaming_rtc;
using agora_utilities;

public class PlayerViewControllerBase : IVideoChatClient
{
    public event Action OnViewControllerFinish;
    protected IRtcEngine mRtcEngine;
    protected Dictionary<uint, VideoSurface> UserVideoDict = new Dictionary<uint, VideoSurface>();
    protected const string SelfVideoName = "MyView";
    protected string mChannel;
    private VideoSurface remoteView;
    private Image remoteVideoImage;
    //    string logFilepath =
    //#if UNITY_EDITOR
    //    Application.dataPath + "/testagora.log";
    //#else
    //    Application.persistentDataPath + "/tesagora.log";
    //#endif
    protected bool remoteUserJoined = false;
    protected bool _enforcing360p = false; // the local view of the remote user resolution
    private bool isVideoPaused = false;

     private bool isScreenSharing = false;

    public PlayerViewControllerBase()
    {
        // Constructor, nothing to do for base
    }

    /// <summary>
    ///   Join a RTC channel
    /// </summary>
    /// <param name="channel"></param>
    public void Join(string channel)
    {
        Debug.Log("calling join (channel = " + channel + ")");

        if (mRtcEngine == null)
            return;

        mChannel = channel;

        // set callbacks (optional)
        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccess;
        mRtcEngine.OnUserJoined = OnUserJoined;
        mRtcEngine.OnUserOffline = OnUserOffline;
        mRtcEngine.OnVideoSizeChanged = OnVideoSizeChanged;
        // Calling virtual setup function
        PrepareToJoin();

        // join channel
        mRtcEngine.JoinChannel(channel, null, 0);

        Debug.Log("initializeEngine done");
    }

    /// <summary>
    ///    Preparing video/audio/channel related characteric set up
    /// </summary>
    protected virtual void PrepareToJoin()
    {
        // enable video
        mRtcEngine.EnableVideo();
        // allow camera output callback
        mRtcEngine.EnableVideoObserver();
    }

    /// <summary>
    ///   Leave a RTC channel
    /// </summary>
    public virtual void Leave()
    {
        Debug.Log("calling leave");

        if (mRtcEngine == null)
            return;

        // leave channel
        mRtcEngine.LeaveChannel();
        // deregister video frame observers in native-c code
        mRtcEngine.DisableVideoObserver();
    }

    protected bool MicMuted { get; set; }

    protected virtual void SetupUI()

    {
    
        GameObject go = GameObject.Find(SelfVideoName);
        if (go != null)
        {
            UserVideoDict[0] = go.AddComponent<VideoSurface>();
            go.AddComponent<UIElementDragger>();
        }

         remoteVideoImage = GameObject.Find("RemoteVideoImage").GetComponent<Image>();
        remoteVideoImage.enabled = false; // Initially, disable the image
        
         Button pauseButton = GameObject.Find("FreezeButton").GetComponent<Button>();
    if (pauseButton != null)
    {
        pauseButton.onClick.AddListener(() =>
{
    ToggleVideoPause();
});
    }

     Button shareScreenButton = GameObject.Find("ShareButton").GetComponent<Button>();
        if (shareScreenButton != null)
        {
            shareScreenButton.onClick.AddListener(ToggleScreenSharing);
        }

        
        Button switchCameraButton = GameObject.Find("Canvas/ButtonPanel/SwitchCameraButton").GetComponent<Button>();
    if (switchCameraButton != null)
    {
        // Check if both front and back cameras are available
        bool isFrontCameraAvailable = IsCameraAvailable(true);  // Front camera
        bool isBackCameraAvailable = IsCameraAvailable(false);  // Back camera

        // Enable or disable the button based on camera availability
        switchCameraButton.interactable = isFrontCameraAvailable && isBackCameraAvailable;

        // Set the button's gameObject to inactive if no back camera is available
        switchCameraButton.gameObject.SetActive(isBackCameraAvailable);

        // Add a listener to your SwitchCamera button
        switchCameraButton.onClick.AddListener(SwitchCamera);
    }
        Button button = GameObject.Find("LeaveButton").GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnLeaveButtonClicked);
        }

        Button mutton = GameObject.Find("MuteButton").GetComponent<Button>();
        if (mutton != null)
        {
            mutton.onClick.AddListener(() =>
            {
                MicMuted = !MicMuted;
                mRtcEngine.EnableLocalAudio(!MicMuted);
                mRtcEngine.MuteLocalAudioStream(MicMuted);
                Text text = mutton.GetComponentInChildren<Text>();
                text.text = MicMuted ? "Unmute" : "Mute";
            });
        }

        go = GameObject.Find("Toggle360p");
        if (go != null)
        {
            Toggle toggle = go.GetComponent<Toggle>();
            _enforcing360p = toggle.isOn; // initial value
            toggle.onValueChanged.AddListener((val) =>
            {
                _enforcing360p = val;
            });
        }
    }

    protected void OnLeaveButtonClicked()
    {
        Leave();
        UnloadEngine();

        if (OnViewControllerFinish != null)
        {
            OnViewControllerFinish();
        }
    }

    protected virtual void OnVideoSizeChanged(uint uid, int width, int height, int rotation)
{
    Debug.LogWarningFormat("OnVideoSizeChanged width = {0} height = {1} for rotation:{2}", width, height, rotation);
    if (UserVideoDict.ContainsKey(uid))
    {
        GameObject go = UserVideoDict[uid].gameObject;
        RawImage image = go.GetComponent<RawImage>();
        
        // Set a fixed size for the RawImage component
        // Adjust the width and height according to your desired size
        Vector2 fixedSize = new Vector2(200, 150); // Example size: 200x150
        
        image.rectTransform.sizeDelta = fixedSize;
    }
}


    bool IsPortraitOrientation(int rotation)
    {
        return rotation == 90 || rotation == 270;
    }

    /// <summary>
    ///   Load the Agora RTC engine with given AppID
    /// </summary>
    /// <param name="appId">Get the APP ID from Agora account</param>
    public void LoadEngine(string appId)
    {
        // init engine
        mRtcEngine = IRtcEngine.GetEngine(appId);

        mRtcEngine.OnError = (code, msg) =>
        {
            Debug.LogErrorFormat("RTC Error:{0}, msg:{1}", code, IRtcEngine.GetErrorDescription(code));
        };

        mRtcEngine.OnWarning = (code, msg) =>
        {
            Debug.LogWarningFormat("RTC Warning:{0}, msg:{1}", code, IRtcEngine.GetErrorDescription(code));
        };

        // mRtcEngine.SetLogFile(logFilepath);
        // enable log
        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
    }

    // unload agora engine
    public virtual void UnloadEngine()
    {
        Debug.Log("calling unloadEngine");

        // delete
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy();  // Place this call in ApplicationQuit
            mRtcEngine = null;
        }
    }

    /// <summary>
    ///   Enable/Disable video
    /// </summary>
    /// <param name="pauseVideo"></param>
    public void EnableVideo(bool pauseVideo)
    {
        if (mRtcEngine != null)
        {
            if (!pauseVideo)
            {
                mRtcEngine.EnableVideo();
            }
            else
            {
                mRtcEngine.DisableVideo();
            }
        }
    }

    public virtual void OnSceneLoaded()
    {
        SetupUI();
    }

    // implement engine callbacks
    protected virtual void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
    }

    // When a remote user joined, this delegate will be called. Typically
    // create a GameObject to render video on it
     protected virtual void OnUserJoined(uint uid, int elapsed)
{
    Debug.Log("User joined: " + uid);

    if (remoteView == null)
    {
        GameObject remoteViewObject = GameObject.Find("RemoteView");
        if (remoteViewObject != null)
        {
            Debug.Log("Adding VideoSurface to RemoteView GameObject...");
            remoteView = remoteViewObject.AddComponent<VideoSurface>();
        }
        else
        {
            Debug.LogError("RemoteView GameObject not found.");
        }
    }

    remoteView?.SetForUser(uid);
    remoteView?.SetEnable(true);
    remoteView?.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
    remoteView?.SetGameFps(30);
    Debug.Log("RemoteView setup for user: " + uid);
}

    // When remote user is offline, this delegate will be called. Typically
    // delete the GameObject for this user
    protected virtual void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        // remove video stream
        Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
        if (UserVideoDict.ContainsKey(uid))
        {
            var surface = UserVideoDict[uid];
            surface.SetEnable(false);
            UserVideoDict.Remove(uid);
            GameObject.Destroy(surface.gameObject);
        }
    }

  private bool IsCameraAvailable(bool isFrontCamera)
{
    WebCamDevice[] devices = WebCamTexture.devices;
    foreach (WebCamDevice device in devices)
    {
        if (device.isFrontFacing == isFrontCamera)
        {
            // Camera of the specified position is available
            return true;
        }
    }
    // Camera of the specified position is not available
    return false;
}


    // Add this function to the PlayerViewControllerBase class
protected void SwitchCamera()
{
    if (mRtcEngine != null)
    {
        mRtcEngine.SwitchCamera(); // Toggle between front and back camera
    }
}

public void ToggleVideoPause()
{
    isVideoPaused = !isVideoPaused;

    if (mRtcEngine != null)
    {
        Debug.Log("ToggleVideoPause - isVideoPaused: " + isVideoPaused);

        // Toggle video pause for all remote users
        mRtcEngine.MuteAllRemoteVideoStreams(isVideoPaused);

        Button pauseButton = GameObject.Find("FreezeButton").GetComponent<Button>();
        if (pauseButton != null)
        {
            Text buttonText = pauseButton.GetComponentInChildren<Text>();
            buttonText.text = isVideoPaused ? "Resume" : "Pause";
        }

        // Get a reference to the pop-up text element
        Text popupText = GameObject.Find("PopupText").GetComponent<Text>();

        if (isVideoPaused)
        {
            // Show the pop-up text when video is paused
            popupText.text = "Video is Paused";
            popupText.enabled = true;
        }
        else
        {
            // Hide the pop-up text when video is resumed
            popupText.enabled = false;
        }
    }
}


void ToggleScreenSharing()
    {
        if (isScreenSharing)
        {
            mRtcEngine.StopScreenCapture();
            isScreenSharing = false;

            // Change the button text or perform any UI update as needed
            Button shareScreenButton = GameObject.Find("ShareButton").GetComponent<Button>();
            if (shareScreenButton != null)
            {
                Text buttonText = shareScreenButton.GetComponentInChildren<Text>();
                buttonText.text = "Share Screen";
            }
        }
        else
        {
            ShareDisplayScreen();
            isScreenSharing = true;

            // Change the button text or perform any UI update as needed
            Button shareScreenButton = GameObject.Find("ShareButton").GetComponent<Button>();
            if (shareScreenButton != null)
            {
                Text buttonText = shareScreenButton.GetComponentInChildren<Text>();
                buttonText.text = "Stop Sharing";
            }
        }
    }


 void ShareDisplayScreen()
    {
        ScreenCaptureParameters sparams = new ScreenCaptureParameters
        {
            captureMouseCursor = true,
            frameRate = 30
        };

        mRtcEngine.StopScreenCapture();

        // Start sharing the display screen
        mRtcEngine.StartScreenCaptureByDisplayId(GetDisplayId(), default(Rectangle), sparams);
    }

   uint GetDisplayId()
{
    uint displayId = 0;

    // Platform-specific logic to get the display ID for screen sharing
    // Implement this based on the target platform (Windows, macOS, etc.)

    // Example for Windows:
    #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        // Retrieve the primary display ID for Windows
        displayId = 0; // Set your logic to obtain the display ID
    #endif

    // Example for macOS:
    #if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        List<uint> ids = AgoraNativeBridge.GetMacDisplayIds();
        if (ids.Count > 0)
        {
            displayId = ids[0]; // Change logic here to get the desired display ID
        }
    #endif

    // Add more platform-specific conditions and logic as needed for different platforms

    return displayId;
}




    protected VideoSurface makeImageSurface(string goName)
    {
        GameObject go = new GameObject();

        if (go == null)
        {
            return null;
        }

        go.name = goName;

        // to be renderered onto
        RawImage image = go.AddComponent<RawImage>();
        image.rectTransform.sizeDelta = new Vector2(1, 1);// make it almost invisible

        // make the object draggable
        go.AddComponent<UIElementDragger>();
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            go.transform.SetParent(canvas.transform);
        }
        // set up transform
        go.transform.Rotate(0f, 0.0f, 180.0f);
        Vector2 v2 = AgoraUIUtils.GetRandomPosition(100);
        go.transform.localPosition = new Vector3(v2.x, v2.y, 0);
        go.transform.localScale = Vector3.one;

        // configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        return videoSurface;
    }
}
 