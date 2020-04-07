using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CompositonAdder : MonoBehaviour
{
    readonly uint COMP_LIMIT = 9;

    public List<GameObject> compChampions = new List<GameObject>();
    public List<string> compTraits = new List<string>();

    public GameObject ChampProfile_Prefab;
    public GameObject Comp_Parent;

    public Vector3 CompAreaStart;

    private void Awake()
    {
        Comp_Parent = new GameObject();
        Comp_Parent.name = "Builded_Comp";
        CompAreaStart = new Vector3(0, -6, 0);

    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject selected = SendRayGameObject();

            if (selected != null && CheckChampionExistOnComp(selected) && compChampions.Count != COMP_LIMIT)
            {
                AddChampToComp(selected);

                AddToCompArea(selected, CompAreaStart);
                CompAreaStart.x++;
            }
        }
    }
    public bool CheckChampionExistOnComp(GameObject go)
    {
        if (compChampions.Contains(go))
            return false;

        return true;
    }

    public void AddChampToComp(GameObject go)
    {
        compChampions.Add(go);
        AddTraits(go);
    }

    public void AddTraits(GameObject go)
    {
        compTraits.Add(go.GetComponent<ChampionData>().traitsName1);
        compTraits.Add(go.GetComponent<ChampionData>().traitsName2);

        if (go.GetComponent<ChampionData>().traitsName3 != "")
            compTraits.Add(go.GetComponent<ChampionData>().traitsName3);
    }

    public GameObject SendRayGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Champion"))
                return hit.transform.gameObject;

            else
                return null;
        }

        else
            return null;

    }

    public void AddToCompArea(GameObject go, Vector3 Comp_pz)
    {
        GameObject CompChamp = Instantiate(go, Comp_pz, new Quaternion(0f, 0f, 0f, 0f)) as GameObject;
        CompChamp.transform.parent = Comp_Parent.transform;

    }
}
