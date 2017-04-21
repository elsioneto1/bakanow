using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackAux  {


    void ActivateProjectileProperties(Vector2 force, Vector3 pos);
    bool GetActive();
}
