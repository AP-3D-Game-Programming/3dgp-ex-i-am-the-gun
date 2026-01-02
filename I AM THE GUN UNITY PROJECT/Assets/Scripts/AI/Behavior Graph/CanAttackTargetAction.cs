using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CanAttackTarget", story: "Can Attack [Target] In [Range] NotBlockedBy [LayerMask]", category: "Action/Conditional", id: "ea0d023b23d1b8d413592b6ebe1ca045")]
public partial class CanAttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<int> LayerMask;

    protected override Status OnUpdate()
    {
        if (Target == null || Target.Value == null)
            return Status.Failure;
        Vector3 origin = GameObject.transform.position + (GameObject.transform.forward * 0.7f);

        Vector3 targetPos = Target.Value.transform.position + Vector3.up * 1.0f;

        //Calculate direction (Destination - Start)
        Vector3 dir = targetPos - origin;

        Debug.DrawRay(origin, dir.normalized * 500f, Color.red);

        if (dir.magnitude < 0.1f) return Status.Failure;

        if (dir.magnitude > Range.Value)
            return Status.Failure;



        if (Physics.Raycast(origin, dir.normalized, out RaycastHit hit, Range.Value, 1 << LayerMask.Value))
        {
            if (hit.collider.gameObject == Target.Value)
            {
                Debug.Log("Target Spotted: " + hit.collider.name);
                return Status.Success;
            }

            Debug.Log("Blocked by: " + hit.collider.name);
            return Status.Success;
        }

        Debug.Log("Raycast hit nothing.");

        return Status.Failure;
    }
}

