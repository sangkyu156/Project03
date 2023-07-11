using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Potion
    {
        HP_Potion = 3, HP_Potion2 = 5, HP_Potion3 = 7
    }

    public enum Scene
    {
        Unknown,
        Title,
        Intro,
        Stage,
        Main,
        GameOver,
        Clear,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum Stage
    {
        Stage01 = 1,
        Stage02,
        Stage03,
        Stage04,
        Stage05
    }

    public enum Skills //��ų�̸��� �׻� Pefeb�̸��� ���ƾ� �Ѵ�. �׷��� Resources �������� ������
    {
        /**********1000**********/
        FireBall_Store = 1000, SawBlade_Store, WaveEnergy_Store, Quickness_Store, BulkingUp_Store, GoldChest_Store,
        PotionChest_Store,
        /**********900***********/

        /**********800***********/
        Redraw_Store = 800, Regenerate_Store,
        /**********700***********/

        /**********600***********/
        Tornado_Store = 600, Spark_Store, Clairvoyant_Store, Trident_Store, Regular_Store,
        /**********500***********/
        RageExplosion_Store = 500,
        /**********400***********/
        Redraw2_Store = 400,
        /**********300***********/

        /**********200***********/

        /**********100***********/

        /**********90************/

        /**********80************/

        /**********70************/

        /**********60************/

        /**********50************/

        /**********40************/

        /**********30************/
        BlackHole_Store = 300, Volcano_Store, Slowdown_Store, Redraw3_Store
        /**********20************/

        /**********10************/

    }

    public enum SkillPrice //��ų ����
    {
        Normal = 150, Rare = 250, Legendary = 200
    }
}
