using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomManager : MonoBehaviour
{
    PhantomMoveCache pmc;
    List<Phantom> phantoms;
    int timeStep = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        phantoms = new List<Phantom>();
        pmc = new PhantomMoveCache();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i != pmc.phantomCount(); ++i)
        {
            phantoms[i].GetComponent<Phantom>().Input(pmc.getPhantomMove(i, timeStep));
        }
        timeStep++;
    }

    public void Reset(List<AbstractCharacter.CharacterMove> moves, Vector3 startLoc)
    {
        timeStep = 0;
        pmc.addNewPhantom(moves, startLoc);
        foreach(var v in phantoms)
        {
            Destroy(v.gameObject);
        }
        phantoms.Clear();

        for(int i = 0; i != pmc.phantomCount(); ++i)
        {
            var p = Instantiate(Resources.Load<GameObject>("Prefab/Phantom"));
            p.transform.parent = this.transform.parent;
            p.transform.position = pmc.getStartPosition(i);
        }
    }
}
