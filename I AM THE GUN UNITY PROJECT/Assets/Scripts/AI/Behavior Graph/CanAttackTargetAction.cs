using System;
using Unity.AppUI.Core;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CanAttackTarget", story: "Can [FireController] Attack [Target] In [Range] NotBlockedBy [BlockingLayers]", category: "Action/Conditional", id: "ea0d023b23d1b8d413592b6ebe1ca045")]
public partial class CanAttackTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<AIFireController> FireController;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<int> BlockingLayers;

    private NavMeshAgent m_Agent;

    protected override Status OnUpdate()
    {
        if (Target == null || Target.Value == null || FireController == null || FireController.Value == null)
        {
            Debug.Log("[LOS] No target assigned");
            return Status.Failure;
        }

        //Set origin & target positions
        Vector3 origin = GameObject.transform.position + Vector3.up * 1f; // AI eye height
        Vector3 targetPos = Target.Value.transform.position + Vector3.up * 0.9f; // Player head height
        Vector3 dir = targetPos - origin;
        float distanceToTarget = dir.magnitude;

        //Range check
        if (distanceToTarget > Range.Value)
        {
            Debug.Log($"[LOS] Target too far ({distanceToTarget:F1} units) — max range {Range.Value}");
            if (m_Agent != null) m_Agent.updateRotation = true;
            FireController.Value.target = null;
            return Status.Failure;
        }

        //Prepare mask to ignore self
        int mask = BlockingLayers.Value & ~(1 << GameObject.layer);

        //Raycast up to the player
        if (Physics.Raycast(origin, dir.normalized, out RaycastHit hit, distanceToTarget, mask))
        {
            if (hit.transform != Target.Value.transform)
            {
                if (m_Agent != null) m_Agent.updateRotation = true;
                FireController.Value.target = null;
                Debug.Log($"[LOS] Blocked by: {hit.collider.name} at {hit.distance:F1} units");
                return Status.Failure;
            }
        }

        //Clear line of sight
        Debug.Log($"[LOS] Target visible! Distance: {distanceToTarget:F1} units");

        if (m_Agent != null) m_Agent.updateRotation = false;
        //visualize ray in Scene view
        Debug.DrawRay(origin, dir.normalized * distanceToTarget, Color.green);

        FireController.Value.target = Target.Value.transform;
        return Status.Success;
    }
}

