using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    public void Upgrade();
    public void SetUpgrade(int upgrade);
    public bool Upgradeable();
}
