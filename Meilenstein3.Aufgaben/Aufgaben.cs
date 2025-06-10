public class Aufgabe
{
    private TimeSpan _frequenz;       
    private DateTime _lastDone;        
    public string AufgabenBezeichnung { get; set; }

    public Aufgabe(string bezeichnung, TimeSpan frequenz)
    {
        AufgabenBezeichnung = bezeichnung;
        _frequenz = frequenz;
        _lastDone = DateTime.MinValue; 
    }

    public bool KannErledigtWerden()
    {
        return (DateTime.Now - _lastDone) >= _frequenz;
    }

    public bool AufgabeErledigt()
    {
        if (KannErledigtWerden())
        {
            _lastDone = DateTime.Now;
            return true;
        }
        return false;
    }

    public DateTime LetztesMalErledigt => _lastDone;
}