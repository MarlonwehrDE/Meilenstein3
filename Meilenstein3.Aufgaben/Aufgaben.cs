using System;


namespace Meilenstein3.Aufgaben;

public class Aufgaben
{
    DateTime _frequenzy = DateTime.Now - new TimeSpan(24, 0, 1);
    private DateTime _lastDone;
    string aufgabenBezeichnung;

    public Aufgaben()
    {
        
    }
    
   

    public void AufgabeErledigt()
    {
        if (_frequenzy < _lastDone)
        {
            _lastDone = DateTime.Now;
        }
        
    }
    
    
    
}