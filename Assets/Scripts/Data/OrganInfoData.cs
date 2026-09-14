using UnityEngine;

[CreateAssetMenu(
    fileName = "NewOrganInfoData",
    menuName = "BIO AR/Organ Information")]
public class OrganInfoData : ScriptableObject
{
    public string organName;

    [TextArea(2, 5)]
    public string overview;

    [TextArea(3, 6)]
    public string structure;

    [TextArea(3, 6)]
    public string organFunction;

    [TextArea(2, 5)]
    public string didYouKnow;
}