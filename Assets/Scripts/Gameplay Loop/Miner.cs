using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveIn.GameplayLoop
{
    public class Miner : MonoBehaviour
    {
        [SerializeField] RectTransform interactionPoint;
        [SerializeField] float maxRaycastDistance = 2;
        [SerializeField] LayerMask raycastFilterLayer;
        [SerializeField] int gold;

        float timer;
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(interactionPoint.position);
                RaycastHit hit;

                if (Physics.Raycast(ray,out hit, maxRaycastDistance,raycastFilterLayer))
                {
                    Ore ore = hit.collider.gameObject.GetComponent<Ore>();
                    if (ore == null) return;
                    
                    if(timer >= ore.GetDestuctionTime())
                    {
                        if (ore.GetIsGold())
                        {
                            gold += 1;
                            ore.Destroy();
                        }
                        else
                        {
                            print("That was not gold!");
                        }
                        
                    }

                    timer += Time.deltaTime;
                    
                }
            }
            else
            {
                timer = 0;
            }
        }
    }
}
