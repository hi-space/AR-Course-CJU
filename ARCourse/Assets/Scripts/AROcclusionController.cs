using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AROcclusionController : MonoBehaviour
{
    [SerializeField]
    private Button envDepthBtn;

    [SerializeField]
    private Button humanDepthBtn;

    [SerializeField]
    private Button prefBtn;

    private AROcclusionManager arOcclusionManager;

    private void Awake()
    {
        arOcclusionManager = GetComponent<AROcclusionManager>();

        envDepthBtn.onClick.AddListener(ChangeEnvDepthMode);
        humanDepthBtn.onClick.AddListener(ChangeHumanDepthMode);
        prefBtn.onClick.AddListener(ChangePrefMode);

        UpdateEnvDepthBtn();
        UpdateHumanDepthBtn();
        UpdatePrefBtn();
    }

    private void ChangeEnvDepthMode()
    {
        int MAX = System.Enum.GetValues(typeof(EnvironmentDepthMode)).Length;
        if ((int) arOcclusionManager.requestedEnvironmentDepthMode >= MAX - 1)
        {
            arOcclusionManager.requestedEnvironmentDepthMode = 0;
        }
        else
        {
            arOcclusionManager.requestedEnvironmentDepthMode++;
        }

        UpdateEnvDepthBtn();
    }

    private void ChangeHumanDepthMode()
    {
        int MAX = System.Enum.GetValues(typeof(HumanSegmentationDepthMode)).Length;
        if ((int)arOcclusionManager.requestedHumanDepthMode >= MAX - 1)
        {
            arOcclusionManager.requestedHumanDepthMode = 0;
        }
        else
        {
            arOcclusionManager.requestedHumanDepthMode++;
        }

        UpdateHumanDepthBtn();
    }

    private void ChangePrefMode()
    {
        int MAX = System.Enum.GetValues(typeof(OcclusionPreferenceMode)).Length;
        if ((int)arOcclusionManager.requestedOcclusionPreferenceMode >= MAX - 1)
        {
            arOcclusionManager.requestedOcclusionPreferenceMode = 0;
        }
        else
        {
            arOcclusionManager.requestedOcclusionPreferenceMode++;
        }

        UpdatePrefBtn();
    }

    private void UpdateEnvDepthBtn()
    {
        envDepthBtn.GetComponentInChildren<Text>().text = "Env:\n" + arOcclusionManager.requestedEnvironmentDepthMode.ToString();
    }
    
    private void UpdateHumanDepthBtn()
    {
        humanDepthBtn.GetComponentInChildren<Text>().text = "Human:\n" + arOcclusionManager.requestedHumanDepthMode.ToString();
    }

    private void UpdatePrefBtn()
    {
        prefBtn.GetComponentInChildren<Text>().text = "Pref:\n" + arOcclusionManager.requestedOcclusionPreferenceMode.ToString();
    }
}
