public class Activity
{
    public int Type { get; set; }
    public Int64 ApplicationId { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Details { get; set; }
    public ActivityTimestamps Timestamps { get; set; }
    public ActivityAssets Assets { get; set; }
    public ActivityParty Party { get; set; }
    public ActivitySecrets Secrets { get; set; }
    public bool Instance { get; set; }
}

public partial struct ActivityTimestamps
{
    public Int64 Start { get; set; }
    public Int64 End { get; set; }
}
public partial struct ActivityAssets
{
    public string LargeImage { get; set; }
    public string LargeText { get; set; }
    public string SmallImage { get; set; }
    public string SmallText { get; set; }
}
public partial struct ActivityParty
{
    public string Id { get; set; }
    public PartySize Size { get; set; }
}
public partial struct PartySize
{
    public Int32 CurrentSize { get; set; }
    public Int32 MaxSize { get; set; }
}
public partial struct ActivitySecrets
{ 
    public string Match { get; set; }
    public string Join { get; set; }
    public string Spectate { get; set; }
}