using System;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
   [SerializeField] private EnemySpawner _enemySpawner;
   [SerializeField] private int maxIterations; 
   [SerializeField] private float stepSize; 
   [SerializeField] private float minDistanceThreshold ; 

   private Transform[] points;
   private Vector3 _maxDistancePoint;
   private void SetTransform()
   {
      for (int i = 0; i < _enemySpawner.EnemyBlues.Count; i++)
      {
         points = new[] { _enemySpawner.EnemyBlues[i].transform };
      }
   }
   private void Start()
   {
      Vector3 startingPoint = Vector3.zero;
      SetTransform();

     
      for (int i = 0; i < maxIterations; i++)
      {
         float maxDistance = 0f;
         _maxDistancePoint = startingPoint;
         
         foreach (Transform point in points )
         {
            float distance = Vector3.Distance(startingPoint, point.position);
            if (distance > maxDistance)
            {
               maxDistance = distance;
               _maxDistancePoint = point.position;
            }
         }
         
         if (maxDistance < minDistanceThreshold)
         {
            Debug.Log("Max distance reached. Iterations: " + i);
            break;
         }
         
         startingPoint += (_maxDistancePoint - startingPoint).normalized * stepSize;
      }
      
      GameObject newPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      newPoint.transform.position = startingPoint;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<TeleportationWall>())
      {
         transform.position = _maxDistancePoint;
      }
   }
}



