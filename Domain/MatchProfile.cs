namespace DatingAppProCardoAI.Domain
{
    public class MatchProfile
    {
        public int Id { get; set; }
        public int ProfileId {  get; set; }
        public  Profile profile { get; set; }


      
        public bool IsMatch(Profile other)
        {
            if (profile == null || other == null)
            {
                return false;
            }

            if (profile.Preferences.Equals(other.Description))
            {
                return true;
            }
            return false;
        }

    }


    
}
