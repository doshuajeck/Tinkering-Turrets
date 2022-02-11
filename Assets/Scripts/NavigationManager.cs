using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;

    [SerializeField] private NavMeshAgent[] pathfinders;
    [SerializeField] private Transform goal;
    private bool valid;
    private bool computed;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public bool Check()
    {
        valid = false;

        //StartCoroutine(CheckRoutine());
        
        //int count = 0;
        /*while (!computed)
            Debug.Log("Not computed");
        
        Debug.Log("Computed in " + count + " attempts");
        */ 
        Invoke("CheckStuff", 0.01f);
        return valid;
    }

    public IEnumerator Check(GameObject tower, Vector2 position, int cost)
    {
        computed = false;
        if (EconomyManager.instance.money >= cost)
        {
            GameObject newTower = Instantiate(tower, position, Quaternion.identity);
            yield return new WaitForSeconds(0.0001f);
            
            do
            {
                CheckStuff();
            } while (!computed);
            
            

            if (!valid)
            {
                Destroy(newTower);
                Debug.Log("Blocked Path");
                //return false;
            }
            else
            {
                EconomyManager.instance.money -= cost;
                Debug.Log("Purchased");
                //return true;
            }
        }

        //return false;
    }
    
    private IEnumerator CheckRoutine()
    {
        computed = false;
        yield return new WaitForSeconds(0.0001f);
        int validInt = 0;
        foreach (var current in pathfinders)
        {
            NavMeshPath path = new NavMeshPath();
            current.CalculatePath(goal.position, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                validInt++;
            }
        }

        if (validInt == 0)
            valid = true;
        else
        {
            valid = false;
        }
        
        computed = true;
        Debug.Log(valid);
    }

    private void CheckStuff()
    {
        int validInt = 0;
        foreach (var current in pathfinders)
        {
            NavMeshPath path = new NavMeshPath();
            current.CalculatePath(goal.position, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            computed = true;
            Debug.Log(valid);
        }
    }
}
