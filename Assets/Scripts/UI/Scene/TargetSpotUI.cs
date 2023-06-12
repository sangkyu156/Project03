using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSpotUI : UI_Scene
{
    public Slider progress;
    public Image fill;

    public void SetMaxProgress(int progress_)
    {
        progress.maxValue = progress_;
        progress.value = 0;
    }

    public void SetProgress(int progress_)
    {
        progress.value = progress_;
    }
}
