using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    PoolManager poolManager; //오브젝트 풀링 매니져

    static CreateManager c_instance; // 유일성이 보장된다
    static public CreateManager Instance { get { Init(); return c_instance; } } // 유일한 매니저를 갖고온다

    private void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    static public void Init()
    {
        if (c_instance == null)
        {
            GameObject go = GameObject.Find("@CreateManager");
            if (go == null)
            {
                go = Managers.Resource.Instantiate("Object/CreateManager");
                go.AddComponent<CreateManager>();
            }

            //DontDestroyOnLoad(go);
            c_instance = go.GetComponent<CreateManager>();
        }
    }

    #region 생성이벤트
    public void Create_01()
    {
        for (int i = 0; i < 15; i++)
        {
            Spawn();
        }
    }
    //public void Create_01_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn();
    //    }
    //}
    //public void Create_01_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn();
    //    }
    //}
    //public void Create_02()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn2();
    //    }
    //}
    //public void Create_02_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn2();
    //    }
    //}
    //public void Create_02_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn2();
    //    }
    //}
    //public void Create_03()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn3();
    //    }
    //}
    //public void Create_03_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn3();
    //    }
    //}
    //public void Create_03_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn3();
    //    }
    //}
    //public void Create_04()
    //{
    //    for (int i = 0; i < 13; i++)
    //    {
    //        Spawn4();
    //    }
    //}
    //public void Create_04_1()
    //{
    //    for (int i = 0; i < 18; i++)
    //    {
    //        Spawn4();
    //    }
    //}
    //public void Create_04_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn4();
    //    }
    //}
    //public void Create_Orc()
    //{
    //    for (int i = 0; i < 1; i++)
    //    {
    //        SpawnOrc();
    //    }
    //}
    //public void Create_10()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn10();
    //    }
    //}
    //public void Create_10_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn10();
    //    }
    //}
    //public void Create_10_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn10();
    //    }
    //}
    //public void Create_11()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn11();
    //    }
    //}
    //public void Create_11_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn11();
    //    }
    //}
    //public void Create_11_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn11();
    //    }
    //}
    //public void Create_12()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn12();
    //    }
    //}
    //public void Create_12_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn12();
    //    }
    //}
    //public void Create_12_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn12();
    //    }
    //}
    //public void Create_13()
    //{
    //    for (int i = 0; i < 15; i++)
    //    {
    //        Spawn13();
    //    }
    //}
    //public void Create_13_1()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        Spawn13();
    //    }
    //}
    //public void Create_13_2()
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        Spawn13();
    //    }
    //}
    //public void Create_Orc2()
    //{
    //    for (int i = 0; i < 1; i++)
    //    {
    //        SpawnOrc2();
    //    }
    //}
    //public void RepeatCreate_01()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        RepeatSpawn();
    //    }
    //}
    //public void RepeatCreate_02()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        RepeatSpawn2();
    //    }
    //}
    //public void RepeatCreate_03()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        RepeatSpawn3();
    //    }
    //}
    //public void RepeatCreate_04()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        RepeatSpawn4();
    //    }
    //}
    //public void RepeatCreate_05()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        RepeatSpawn5();
    //    }
    //}
    //#endregion

    //#region 오브잭트 생성
    void Spawn()
    {
        Chicken chicken = poolManager.GetFromPool<Chicken>();
        Chick chick = poolManager.GetFromPool<Chick>();
    }
    //void Spawn2()
    //{
    //    Chicken2 chicken2 = poolManager.GetFromPool<Chicken2>();
    //    Chick2 chick2 = poolManager.GetFromPool<Chick2>();
    //}
    //void Spawn3()
    //{
    //    Cow cow = poolManager.GetFromPool<Cow>();
    //    Cow2 cow2 = poolManager.GetFromPool<Cow2>();
    //}
    //void Spawn4()
    //{
    //    Cow3 cow3 = poolManager.GetFromPool<Cow3>();
    //    Wolf wolf = poolManager.GetFromPool<Wolf>();
    //}
    //void SpawnOrc()
    //{
    //    Orc orc = poolManager.GetFromPool<Orc>();
    //}
    //void Spawn10()
    //{
    //    Larva larva = poolManager.GetFromPool<Larva>();
    //    Larva2 larva2 = poolManager.GetFromPool<Larva2>();
    //}
    //void Spawn11()
    //{
    //    Larva3 larva3 = poolManager.GetFromPool<Larva3>();
    //    Larva4 larva4 = poolManager.GetFromPool<Larva4>();
    //}
    //void Spawn12()
    //{
    //    Rat rat = poolManager.GetFromPool<Rat>();
    //    Rat2 rat2 = poolManager.GetFromPool<Rat2>();
    //}
    //void Spawn13()
    //{
    //    Rat3 rat3 = poolManager.GetFromPool<Rat3>();
    //    Lizard orc = poolManager.GetFromPool<Lizard>();
    //}
    //void SpawnOrc2()
    //{
    //    Orc2 orc = poolManager.GetFromPool<Orc2>();
    //}
    //void RepeatSpawn()
    //{
    //    Pig pig = poolManager.GetFromPool<Pig>();
    //}
    //void RepeatSpawn2()
    //{
    //    Pig2 pig2 = poolManager.GetFromPool<Pig2>();
    //}
    //void RepeatSpawn3()
    //{
    //    Pig3 pig3 = poolManager.GetFromPool<Pig3>();
    //}
    //void RepeatSpawn4()
    //{
    //    Pig4 pig4 = poolManager.GetFromPool<Pig4>();
    //}
    //void RepeatSpawn5()
    //{
    //    Pig5 pig5 = poolManager.GetFromPool<Pig5>();
    //}
    public void CoinSpawn()
    {
        Coin coin = poolManager.GetFromPool<Coin>();
    }
    //public void Coin2Spawn()
    //{
    //    Coin2 coin = poolManager.GetFromPool<Coin2>();
    //}
    //#endregion

    //#region 오브젝트 회수
    public void ReturnPool(Chicken clone)
    {
        poolManager.TakeToPool<Chicken>(clone.idName, clone);
    }
    public void ReturnPool(Chick clone)
    {
        poolManager.TakeToPool<Chick>(clone.idName, clone);
    }
    //public void ReturnPool(Chicken2 clone)
    //{
    //    poolManager.TakeToPool<Chicken2>(clone.idName, clone);
    //}
    //public void ReturnPool(Chick2 clone)
    //{
    //    poolManager.TakeToPool<Chick2>(clone.idName, clone);
    //}
    //public void ReturnPool(Cow clone)
    //{
    //    poolManager.TakeToPool<Cow>(clone.idName, clone);
    //}
    //public void ReturnPool(Cow2 clone)
    //{
    //    poolManager.TakeToPool<Cow2>(clone.idName, clone);
    //}
    //public void ReturnPool(Cow3 clone)
    //{
    //    poolManager.TakeToPool<Cow3>(clone.idName, clone);
    //}
    //public void ReturnPool(Wolf clone)
    //{
    //    poolManager.TakeToPool<Wolf>(clone.idName, clone);
    //}
    //public void ReturnPool(Pig clone)
    //{
    //    poolManager.TakeToPool<Pig>(clone.idName, clone);
    //}
    //public void ReturnPool(Pig2 clone)
    //{
    //    poolManager.TakeToPool<Pig2>(clone.idName, clone);
    //}
    //public void ReturnPool(Pig3 clone)
    //{
    //    poolManager.TakeToPool<Pig3>(clone.idName, clone);
    //}
    //public void ReturnPool(Pig4 clone)
    //{
    //    poolManager.TakeToPool<Pig4>(clone.idName, clone);
    //}
    //public void ReturnPool(Pig5 clone)
    //{
    //    poolManager.TakeToPool<Pig5>(clone.idName, clone);
    //}
    //public void ReturnPool(Wasp clone)
    //{
    //    poolManager.TakeToPool<Wasp>(clone.idName, clone);
    //}
    public void ReturnPool(Coin clone)
    {
        poolManager.TakeToPool<Coin>(clone.idName, clone);
    }
    //public void ReturnPool(Coin2 clone)
    //{
    //    poolManager.TakeToPool<Coin2>(clone.idName, clone);
    //}
    //public void ReturnPool(Orc clone)
    //{
    //    poolManager.TakeToPool<Orc>(clone.idName, clone);
    //}
    //public void ReturnPool(Larva clone)
    //{
    //    poolManager.TakeToPool<Larva>(clone.idName, clone);
    //}
    //public void ReturnPool(Larva2 clone)
    //{
    //    poolManager.TakeToPool<Larva2>(clone.idName, clone);
    //}
    //public void ReturnPool(Larva3 clone)
    //{
    //    poolManager.TakeToPool<Larva3>(clone.idName, clone);
    //}
    //public void ReturnPool(Larva4 clone)
    //{
    //    poolManager.TakeToPool<Larva4>(clone.idName, clone);
    //}
    //public void ReturnPool(Rat clone)
    //{
    //    poolManager.TakeToPool<Rat>(clone.idName, clone);
    //}
    //public void ReturnPool(Rat2 clone)
    //{
    //    poolManager.TakeToPool<Rat2>(clone.idName, clone);
    //}
    //public void ReturnPool(Rat3 clone)
    //{
    //    poolManager.TakeToPool<Rat3>(clone.idName, clone);
    //}
    //public void ReturnPool(Lizard clone)
    //{
    //    poolManager.TakeToPool<Lizard>(clone.idName, clone);
    //}
    //public void ReturnPool(Orc2 clone)
    //{
    //    poolManager.TakeToPool<Orc2>(clone.idName, clone);
    //}
    #endregion
}
