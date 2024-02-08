namespace DatingAppProCardoAI.Domain
{
    public class MatchProfile
    {
        public int Id { get; set; }
        public int ProfileId {  get; set; }
        public  Profile profile { get; set; }



        public bool IsMatch(Profile currentUserProfile, Profile otherProfile)
        {
            if (currentUserProfile == null || otherProfile == null)
            {
                return false;
            }

           
            if (currentUserProfile.Preferences.SequenceEqual(otherProfile.Description))
            {
                return true;
            }

            return false;
        }

    }


    
}
