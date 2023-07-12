using System.IO;
using UnityEngine;

public class QuestionPopup : MonoBehaviour
{
    public void FileDelete()
    {
        Managers.Sound.Play("Button01");

        File.Delete(Managers.SaveLode.path);
        Debug.Log($"{Managers.SaveLode.path} ªË¡¶");
    }
}
