
[System.Serializable]
public class SaveStatsPlayer
{
    //aca se colocan que datos necesito guardar del player
    public int maxHealth;
    //public int water;
    //public int fire;
    //public int ghost;
    //public int iron;
    //public int electric;
    //public int glass;
    //public int poison;

    //tambien deberiamos ver como agregar un punto a guardar de las misiones ya hechas//
    public SaveStatsPlayer(Player player)
    {
        maxHealth = player.maxHealth;
        //    water = player. etc
    }

}
