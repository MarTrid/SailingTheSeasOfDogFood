using System.Collections;
using System.Runtime.Serialization;
using Innoactive.Hub.Training;
using Innoactive.Hub.Training.Attributes;
using Innoactive.Hub.Training.Conditions;
using Innoactive.Hub.Training.SceneObjects;
using Newtonsoft.Json;

[DataContract(IsReference = true)]
public class EnabledCondition : Condition<EnabledCondition.EntityData>
{
    [DisplayName("Enabled Condition")]
    [DataContract(IsReference = true)]

    public class EntityData : IConditionData
    {
        [DataMember]
        public SceneObjectReference Target { get; set; }
        
        [DataMember]
        public bool Inverted { get; set; }
        
        public Metadata Metadata { get; set; }
        public bool IsCompleted { get; set; }
        
        public string Name { get; set; }
    }
    
    private class ActiveProcess: IStageProcess<EntityData>
    {
        public void Start(EntityData data)
        {
            data.IsCompleted = false;
        }

        public IEnumerator Update(EntityData data)
        {
            while (data.Target.Value.GameObject.GetActive() == data.Inverted)
            {
                yield return null;
            }

            data.IsCompleted = true;
        }

        public void End(EntityData data)
        {
        }

        public void FastForward(EntityData data)
        {
        }
    }

    protected override IProcess<EntityData> Process
    {
        get
        {
            return new ActiveOnlyProcess<EntityData>(new ActiveProcess());
        }
    }

    protected override IAutocompleter<EntityData> Autocompleter { get; }
    
    public EnabledCondition(string target, bool inverted, string name = "Object Enabled")
    {
        Data = new EntityData()
        {
            Target = new SceneObjectReference(target),
            Inverted = inverted,
            Name = name
        };
    }

    [JsonConstructor]
    public EnabledCondition() : this("", false)
    {
    }
}
