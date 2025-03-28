public class Match {
    public int Id; 
    public string LocalTeam = "";
    public string VisitTeam = "";
    public string Date = "";
    public int Goals = 0;
    public int YellowCards = 0;
    public int RedCards = 0;
    public bool ExtraTime = false; 

    public Match(String localTeam, String visitTeam, String date){
        this.LocalTeam = localTeam;
        this.VisitTeam = visitTeam;
        this.Date = date;
    }

    public void updateLocalTeam(string newLocalTeam){
        this.LocalTeam = newLocalTeam;
    }

    public void updateVisitTeam(string newVisitTeam){
        this.VisitTeam = newVisitTeam;
    }

    public void updateDate(string date){
        this.Date = date;
    }

    public void registerGoal(){
        this.Goals += 1;
    }

    public void registerYellowCard(){
        this.YellowCards += 1;
    }

    public void registerRedCard(){
        this.RedCards += 1;
    }

    public void setExtraTime(){
        this.ExtraTime = true;
    }


}
