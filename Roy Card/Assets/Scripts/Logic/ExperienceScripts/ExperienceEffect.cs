using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;

public class ExperienceEffect
{
    public virtual void ActivateEffect(int specialAmount = 0, ICharacter target = null)
    {
        Debug.Log("No Experience effect with this name found! Check for typos in CardAssets");
    }
}
