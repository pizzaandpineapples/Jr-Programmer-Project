using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_ProductionSpeed = 0.5f; // add new private backing field. A private field that stores the data exposed by the public property. 

    // With the backing field declared, you can now manually add the get and set functions, which retrieve or assign the backing field when someone accesses the public property.
    // Since these are not going to be simple get and set methods, it’s not possible to use the “{ get; set; }” shorthand we used with the auto-implemented property earlier. 
    public float ProductionSpeed // remove value from public property.
    {
        get { return m_ProductionSpeed; } // getter returns backing field
        set {
            if (value < 0.0f)
            {

                Debug.LogError("You can't set a negative production speed!");
            }
            else
            {
                m_ProductionSpeed = value; // original setter now in if/else statement
            }
        } // setter uses backing field
    }

    private float m_CurrentProduction = 0.0f;

    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime; // // swap in m_ version
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {m_ProductionSpeed}/s"; // swap in m_ version

    }
    
    
}
