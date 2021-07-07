<%@ Application Codebehind="Global.asax.cs" Inherits="RegexLib.com.Global" Language="C#" %>

<script runat="server">

    
    void Profile_MigrateAnonymous(object sender, ProfileMigrateEventArgs e) {
        
        // Migrate
        ProfileCommon profile = Profile.GetProfile(e.AnonymousID) ;
        if( profile.OptionsPanelSet ) { 
            Profile.OptionsPanelExpanded = profile.OptionsPanelExpanded;
        }

        if (!string.IsNullOrEmpty(profile.FullName)) {
            Profile.FullName = profile.FullName;
        }
        
        // Clean-up
        ProfileManager.DeleteProfile(e.AnonymousID);
        AnonymousIdentificationModule.ClearAnonymousIdentifier();
    }
       
</script>
