using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class MSAA : MonoBehaviour
{
    [SerializeField]
    private Slider renderScaleSlider;
    [SerializeField]
    private Button msaaButton;
    [SerializeField]
    private TMPro.TMP_Text text;

    private static string[] MSAA_TEXT = new string[] { "NONE", "x2", "x4", "x8" };
    private UniversalRenderPipelineAsset pipelineAsset;
    private int msaaValue;

    private void Start()
    {
        pipelineAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (pipelineAsset != null)
        {
            msaaButton.onClick.AddListener(OnPushMSAA);
            renderScaleSlider.onValueChanged.AddListener(OnValueChanged);
            msaaValue = (int)Mathf.Log(pipelineAsset.msaaSampleCount, 2);
            renderScaleSlider.value = pipelineAsset.renderScale;
            SetMSAAText();
        }
    }

    private void OnValueChanged(float value)
    {
        if (pipelineAsset != null)
        {
            pipelineAsset.renderScale = value;
        }
    }

    private void OnPushMSAA()
    {
        if (pipelineAsset != null)
        {
            msaaValue = (msaaValue + 1) % 4;
            pipelineAsset.msaaSampleCount = 1 << msaaValue;
            SetMSAAText();
        }
    }

    private void SetMSAAText()
    {
        text.text = "MSAA " + MSAA_TEXT[msaaValue % 4];
    }
}
