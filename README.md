<<<<<<< HEAD
# Agora Unity Video SDK Demos

This project contains sample code to demonstrate advanced features provided by the Agora Video SDK.

## Note March 8th, 2021 - this repo has been merged into [Agora Unity QuickStart](https://github.com/AgoraIO/Agora-Unity-Quickstart), please get the latest update there

  
## Feature Note

  

 **1. App Screen Share**
     This demo shows how to share the screen (or rather "current app window") by recording the current camera view and push as an external video frame.  Applicable to mobile and desktop platforms.<br>
 **2. Desktop Screen Share**
     This demo shows how to share the desktop/window of the current running OS.  Applicable to desktop platforms only (Windows/MacOS). Note SDK calls for Windows are still under improvement.  Sharing specific window is not working.<br>
 **3. Transcoding**
     The demo shows the configuration to publish live streaming video to known CDNs, including Youtube, Facebook and Twitch.<br>
 **4. Simple 1 to 1 video call **
     Allow communication or live streaming mode for 1 to 1 call <br>
    

With this sample app, you can:

  

## Developer Environment Requirements

  

- Unity3d 2017 or above

-  [Agora Video SDK from Unity Asset Store](https://assetstore.unity.com/packages/tools/video/agora-video-chat-sdk-for-unity-134502)

- Real devices (Windows, Android, iOS, and Mac supported)

  (Note some feature may require SDK version 3.0.1 and higher.)

  

## Quick Start

  

This section shows you how to prepare, build, and run the sample application.

  

### Obtain an App ID

  

To build and run the sample application, get an App ID:

  

1.  Create a developer account at  [agora.io](https://dashboard.agora.io/signin/). Once you finish the signup process, you will be redirected to the Dashboard.

2.  Navigate in Agora Console on the left to  ****Projects****  >  ****More****  >  ****Create****  >  ****Create New Project****.

3.  Save the  ****App ID****  from the Dashboard for later use.

  

  

### [](https://github.com/AgoraIO-Community/Unity-RTM#run-the-application)Run the Application

  

1.  First clone this repository

2. From Asset Store window in the Unity Editor, download and import the Agora Video SDK

3.  [Mac only] Obtain the Mac ScreenShare library plugin [here](https://bit.ly/2AIFyjK)

4. [Mac only] import the downloaded plugin from Asset->Import Package->Custom Package

5.  From Project window, open Asset/AgoraEngine/Demo+/SceneHome2.scene

6. Next go into your Hierarchy window and select  ****GameController****, in the Inspector add your  ****App ID****  to to the  ****AppID****  Input field

  

****Note****

The library from Step 3/4 is non-official.  You may build your own Mac library in case this doesn't work for you.  Source code gist can be found [here](https://gist.github.com/icywind/0fd26481dd6884821d7f917944ec0042).

#### [](https://github.com/AgoraIO-Community/Unity-RTM#test-in-editor)Test in Editor

  

1.  Go to  ****File****  >  ****Builds****  >  ****Platform****  and select either Windows or Mac depending on the device you are working on.

2. [Mac] make sure you fill in Camera and Microphone usage description

3. Press the Unity Play button to run the example scene

  

#### [](https://github.com/AgoraIO-Community/Unity-RTM#deploy-to-windows-mac-android)Deploy to Windows, Mac, iOS, Android

  

1.  Deploy to Mac, Android, and Windows by simply changing the Platform in the  ****File****  >  ****Build Settings****, then switch to your prefered platform

2.  [Mac or iOS] make sure you fill in Camera and Microphone usage description

3.  Hit  ****Build and Run****

  

## [](https://github.com/AgoraIO-Community/Unity-RTM#resources)Resources

  

- For potential issues, take a look at our  [FAQ](https://docs.agora.io/en/faq)  first

- Dive into  [Agora SDK Samples](https://github.com/AgoraIO)  to see more tutorials, including [API demos](https://github.com/AgoraIO/Agora-Unity-Quickstart/tree/master/API-Example-Unity)

- Take a look at  [Agora Use Case](https://github.com/AgoraIO-usecase)  for more complicated real use case

- Repositories managed by developer communities can be found at  [Agora Community](https://github.com/AgoraIO-Community)




## License

  

The MIT License (MIT).
=======
# Remote assistance



## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.com/ar-projects4/remote-assistance.git
git branch -M main
git push -uf origin main
```

## Integrate with your tools

- [ ] [Set up project integrations](https://gitlab.com/ar-projects4/remote-assistance/-/settings/integrations)

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Set auto-merge](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing (SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

***

# Editing this README

When you're ready to make this README your own, just edit this file and use the handy template below (or feel free to structure it however you want - this is just a starting point!). Thanks to [makeareadme.com](https://www.makeareadme.com/) for this template.

## Suggestions for a good README

Every project is different, so consider which of these sections apply to yours. The sections used in the template are suggestions for most open source projects. Also keep in mind that while a README can be too long and detailed, too long is better than too short. If you think your README is too long, consider utilizing another form of documentation rather than cutting out information.

## Name
Choose a self-explaining name for your project.

## Description
Let people know what your project can do specifically. Provide context and add a link to any reference visitors might be unfamiliar with. A list of Features or a Background subsection can also be added here. If there are alternatives to your project, this is a good place to list differentiating factors.

## Badges
On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals
Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation
Within a particular ecosystem, there may be a common way of installing things, such as using Yarn, NuGet, or Homebrew. However, consider the possibility that whoever is reading your README is a novice and would like more guidance. Listing specific steps helps remove ambiguity and gets people to using your project as quickly as possible. If it only runs in a specific context like a particular programming language version or operating system or has dependencies that have to be installed manually, also add a Requirements subsection.

## Usage
Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support
Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap
If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing
State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment
Show your appreciation to those who have contributed to the project.

## License
For open source projects, say how it is licensed.

## Project status
If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.
>>>>>>> 0c0d1a123ccbbf4aec99d75fe57bd229ab4b560f
