using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a single flower with nector
/// </summary>

public class Flower : MonoBehaviour
{
    [Tooltip("The color when the flower is full")]
    public Color fullFlowerColor = new Color(1f, 0f, .3f);

    [Tooltip("The color when the flower is empty")]
    public Color emptyFlowerColor = new Color(.5f, 0f, 1f);

    ///<summary>
    ///The trigger collider representing the nector
    ///</summary>
    [HideInInspector]
    public Collider nectorCollider;

    //The solid collider representing the flower petals
    private Collider flowerCollider;

    //The flower's material
    private Material flowerMaterial;

    ///<summary>
    ///A vector pointing straight out of the flower
    ///</summary>

    public Vector3 FlowerUpVector
    {
        get
        {
            return nectorCollider.transform.up;
        }
    }
    
///<summary>
///The center position of the nector collider 
///</summary>

    public Vector3 FlowerCenterPosition
    {
        get
        {
            return nectorCollider.transform.position;

        }
    }

    ///<summary>
    ///The amount of nector remaining in the flower
    /// </summary>
   
    public float NectorAmount { get; private set; }


    ///<summary>
    ///Whether the flower has any nector remaining 
    ///</summary>

    public bool HasNector
    {
        get
        {
            return NectorAmount > 0f;
        }
    }

    /// <summary>
    /// Attemps to remove nector from the flower
    /// </summary>
    /// <param name="amount">The amount of nector to remove</param>
    /// <returns>The actual amount successfully removed </returns>
    
    public float Feed(float amount)
    {
        //Track how much nector was successfully taken (cannot taken more than is availavle)
        float nectorTaken = Mathf.Clamp(amount, 0f, NectorAmount);
       
        //Subtract the nector
        NectorAmount -= amount;
       
        if (NectorAmount <= 0)
        {
            //No nector remaining
            NectorAmount = 0;

            //Disable the flower and nector colliders
            flowerCollider.gameObject.SetActive(false);
            nectorCollider.gameObject.SetActive(false);

            //Change the flower color to indicate that it is empty
            flowerMaterial.SetColor("_BaseColor", emptyFlowerColor);

        }
        //Return the amount of nector that was taken
        return nectorTaken;
    }
  
    ///<summary>
    ///Resets the flower
    /// </summary>
    
    public void ResetFlower()
    {
        //Refill the nector 
        NectorAmount = 1f;

        //Enable the flower and nector colliders;
        flowerCollider.gameObject.SetActive(true);
        nectorCollider.gameObject.SetActive(true);

        //Change the flower color to indicate that it is full
        flowerMaterial.SetColor("_BaseColor", fullFlowerColor);

    }
    ///<summary>
    ///Called when the flower wakes up
    /// </summary>

    private void Awake ()
    {
        //FInd the flower's mesh renderer and get the main material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshRenderer.material;

        //Find the flower and nector colliders
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectorCollider = transform.Find("FlowerNectorCollider").GetComponent<Collider>();
    }


}
