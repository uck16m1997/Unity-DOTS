using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = MouseWorldPosition.I.GetPosition();

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var entityQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<UnitMover>().Build(entityManager);

            var unitMoverArray = entityQuery.ToComponentDataArray<UnitMover>(Allocator.Temp);
            var entityArray = entityQuery.ToEntityArray(Allocator.Temp);
            
            for (int i = 0; i < unitMoverArray.Length; i++) {
                var unitMover = unitMoverArray[i];
                unitMover.targetPosition = mouseWorldPosition;
                entityManager.SetComponentData(entityArray[i],unitMover);
            }
        }
    }
}
