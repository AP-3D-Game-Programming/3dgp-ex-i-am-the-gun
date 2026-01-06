using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Fires Weapon", story: "[FireController] Fires", category: "Action", id: "97ed2ac5afb1cab23e8fe1831ab4828c")]
public partial class FiresWeaponAction : Action
{
    [SerializeReference] public BlackboardVariable<AIFireController> FireController;


    protected override Status OnUpdate()
    {
        if (FireController == null || FireController.Value == null)
            return Status.Failure;


        FireController.Value.FireAtTarget();
        return Status.Success; //Instant action
    }
}

