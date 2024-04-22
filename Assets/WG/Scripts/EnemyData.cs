using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] float Meteor_BIG_HP = 30f;
    [SerializeField] float Meteor_Wide_HP = 40f;
    [SerializeField] float BOSS_5_HP = 10000f;
    [SerializeField] float Hazzling1_HP = 1000f;

    public float Meteor_BIG_HP_p { get { return Meteor_BIG_HP; } set { Meteor_BIG_HP = value; } }
    public float Meteor_Wide_HP_p { get { return Meteor_Wide_HP; } set { Meteor_Wide_HP = value; } }
    public float BOSS_5_HP_p { get { return BOSS_5_HP; } set { BOSS_5_HP = value; } }
    public float Hazzling1_HP_p { get { return Hazzling1_HP; } set { Hazzling1_HP = value; } }
}
