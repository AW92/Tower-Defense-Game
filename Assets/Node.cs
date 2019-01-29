using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    //the undeclared variables above are changed in the unity ui window.

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;


    void Start()
    {
        //this method has been created so that we don't have to make the call everytime in onmouseenter. It means its cached in the variable instead, rend.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        //are we hovering over a ui element in the way? like a shop item?
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("Something is here already, selling coming soon");
            return;
        }

        //Build a turret
        GameObject turretToBuild = buildManager.GetTurretToBuild();
        //without positionOffset, the turret would spawn inside the node
        turret =  (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        //are we hovering over a ui element in the way? like a shop item?
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        //This is here so that if nothing is selected in the shop, the node colour doesnt change, indicating to the player that something needs to be selected
        if (buildManager.GetTurretToBuild() == null)
            return;
        //called once everytime the mouse enters the confines of the collider. a unity thing.
        rend.material.color = hoverColor;
    }
    
    void OnMouseExit()
    {
        //startcolor is used instead of white incase we ever change the node color. startColor is set in Start method which is cool.
        rend.material.color = startColor;
    }
}
