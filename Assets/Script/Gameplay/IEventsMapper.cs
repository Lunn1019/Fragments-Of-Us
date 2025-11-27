using System.Collections;
using System.Collections.Generic;

public interface IEventsMapper
{
    public void SetScoreSystem();

    public List<int> GetCheckScores();

    public SongTitle GetSongTitle();

    public List<float> GetCheckPoints();

    public List<EventData> GetEventDatas();    
}
