using UnityEngine;

public class TitleScene : BaseScene
{
    public GameObject titleCamera;
    public GameObject[] bg_1;
    public GameObject[] bg_2;
    public GameObject[] bg_3;
    public GameObject[] bg_4;
    public GameObject[] bg_5;
    public GameObject[] bg_6;
    public GameObject[] bg_7;
    public GameObject[] bg_8;
    public float time_;
    float gradationSpeed;
    Vector3 cameraPosition;

    private void Start()
    {
        base.Init();

        cameraPosition = titleCamera.transform.position;
        time_ = 0;
        gradationSpeed = 0.3f;
    }

    private void Update()
    {
        cameraPosition.x += 4 * Time.deltaTime;

        titleCamera.transform.position = cameraPosition;

        time_ += Time.deltaTime;

        if (0 <= time_ && time_ < 4)
            BG_Gradation(bg_1);
        else if (4 <= time_ && time_ < 8)
            BG_Gradation(bg_2);
        else if (8 <= time_ && time_ < 12)
            BG_Gradation(bg_3);
        else if (12 <= time_ && time_ < 16)
            BG_Gradation(bg_4);
        else if (16 <= time_ && time_ < 20)
            BG_Gradation(bg_5);
        else if (20 <= time_ && time_ < 24)
            BG_Gradation(bg_6);
        else if (24 <= time_ && time_ < 28)
            BG_Gradation(bg_7);
        else if (28 <= time_ && time_ < 32)
            BG_Gradation(bg_8);
        else if (32 <= time_)
            BGReset();
    }

    void BG_Gradation(GameObject[] bg)
    {
        Color color_;
        for (int i = 0; i < bg.Length; i++)
        {
            color_ = bg[i].GetComponent<SpriteRenderer>().color;
            color_.a -= Time.deltaTime * gradationSpeed;
            bg[i].GetComponent<SpriteRenderer>().color = color_;
        }
    }

    void BGReset()
    {
        Color color_;
        for (int i = 0; i < bg_1.Length; i++)
        {
            color_ = bg_1[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_1[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_2.Length; i++)
        {
            color_ = bg_2[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_2[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_3.Length; i++)
        {
            color_ = bg_3[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_3[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_4.Length; i++)
        {
            color_ = bg_4[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_4[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_5.Length; i++)
        {
            color_ = bg_5[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_5[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_6.Length; i++)
        {
            color_ = bg_6[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_6[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_7.Length; i++)
        {
            color_ = bg_7[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_7[i].GetComponent<SpriteRenderer>().color = color_;
        }

        for (int i = 0; i < bg_8.Length; i++)
        {
            color_ = bg_8[i].GetComponent<SpriteRenderer>().color;
            color_.a = 1;
            bg_8[i].GetComponent<SpriteRenderer>().color = color_;
        }

        titleCamera.transform.position = new Vector3(0, -0.02f, -10);
        cameraPosition = titleCamera.transform.position;
        time_ = 0;
    }

    protected override void Init()
    {
        base.Init();

        Managers.currScene = (int)Define.Scene.Title;
    }


    public override void Clear()
    {
        //비어있음
    }
}
