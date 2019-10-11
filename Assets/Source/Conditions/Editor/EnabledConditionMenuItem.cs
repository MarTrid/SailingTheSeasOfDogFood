using Innoactive.Hub.Training.Conditions;
using Innoactive.Hub.Training.Editors.Configuration;
using UnityEngine;

public class EnabledConditionMenuItem : Menu.Item<ICondition>
{
    public override ICondition GetNewItem()
    {
        return new EnabledCondition();
    }

    public override GUIContent DisplayedName
    {
        get
        {
            return new GUIContent("DogFood/EnabledCondition");
        }
    }
}
