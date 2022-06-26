
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cost System", fileName = "New Cost System")]
public class UpgradeCosts : ScriptableObject
{
    [Header("Health Upgrade Costs")]
    public int cost_Health_Lvl1;
    public int cost_Health_Lvl2;
    public int cost_Health_Lvl3;

    [Header("Attack Upgrade Costs")]
    public int cost_Attack_Lvl1;
    public int cost_Attack_Lvl2;
    public int cost_Attack_Lvl3;
}
